using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CellularAutomata
{
    public class GridController : MonoBehaviour
    {
        private CellGrid cellGrid;

        void OnEnable()
        {
            cellGrid = new CellGrid(5, 5);
        }


        private void Update()
        {
            cellGrid.Update();
        }



        public CellGrid GetCellGrid()
        {
            return cellGrid;
        }
    }
}