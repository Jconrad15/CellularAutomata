using System.Collections;
using System.Collections.Generic;

namespace CellularAutomata
{
    public class Cell
    {
        public bool isFilled;

        public int x;
        public int y;
        public int i;

        public Cell(bool isFilled, int x, int y, int i)
        {
            this.isFilled = isFilled;
            this.x = x;
            this.y = y;
            this.i = i;
        }
    }
}