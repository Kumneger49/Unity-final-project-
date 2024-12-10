
using UnityEngine; // Provides access to Unity's core engine functionality.
using UnityEngine.InputSystem; // Provides access to Unity's Input System for handling inputs.

// Ensures this script is only attached to GameObjects with an Animator component.
[RequireComponent(typeof(Animator))]
public class AnimateHandController : MonoBehaviour // Define the AnimateHandController class, which extends MonoBehaviour.
{
    // Public references to InputAction assets for grip and trigger inputs, configurable in the Unity Editor.
    public InputActionReference gripInputActionReference;
    public InputActionReference triggerInputActionReference;

    // Private fields to store the Animator component and input values for grip and trigger.
    private Animator _handAnimator; // Reference to the Animator component controlling hand animations.
    private float _gripValue; // Current value of the grip input.
    private float _triggerValue; // Current value of the trigger input.

    // Initialization logic, executed when the script starts.
    private void Start()
    {
        _handAnimator = GetComponent<Animator>(); // Get the Animator component attached to this GameObject.
    }

    // Called every frame to update the hand animations.
    private void Update()
    {
        AnimateGrip(); // Update the grip animation based on input.
        AnimateTrigger(); // Update the trigger animation based on input.
    }

    // Logic to animate the grip of the hand.
    private void AnimateGrip()
    {
        _gripValue = gripInputActionReference.action.ReadValue<float>(); // Read the grip input value (0 to 1) from the Input System.
        _handAnimator.SetFloat("Grip", _gripValue); // Set the "Grip" parameter in the Animator to drive animations.
    }

    // Logic to animate the trigger of the hand.
    private void AnimateTrigger()
    {
        _triggerValue = triggerInputActionReference.action.ReadValue<float>(); // Read the trigger input value (0 to 1) from the Input System.
        _handAnimator.SetFloat("Trigger", _triggerValue); // Set the "Trigger" parameter in the Animator to drive animations.
    }
}
