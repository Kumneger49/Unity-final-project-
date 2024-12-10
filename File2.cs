
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveUIObjectWithRay : MonoBehaviour
{
    private XRRayInteractor rightHandRayInteractor; // Reference to the right-hand ray interactor
    private bool isDragging = false;               // Indicates if a drag operation is active
    private RectTransform draggedUIObject;         // The UI object being dragged
    public float dragSpeed = 5f;                   // Speed for smooth dragging
    private Vector3 initialOffset;                 // Offset between the object and ray hit point

    void Start()
    {
        // Find the right-hand ray interactor in the scene
        var xrControllers = FindObjectsOfType<XRRayInteractor>();
        foreach(var interactor in xrControllers)
        {
            if (interactor.name.Contains("Right")) // Ensure proper naming in the scene
            {
                rightHandRayInteractor = interactor;
                break;
            }
        }
    }

    void Update()
    {
        if (isDragging && rightHandRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            // Calculate the new target position using the offset
            Vector3 targetPosition = hit.point + initialOffset;

            // Smoothly move the dragged UI object to the target position
            draggedUIObject.position = Vector3.Lerp(
                draggedUIObject.position, targetPosition, Time.deltaTime * dragSpeed
            );
        }
    }

    public void OnSelectEntered(XRBaseInteractor interactor)
    {
        // Check if the event is triggered by the right hand
        if (interactor == rightHandRayInteractor)
        {
            draggedUIObject = GetComponent<RectTransform>(); // Get the RectTransform of the UI object

            // Calculate the initial offset between the object and the ray hit point
            if (rightHandRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                initialOffset = draggedUIObject.position - hit.point;
            }

            isDragging = true; // Start dragging
        }
    }

    public void OnSelectExited(XRBaseInteractor interactor)
    {
        if (interactor == rightHandRayInteractor)
        {
            isDragging = false;  // Stop dragging
            draggedUIObject = null; // Clear the reference to the dragged object
        }
    }
}