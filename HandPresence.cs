using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

//builds on the fucnitonality of the hand script by adding movement capabilities while maintaining the core hand animation features
//this script ensures the updating of the animations and input DURING MOVEMENT
public class HandPresence : MonoBehaviour
{
    //the controller device that this script targets
    private InputDevice targetDevice;
    //identify the controller
    private InputDeviceCharacteristics controllerCharacteristics;
    //prefab for ht ehand model that is the visual representation o the player's hand
    public GameObject handModelPrefab;
    //
    private GameObject spawnedHandModel;
    private Animator handAnimator;

    // Movement speed of the player when using the controller
    public float moveSpeed = 1.0f;
    public float rotationSpeed = 50f;  // Optional: For rotating the user (optional)

    //reference to the player's position(typically the camera of the xr rig)
    private Transform playerTransform;
    //direction of movement based controllers
    private Vector3 movementDirection = Vector3.zero;

    // Start is called when script is initialized
    void Start()
    {
        // Set the characteristics to look for the right controller (or both controllers as needed)
        controllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;

        //list to store all devices that match the specified characteristics
        List<InputDevice> devices = new List<InputDevice>();
        //populates list with devices that match the characteristics
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        //debug log all devices found for testing and verification
        //iterates though the detected decices and confirms that tthe devices
        //are detected correctly by printing characteristics
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];

            // Instantiate Hand Model=bring your hand model into your vr world from the prefab
            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();

            // Initialize player transform (the user or camera rig's position)
            if (Camera.main != null)
            {
                //like the address and direction of view that the player sees
                playerTransform = Camera.main.transform;
            }
            else
            {
                Debug.LogError("No main camera found.");
            }
        }
    }

    // Update hand animations based on controller input
    //      takes input from hand and makes the hand model in the vr world behave the same way
    void UpdateHandAnimation()
    {
        //checks if trigger is being squeezed
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            //returns the float for how much it is being squeezed
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        //same for grip here
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Movement input from the touchpad/joystick
        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
        {
            // Optional: Add a dead zone for more controlled movement
            if (primary2DAxisValue.magnitude > 0.1f)
            {
                // Translate the player based on the joystick/touchpad input
                movementDirection = new Vector3(primary2DAxisValue.x, 0, primary2DAxisValue.y);
                playerTransform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);
            }
        }

        // Update the hand animation based on controller input!!!!!
        UpdateHandAnimation();
    }
}
