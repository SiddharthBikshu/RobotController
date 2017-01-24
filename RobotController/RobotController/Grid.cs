using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotController
{
    /// <summary>
    /// Grid that represents column and rows Counts
    /// </summary>
    public class Grid
    {
        public int RowsCount { get; set; }
        public int ColumnsCount { get; set; }
        Location[,] LocationGrid;

        public Grid(int rowsCount, int columnsCount)
        {
            LocationGrid = new Location[columnsCount, rowsCount];
            for (int i = 0; i < columnsCount; i++)
            {
                for (int j = 0; j < rowsCount; j++)
                {
                    LocationGrid[i, j] = new Location(i, j);
                }
            }
            this.RowsCount = LocationGrid.GetLength(1);
            this.ColumnsCount = LocationGrid.GetLength(0);
        }

        /// <summary>
        /// Places an obstacle to the grid
        /// </summary>
        /// <param name="X">X cordinate of obstacle</param>
        /// <param name="Y">Y cordinate of obstacle</param>
        /// <param name="obstacle">Obstacle to place</param>
        /// <returns>True if the obstacle has been place to the grid.</returns>
        public bool PlaceObstacle(int x, int y, IObstacle obstacle)
        {
            if (x >= 0 && y >= 0 && x < RowsCount && y < ColumnsCount)
            {
                LocationGrid[x, y].Obstacle = obstacle;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get the Location object for a given coordinate
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Location GetLocation(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < RowsCount && y < ColumnsCount)
            {
                return LocationGrid[x, y];
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format("{0}, {1} are outside of grid dimension.", x, y));
            }
        }
    }
}