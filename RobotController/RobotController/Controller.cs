using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RobotController
{
    /// <summary>
    /// Creates Grid, places Obstacle, Creates Robot(s), moves the robots around.
    /// </summary>
    public static class Controller
    {
        /// <summary>
        /// create and place an obstacle onto a grid at a specified coordinate
        /// </summary>
        /// <param name="grid">The Gird to place the obstacle</param>
        /// <param name="x">X coordinate to place the obstacle</param>
        /// <param name="y">Y coordinate to place the obstacle</param>
        /// <param name="obstacleConfig">Configuration to </param>
        /// <returns>True if the obstacle is placed successfully.</returns>
        public static bool PlaceObstacle(Grid grid, int x, int y, IObstacleConfigurations obstacleConfig)
        {
            IObstacle obstacle = null;
            if (obstacleConfig != null)
            {
                ObstacleFactory obstacleFactory = new ObstacleFactory(obstacleConfig);
                obstacle = obstacleFactory.CreateObstacle();
            }

            if (obstacle != null && grid != null)
            {
                return grid.PlaceObstacle(x, y, obstacle);
            }
            return false;
        }

        /// <summary>
        /// Creates a robot at a specified location on a gird.
        /// </summary>
        /// <param name="x">X coordinate of the robot.</param>
        /// <param name="y">Y coordinate of the robot.</param>
        /// <param name="direction">Direction to which robot is currently looking</param>
        /// <param name="grid">The Grid to place the Robot</param>
        /// <returns></returns>
        public static IRobot CreateRobot(int x, int y, Direction direction, Grid grid)
        {
            if (grid != null)
            {
                Location robotLocation = grid.GetLocation(x, y);
                return new RobotController.Robot(robotLocation, direction, grid);
            }
            return null;
        }
    }
}