using MartianRobots.Model;
using System;

namespace MartianRobots.Movements
{
    public class RotationBy90Degree : IMovement
    {
        public string Direction { get; private set; }
        public RotationBy90Degree(string direction)
        {
            if (string.IsNullOrWhiteSpace(direction))
            {
                throw new ArgumentNullException(nameof(direction));
            }
            Direction = direction.ToLower();
            if (direction != "l" && direction != "r")
            {
                throw new ArgumentException("for rotation by 90 degree direction must be L,l or R,r");
            }
        }

        public Result MoveRobot(Robot robot)
        {
            if (robot == null)
            {
                throw new ArgumentNullException(nameof(robot));
            }
            if (Direction == "l")
            {
                switch (robot.CurrentOrientation)
                {
                    case Orientation.North:
                        robot.CurrentOrientation = Orientation.West;
                        break;
                    case Orientation.West:
                        robot.CurrentOrientation = Orientation.South;
                        break;
                    case Orientation.South:
                        robot.CurrentOrientation = Orientation.East;
                        break;
                    case Orientation.East:
                        robot.CurrentOrientation = Orientation.North;
                        break;
                }
            }
            else if (Direction == "r")
            {
                switch (robot.CurrentOrientation)
                {
                    case Orientation.North:
                        robot.CurrentOrientation = Orientation.East;
                        break;
                    case Orientation.West:
                        robot.CurrentOrientation = Orientation.North;
                        break;
                    case Orientation.South:
                        robot.CurrentOrientation = Orientation.West;
                        break;
                    case Orientation.East:
                        robot.CurrentOrientation = Orientation.South;
                        break;
                }
            }
            return Result.SuccessfullResult;
        }
    }
}
