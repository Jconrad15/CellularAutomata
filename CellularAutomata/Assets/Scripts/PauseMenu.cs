using UnityEngine;

namespace CellularAutomata
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private UserInputController userInputController;

        [SerializeField]
        private GameObject pausePanel;

        void OnEnable()
        {
            userInputController.RegisterIsPaused(OnPaused);
        }

        private void OnPaused(bool isPaused)
        {
            pausePanel.SetActive(isPaused);
        }
    }
}