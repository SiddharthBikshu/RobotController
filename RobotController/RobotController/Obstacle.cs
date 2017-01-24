using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotController
{
    public interface IObstacleConfigurations
    {
        String ObstacleType { get; set; }
    }

    public class SpinnerConfiguration : IObstacleConfigurations
    {
        public String ObstacleType { get; set; }
        public int RotationAngle { get; set; }

        public SpinnerConfiguration(string type, int rotationAngle)
        {
            ObstacleType = type;
            RotationAngle = rotationAngle;
        }
    }

    public class HoleConfiguration : IObstacleConfigurations
    {
        public String ObstacleType { get; set; }
        public int ConnectedXLocation { get; set; }
        public int ConnectedYLocation { get; set; }

        public HoleConfiguration(string type, int newX, int newY)
        {
            ObstacleType = type;
            ConnectedXLocation = newX;
            ConnectedYLocation = newY;
        }
    }

    public class RockConfiguration : IObstacleConfigurations
    {
        public String ObstacleType { get; set; }

        public RockConfiguration(string type)
        {
            ObstacleType = type;
        }
    }

    /// <summary>
    /// Factory to create obstacle instances
    /// </summary>
    public class ObstacleFactory
    {
        IObstacleConfigurations _obstacleConfiguration;

        public ObstacleFactory(IObstacleConfigurations configuration)
        {
            _obstacleConfiguration = configuration;
        }

        public IObstacle CreateObstacle()
        {
            switch (_obstacleConfiguration.ObstacleType)
            {
                case "Rock":
                {
                    return new Rock(); //could have passed the configuaration but it don't need actually
                }
                case "Spinner":
                {
                    return new Spinner(_obstacleConfiguration);
                }
                case "Hole":
                {
                    return new Hole(_obstacleConfiguration);
                }
                default:
                {
                    return null;
                }
            }
        }
    }

    public interface IObstacle
    {
        //Decides if the obstacle can be stepped upon by a Robot
        bool CanStepOn(IRobot robot);
        //Change the Robot locaiton/direction etc as per the obstacle behaviour
        void Execute(IRobot robot);
    }

    /// <summary>
    /// Rock cannot be stepped upon by a Robot.
    /// </summary>
    public class Rock : IObstacle
    {
        public void Execute(IRobot robot)
        {
            if (robot != null)
            {
                Console.WriteLine("Robot: " + robot.ToString() + " cannot occupy Rock");
            }
        }

        public bool CanStepOn(IRobot robot)
        {
            //In future there could be a robot which can step on a rock, currently there is none.
            return false;
        }
    }

    /// <summary>
    /// Hole takes to a connected location if that can be stepped upon, else hole cannot be stepped upon.
    /// </summary>
    public class Hole : IObstacle
    {
        private int _connectedXLocation;
        private int _connectedYLocation;

        public Hole(IObstacleConfigurations holeConfiguration)
        {
            //The connected location does not know about any obstacle at that location as a Hole itself is an Obstacle.
            //We cannot guarrenty sequence of obstacle creation.
            HoleConfiguration holeconfig = holeConfiguration as HoleConfiguration;
            _connectedXLocation = holeconfig.ConnectedXLocation;
            _connectedYLocation = holeconfig.ConnectedYLocation;
        }

        public bool CanStepOn(IRobot robot)
        {
            //Assumption: You cannot step on a hole if connected location cannot be stepped upon.
            if (robot != null && robot.Grid.GetLocation(_connectedXLocation, _connectedYLocation).Obstacle != null)
            {
                return robot.Grid.GetLocation(_connectedXLocation, _connectedYLocation).Obstacle.CanStepOn(robot);
            }
            return true;
        }

        public void Execute(IRobot robot)
        {
            if (robot != null && CanStepOn(robot))
            {
                Console.WriteLine(string.Format("Hole encountered, moving Robot to: {0}, {1} ", _connectedXLocation,
                    _connectedYLocation));
                robot.Move(_connectedXLocation, _connectedYLocation);
            }
        }
    }

    /// <summary>
    /// Rotates a Robot by a rotation angle to change its direction on the Grid
    /// </summary>
    public class Spinner : IObstacle
    {
        private int _rotationAngle;

        public Spinner(IObstacleConfigurations spinnerConfiguration)
        {
            SpinnerConfiguration config = spinnerConfiguration as SpinnerConfiguration;

            Direction dir;
            if (!Enum.TryParse((config.RotationAngle % 360).ToString(), out dir))
            {
                Console.WriteLine("Invalid spinner rotation angle");
            }
            _rotationAngle = config.RotationAngle % 360;
        }

        public bool CanStepOn(IRobot robot)
        {
            return true;
        }

        public void Execute(IRobot robot)
        {
            Direction dir;
            //Get the new direction by adding the current direction and rotation angle, they both can be 0, 90, 180 or 270 increments.
            if (robot != null && Enum.TryParse((((int) (robot.Direction) + _rotationAngle) % 360).ToString(), out dir))
            {
                Console.WriteLine("Spinner encountered, rotating Robot by: " + _rotationAngle + " Degrees");
                robot.Direction = dir;
            }
        }
    }
}