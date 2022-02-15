using System;
using System.Collections.Generic;
using UnityEngine;

namespace CellularAutomata
{
    public class Cell: MonoBehaviour
    {
        public Action cbCellClicked;

        private bool isAlive;
        public bool IsAlive 
        {
            get 
            { 
                return isAlive; 
            }
            set 
            { 
                isAlive = value;
                UpdateColor();
            } 
        }

        public int x;
        public int y;
        public int i;

        private Color32 filledColor = new Color32(169, 184, 202, 255);
        private Color32 emptyColor = Color.black;

        private SpriteRenderer sr;

        public void SetUp(bool isAlive, int x, int y, int i)
        {
            this.isAlive = isAlive;
            this.x = x;
            this.y = y;
            this.i = i;

            sr = gameObject.GetComponent<SpriteRenderer>();
            UpdateColor();
        }

        private void UpdateColor()
        {
            if (isAlive == true)
            {
                sr.color = filledColor;
                return;
            }

            sr.color = emptyColor;
        }

        private void OnMouseDown()
        {
            isAlive = !isAlive;
            UpdateColor();

            cbCellClicked.Invoke();
        }

        // Callbacks
        public void RegisterCellClicked(Action callbackFunc)
        {
            cbCellClicked += callbackFunc;
        }

        public void UnregisterCellClicked(Action callbackFunc)
        {
            cbCellClicked -= callbackFunc;
        }



    }
}