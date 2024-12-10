using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;  // Required for ISubmitHandler and BaseEventData

namespace Scene12.ButtonHandlers
{
    public class ControllerButtonHandler : MonoBehaviour, ISelectHandler, ISubmitHandler
    {
        public string sceneToLoad = "Scene2";  // Name of the scene to load
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(LoadScene);  // Handle button click
        }

        // Implementing ISelectHandler: Called when the button is selected (e.g., focused)
        public void OnSelect(BaseEventData eventData)
        {
            Debug.Log("Button selected");
            // Optional: You can perform custom logic when the button is selected
        }

        // Implementing ISubmitHandler: Called when the button is submitted (e.g., pressed)
        public void OnSubmit(BaseEventData eventData)
        {
            Debug.Log("Button submit triggered!");
            LoadScene();  // Call the LoadScene method when submit is triggered
        }

        // This method is used to load the specified scene
        private void LoadScene()
        {
            SceneManager.LoadScene(sceneToLoad);
            Debug.Log("Loading Scene: " + sceneToLoad);  // Optional: Log the name of the scene
        }

        // Remove the listener to prevent memory leaks or incorrect behavior
        private void OnDestroy()
        {
            button.onClick.RemoveListener(LoadScene);
        }
    }
}
