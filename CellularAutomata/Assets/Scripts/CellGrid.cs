using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CellularAutomata
{
    public enum Direction { N, NE, E, SE, S, SW, W, NW};

    public class CellGrid: MonoBehaviour
    {
        [SerializeField]
        private UserInputController userInputController;

        [SerializeField]
        private SoundController soundController;

        [SerializeField]
        private GameObject cellPrefab;

        private List<Cell> grid;
        private int width;
        private int height;

        private bool isPaused;

        private float counterThreshold = 1.5f;
        private float counter;
        private float iteractionSpeed = 1f;

        private void OnEnable()
        {
            // Start as paused
            isPaused = true;
            counter = 0;

            width = 40;
            height = 20;

            // Register user input
            userInputController.RegisterIsPaused(OnPaused);
            userInputController.RegisterSpeedChanged(OnSpeedChanged);
            userInputController.RegisterClearAll(ClearAll);

            GenerateGrid();
        }

        private void GenerateGrid()
        {
            grid = new List<Cell>();

            int i = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    CreateNewCell(x, y, i);

                    // Increment i
                    i += 1;
                }
            }
        }

        /// <summary>
        /// Creates a cell gameobject at the provided location.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="i"></param>
        private void CreateNewCell(int x, int y, int i)
        {
            // Create Cell gameobject
            GameObject cellGO = Instantiate(cellPrefab, this.transform);
            cellGO.transform.position = new Vector3(x, y, 0);

            // Get and setup cell
            Cell cell = cellGO.GetComponent<Cell>();
            cell.SetUp(false, x, y, i);

            cell.RegisterCellClicked(soundController.OnCellClicked);

            grid.Add(cell);
        }

        public void Update()
        {
            if (isPaused == false)
            {
                SimulateGameOfLife();
            }
        }

        private void SimulateGameOfLife()
        {
            // First check update time thresholds
            counter += (Time.deltaTime * iteractionSpeed);
            if (counter < counterThreshold) { return; }

            // Reduce counter
            counter -= counterThreshold;

            bool[] replacementGrid = new bool[grid.Count];

            // Determine next generation
            for (int i = 0; i < grid.Count; i++)
            {
                replacementGrid[i] = LifeRules(grid[i]);
            }

            // Update the current generation to be the next generation
            for (int i = 0; i < grid.Count; i++)
            {
                grid[i].IsAlive = replacementGrid[i];
            }
        }

        /// <summary>
        /// Returns true if the cell is still alive.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private bool LifeRules(Cell cell)
        {
            bool isAlive;
            int aliveNeighborCount = GetAliveNeighborCount(cell);

            // Is cell alive?
            if (cell.IsAlive)
            {
                // Die if fewer than two alive neighbors
                if (aliveNeighborCount < 2)
                {
                    isAlive = false;
                }
                // Die if more than 3 alive neighbors
                else if (aliveNeighborCount > 3)
                {
                    isAlive = false;
                }
                else
                {
                    isAlive = true;
                }
            }
            else
            {
                // Born if exactly three alive neighbors
                if (aliveNeighborCount == 3)
                {
                    isAlive = true;
                }
                // Stay dead/empty
                else
                {
                    isAlive = false;
                }
            }

            return isAlive;
        }

        private int GetAliveNeighborCount(Cell cell)
        {
            int aliveNeighborCount = 0;

            // For all directions
            foreach (Direction direction in (Direction[])System.Enum.GetValues(typeof(Direction)))
            {
                // Is the neighbor alive?
                if (GetNeighbor(cell, direction).IsAlive == true)
                {
                    aliveNeighborCount += 1;
                }
            }

            return aliveNeighborCount;
        }

        private void OnPaused(bool p)
        {
            isPaused = p;
        }

        private void OnSpeedChanged(float change)
        {
            iteractionSpeed *= change;
            Debug.Log("Speed");
        }

        /// <summary>
        /// Kills all of the cells.
        /// </summary>
        private void ClearAll()
        {
            Debug.Log("ClearAll");
            for (int i = 0; i < grid.Count; i++)
            {
                grid[i].IsAlive = false;
            }
        }

        /// <summary>
        /// Returns a neighboring cell in the provided direction.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private Cell GetNeighbor(Cell cell, Direction direction)
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

                case Direction.NE:
                    index = cell.i;

                    // if on the East edge
                    if (index % width == width - 1)
                    {
                        index -= (width - 1);
                    }
                    else
                    {
                        // Shift to right
                        index += 1;
                    }

                    // if on the North edge
                    if (index >= width * (height - 1))
                    {
                        index -= (width * (height - 1));
                    }
                    else
                    {
                        // Shift up
                        index += width;
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

                case Direction.SE:
                    index = cell.i;

                    // if on the East edge
                    if (index % width == width - 1)
                    {
                        index -= (width - 1);
                    }
                    else
                    {
                        // Shift to right
                        index += 1;
                    }

                    // if on the South edge
                    if (index < width)
                    {
                        index += (width * (height - 1));
                    }
                    else
                    {
                        // Shift down
                        index -= width;
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

                case Direction.SW:
                    index = cell.i;

                    // if on the West edge
                    if (index % width == 0)
                    {
                        index += (width - 1);
                    }
                    else
                    {
                        // Shift to left
                        index -= 1;
                    }

                    // if on the South edge
                    if (index < width)
                    {
                        index += (width * (height - 1));
                    }
                    else
                    {
                        // Shift down
                        index -= width;
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

                case Direction.NW:
                    index = cell.i;

                    // if on the West edge
                    if (index % width == 0)
                    {
                        index += (width - 1);
                    }
                    else
                    {
                        // Shift to left
                        index -= 1;
                    }

                    // if on the North edge
                    if (index >= width * (height - 1))
                    {
                        index -= (width * (height - 1));
                    }
                    else
                    {
                        // Shift up
                        index += width;
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