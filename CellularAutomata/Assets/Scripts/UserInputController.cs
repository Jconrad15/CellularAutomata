using System;
using System.Collections.Generic;
using UnityEngine;

namespace CellularAutomata
{
    public class UserInputController : MonoBehaviour
    {
        private bool isPaused;

        public Action<bool> cbIsPaused;

        // Update is called once per frame
        void Update()
        {
            UserInput();
        }

        private void UserInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isPaused = !isPaused;
                cbIsPaused.Invoke(isPaused);
            }
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