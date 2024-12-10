using UnityEngine; // Includes Unity's core functionality.
using Unity.XR.CoreUtils; // Provides XR utilities, including XROrigin for XR setups.

public class BoundaryConstraint : MonoBehaviour
{
    // Define the boundaries for the allowed movement area in the X and Z axes.
    private float maxX = 3122f; // Maximum X-coordinate value.
    private float minX = -540f; // Minimum X-coordinate value.
    private float maxZ = -908f; // Maximum Z-coordinate value.
    private float minZ = -3344f; // Minimum Z-coordinate value.

    private XROrigin xrOrigin; // Reference to the XROrigin component, which represents the player's position in XR.

    // Initialization logic, runs once at the start of the script's lifecycle.
    void Start()
    {
        // Attempt to get the XROrigin component attached to the same GameObject.
        xrOrigin = GetComponent<XROrigin>();

        // If the XROrigin component is not found, log an error and disable this script.
        if (xrOrigin == null)
        {
            Debug.LogError("XROrigin component not found!"); // Print an error message in the console.
            enabled = false; // Disable this script to prevent further execution.
            return; // Exit the Start method.
        }
    }

    // LateUpdate is called once per frame, after all other updates.
    // It's ideal for applying constraints to ensure all movement updates have been processed.
    void LateUpdate()
    {
        // Check if the XROrigin reference is valid.
        if (xrOrigin != null)
        {
            // Get the current position of the XR Origin.
            Vector3 currentPosition = xrOrigin.transform.position;

            // Clamp the position's X and Z values to stay within the defined boundaries.
            // Y-coordinate remains unchanged (no vertical constraints).
            Vector3 clampedPosition = new Vector3(
                Mathf.Clamp(currentPosition.x, minX, maxX), // Restrict X within [minX, maxX].
                currentPosition.y,                         // Keep the Y-coordinate unchanged.
                Mathf.Clamp(currentPosition.z, minZ, maxZ) // Restrict Z within [minZ, maxZ].
            );

            // If the current position is outside the boundaries, adjust it to the clamped position.
            if (currentPosition != clampedPosition)
            {
                xrOrigin.transform.position = clampedPosition; // Apply the clamped position to the XR Origin.
            }
        }
    }
}
