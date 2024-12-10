using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace Scene21.ButtonHandlers
{
    public class ControllerButtonHandler : MonoBehaviour, ISubmitHandler, ISelectHandler
    {
        public string sceneToLoad = "Scene2"; // Changed to Scene2 as per your requirement
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(LoadScene);
        }

        public void OnSelect(BaseEventData eventData)
        {
            // Optional: Do something when the button is selected
            Debug.Log("Button selected");
        }

        public void OnSubmit(BaseEventData eventData)
        {
            Debug.Log("Submit event received");
            LoadScene();
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        private void OnDestroy()
        {
            if (button != null)
            {
                button.onClick.RemoveListener(LoadScene);
            }
        }
    }
}