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
        public Action cbClearAll;

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

        public void ClearAllButton()
        {
            cbClearAll.Invoke();
        }

        // Callbacks
        public void RegisterClearAll(Action callbackFunc)
        {
            cbClearAll += callbackFunc;
        }

        public void UnregisterClearAll(Action callbackFunc)
        {
            cbClearAll -= callbackFunc;
        }

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