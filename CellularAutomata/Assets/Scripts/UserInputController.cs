using System;
using System.Collections.Generic;
using UnityEngine;

namespace CellularAutomata
{
    public class UserInputController : MonoBehaviour
    {
        private bool isPaused;

        public Action<bool> cbIsPaused;
        public Action<float> cbSpeedChanged;

        // Update is called once per frame
        void Update()
        {
            PauseInput();
        }

        private void PauseInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isPaused = !isPaused;
                cbIsPaused.Invoke(isPaused);
            }
        }

        public void IncreaseSpeed()
        {
            cbSpeedChanged.Invoke(2f);
        }

        public void DecreaseSpeed()
        {
            cbSpeedChanged.Invoke(0.5f);
        }

        // Callbacks

        public void RegisterSpeedChanged(Action<float> callbackFunc)
        {
            cbSpeedChanged += callbackFunc;
        }

        public void UnregisterSpeedChanged(Action<float> callbackFunc)
        {
            cbSpeedChanged -= callbackFunc;
        }

        public void RegisterIsPaused(Action<bool> callbackFunc)
        {
            cbIsPaused += callbackFunc;
        }

        public void UnregisterIsPaused(Action<bool> callbackFunc)
        {
            cbIsPaused -= callbackFunc;
        }

    }
}