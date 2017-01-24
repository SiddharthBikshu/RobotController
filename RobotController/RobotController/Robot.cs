using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotController
{
    /// <summary>
    /// Robot interface
    /// </summary>
    public interface IRobot
    {
        Direction Direction { get; set; }
        Grid Grid { get; set; }
        void ExecuteMoveSequence(string sequence);
        void Move(int x, int y);
    }

    /// <summary>
    /// A Robot which walks a known grid and responds to any obstacles present on its way.
    /// </summary>
    public class Robot : IRobot
    {
        private Location _location;
        public Direction Direction { get; set; }
        public Grid Grid { get; set; }

        public Robot(Location location, Direction direction, Grid grid)
        {
            _location = location;
            this.Direction = direction;
            this.Grid = grid;

            //Ensure when the robot is created at a location having an obstacle, we need to honor that obstacle.
            if (_location.Obstacle != null)
            {
                if (_location.Obstacle.CanStepOn(this))
                {
                    Move(_location);
                }
                else
                {
                    Console.WriteLine(
                        "Invalid Operation: Robot cannot be created on a location that cannot be stepped upon.");
                    throw new InvalidOperationException(
                        "Robot cannot be created on a location that cannot be stepped upon.");
                }
            }
        }

        /// <summary>
        /// Moves the Robot on the Grid boundaries and honor any obstacle encountered
        /// </summary>
        /// <param name="sequence">F=Forward, L=Left, R=Right</param>
        public void ExecuteMoveSequence(string sequence)
        {
            if (!string.IsNullOrEmpty(sequence))
            {
                foreach (char commandLetter in sequence.ToCharArray())
                {
                    string move;
                    try
                    {
                        switch (commandLetter)
                        {
                            case 'L':
                                move = "Left";
                                Console.WriteLine("\nMoving Robot " + move);
                                MoveLeft();
                                break;
                            case 'R':
                                move = "Right";
                                Console.WriteLine("\nMoving Robot " + move);
                                MoveRight();
                                break;
                            case 'F':
                                move = "Forward";
                                Console.WriteLine("\nMoving Robot " + move);
                                MoveForward();
                                break;
                            default:
                                throw new ArgumentException(string.Format("Invalid command sequence: {0}", commandLetter));
                        }
                        Console.WriteLine("Robot moved: " + move + " to " + this.ToString());
                    }
                    catch (InvalidOperationException ex)
                    {
                        //Execute other command sequence even if this command sequence cannot be processed.
                        Console.WriteLine(ex.Message);
                    }
                    catch (ArgumentException)
                    {
                        //Let's caller know there was an invalid command seqeunce passed.
                        throw;
                    }
                }
            }
        }

        public override string ToString()
        {
            return _location.X + ", " + _location.Y + " facing " + Direction.ToString();
        }

        /// <summary>
        /// Moves the robot to Left by one location
        /// </summary>
        private void MoveLeft()
        {
            bool canMove = true;
            int newX = _location.X;
            int newY = _location.Y;
            switch (this.Direction)
            {
                case Direction.Top:
                    if (_location.X == 0)
                    {
                        canMove = false;
                    }
                    else
                    {
                        newX = _location.X - 1;
                    }
                    break;
                case Direction.Bottom:
                    if (_location.X == this.Grid.ColumnsCount - 1)
                    {
                        canMove = false;
                    }
                    else
                    {
                        newX = _location.X + 1;
                    }
                    break;
                case Direction.Left:
                    if (_location.Y == this.Grid.RowsCount - 1)
                    {
                        canMove = false;
                    }
                    else
                    {
                        newY = _location.Y + 1;
                    }
                    break;
                case Direction.Right:
                    if (_location.Y == 0)
                    {
                        canMove = false;
                    }
                    else
                    {
                        newY = _location.Y - 1;
                    }
                    break;
            }

            //Check for grid boundary condition
            if (canMove)
            {
                Move(newX, newY);
            }
            else
            {
                throw new InvalidOperationException("Cannot move Left, grid edge encountered.");
            }
        }

        /// <summary>
        /// Moves the robot to Right by one location
        /// </summary>
        private void MoveRight()
        {
            bool canMove = true;
            int newX = _location.X;
            int newY = _location.Y;
            switch (this.Direction)
            {
                case Direction.Top:
                    if (_location.X == this.Grid.ColumnsCount - 1)
                    {
                        canMove = false;
                    }
                    else
                    {
                        newX = _location.X + 1;
                    }
                    break;
                case Direction.Bottom:
                    if (_location.X == 0)
                    {
                        canMove = false;
                    }
                    else
                    {
                        newX = _location.X - 1;
                    }
                    break;
                case Direction.Left:
                    if (_location.Y == 0)
                    {
                        canMove = false;
                    }
                    else
                    {
                        newY = _location.Y - 1;
                    }
                    break;
                case Direction.Right:
                    if (_location.Y == this.Grid.RowsCount - 1)
                    {
                        canMove = false;
                    }
                    else
                    {
                        newY = _location.Y + 1;
                    }
                    break;
            }

            //Check for grid boundary condition
            if (canMove)
            {
                Move(newX, newY);
            }
            else
            {
                throw new InvalidOperationException("Cannot move Right, grid edge encountered.");
            }
        }

        /// <summary>
        /// Moves the robot forward by one location
        /// </summary>
        private void MoveForward()
        {
            bool canMove = true;
            int newX = _location.X;
            int newY = _location.Y;
            switch (this.Direction)
            {
                case Direction.Top:
                    if (_location.Y == 0)
                    {
                        canMove = false;
                    }
                    else
                    {
                        newY = _location.Y - 1;
                    }
                    break;
                case Direction.Bottom:
                    if (_location.Y == this.Grid.RowsCount - 1)
                    {
                        canMove = false;
                    }
                    else
                    {
                        newY = _location.Y + 1;
                    }
                    break;
                case Direction.Left:
                    if (_location.X == 0)
                    {
                        canMove = false;
                    }
                    else
                    {
                        newX = _location.X - 1;
                    }
                    break;
                case Direction.Right:
                    if (_location.X == this.Grid.ColumnsCount - 1)
                    {
                        canMove = false;
                    }
                    else
                    {
                        newX = _location.X + 1;
                    }
                    break;
            }

            //Check for grid boundary condition
            if (canMove)
            {
                Move(newX, newY);
            }
            else
            {
                throw new InvalidOperationException("Cannot move Forward, grid edge encountered.");
            }
        }

        /// <summary>
        /// Moves the Robot to a new location in the Grid and honor any obstacle present over the new location.
        /// </summary>
        /// <param name="RobotNewLocation">The location where robot is exptected to move</param>
        private void Move(Location RobotNewLocation)
        {
            if (RobotNewLocation.Obstacle != null)
            {
                if (RobotNewLocation.Obstacle.CanStepOn(this))
                {
                    _location = RobotNewLocation;
                    _location.Obstacle.Execute(this);
                }
                else
                {
                    throw new InvalidOperationException(
                        string.Format(
                            "Robot cannot move to {0} as it contains an obstacle that cannot be stepped upon.",
                            RobotNewLocation.ToString()));
                }
            }
            else
            {
                _location = RobotNewLocation;
            }
        }

        /// <summary>
        /// Moves the robot to a given X and Y coordinates of the Gird
        /// </summary>
        /// <param name="x">X position to move the robot</param>
        /// <param name="y">Y position to move the robot</param>
        public void Move(int x, int y)
        {
            Location robotNewLocation = Grid.GetLocation(x, y);
            Move(robotNewLocation);
        }
    }


    /// <summary>
    /// Directions for a robot, values represents angle based on clockwise direction
    /// </summary>
    public enum Direction
    {
        Top = 0,
        Right = 90,
        Bottom = 180,
        Left = 270
    }
}