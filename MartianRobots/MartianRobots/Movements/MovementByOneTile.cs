
using MartianRobots.Model;
using System;

namespace MartianRobots.Movements
{
    public class MovementByOneTile : IMovement
    {
        private readonly IRobotMovementSafetyHandler _safetyHandler;
        private readonly int _gridWidth;
        private readonly int _gridHeight;

        public MovementByOneTile(IRobotMovementSafetyHandler safetyHandler, int gridWidth, int gridHeight)
        {
            _safetyHandler = safetyHandler;
            _gridHeight = gridHeight;
            _gridWidth = gridWidth;
        }

        public Result MoveRobot(Robot robot)
        {
            Result result = null;
            if (robot == null)
            {
                throw new ArgumentNullException(nameof(robot));
            }

            if (!_safetyHandler.IsSafeToMove(robot.X, robot.Y, robot.CurrentOrientation))
            {
                return Result.SuccessfullResult;
            }

            if (
                (robot.X == 0 && robot.CurrentOrientation == Orientation.West) ||
                (robot.X == _gridWidth - 1 && robot.CurrentOrientation == Orientation.East) ||
                (robot.Y == _gridHeight - 1 && robot.CurrentOrientation == Orientation.North) ||
                (robot.Y == 0 && robot.CurrentOrientation == Orientation.South)
                )
            {
                result = Result.FailedResult(robot);
            }

            if (result != null && !result.Success)
            {
                _safetyHandler.AddFailedMove(robot.X, robot.Y, robot.CurrentOrientation);
                return result;
            }


            if(robot.CurrentOrientation == Orientation.North)
            {
                robot.Y++;
            }
            else if (robot.CurrentOrientation == Orientation.South)
            {
                robot.Y--;
            }
            else if (robot.CurrentOrientation == Orientation.East)
            {
                robot.X++;
            }
            else if (robot.CurrentOrientation == Orientation.West)
            {
                robot.X--;
            }

            return Result.SuccessfullResult;
        }
    }
}
