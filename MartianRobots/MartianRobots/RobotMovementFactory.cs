
using MartianRobots.Input;
using MartianRobots.Model;
using MartianRobots.Movements;
using System.Collections.Generic;

namespace MartianRobots
{
    public class RobotMovementFactory
    {
        public static List<Robot> CreateRobotData(int gridWidth,int gridHeight, List<RobotDataInput> robotInputData, IRobotMovementSafetyHandler _safetyHandler)
        {
            var robots = new List<Robot>();
            foreach (var input in robotInputData)
            {
                var movements = input.MovementData.ToCharArray();
                var robot = new Robot()
                {
                    CurrentOrientation = input.InitialOrientation,
                    X = input.StartX,
                    Y = input.StartY,
                    Movements = new List<IMovement>()
                };
                foreach (var movement in movements)
                {
                    if (movement.ToString().ToLower() == "l" || movement.ToString().ToLower() == "r")
                    {
                        robot.Movements.Add(new RotationBy90Degree(movement.ToString().ToLower()));
                    }
                    else
                    {
                        robot.Movements.Add(new MovementByOneTile(_safetyHandler,gridWidth,gridHeight));
                    }
                }
                robots.Add(robot);
            }
            return robots;
        }
    }
}
