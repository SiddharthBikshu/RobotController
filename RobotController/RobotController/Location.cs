using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotController
{
    /// <summary>
    /// Represents X, Y coordinates and any Obstacle on the location.
    /// </summary>
    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }

        //Assumption: Only one Obstacle at a given location
        public IObstacle Obstacle { get; set; }

        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return Obstacle != null
                ? string.Format("{0}, {1} {2}", X, Y, Obstacle.ToString())
                : string.Format("{0}, {1}", X, Y);
        }
    }
}