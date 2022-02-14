using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CellularAutomata
{
    public enum Direction { N, E, S, W };

    public class CellGrid
    {
        private List<Cell> grid;
        private int width;
        private int height;

        public CellGrid(int width, int height)
        {
            this.width = width;
            this.height = height;

            GenerateGrid();
        }

        private void GenerateGrid()
        {
            int i = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    grid.Add(new Cell(false, x, y, i));

                    // Increment i
                    i += 1;
                }
            }
        }

        public void Update()
        {

        }


        /// <summary>
        /// Returns a neighboring cell in the provided direction.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Cell GetNeighbor(Cell cell, Direction direction)
        {
            int index;

            switch (direction)
            {
                case Direction.N:
                    // if on North edge
                    if (cell.i >= width * (height - 1))
                    {
                        index = cell.i - (width * (height - 1));
                    }
                    else
                    {
                        index = cell.i + width;
                    }
                    break;

                case Direction.E:
                    // if on East edge
                    if (cell.i % width == width - 1)
                    {
                        index = cell.i - (width - 1);
                    }
                    else
                    {
                        index = cell.i + 1;
                    }
                    break;

                case Direction.S:
                    // if on South edge
                    if (cell.i < width)
                    {
                        index = cell.i + (width * (height - 1));
                    }
                    else
                    {
                        index = cell.i - width;
                    }
                    break;

                case Direction.W:
                    // if on West edge
                    if (cell.i % width == 0)
                    {
                        index = cell.i + (width - 1);
                    }
                    else
                    {
                        index = cell.i - 1;
                    }
                    break;

                default:
                    Debug.LogError("Why does enum not have a value?");
                    index = 0;
                    break;
            }

            return grid[index];
        }

    }
}