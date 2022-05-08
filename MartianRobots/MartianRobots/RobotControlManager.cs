using MartianRobots.Input;
using MartianRobots.Model;
using MartianRobots.Movements;
using System;
using System.Collections.Generic;

namespace MartianRobots
{
    public class RobotControlManager
    {
        private readonly IInputReader _inputReader;
        protected ExecutionInput _input;
        private readonly IRobotMovementSafetyHandler _safetyHandler = null;
        public RobotControlManager(IInputReader inputReader, IRobotMovementSafetyHandler safetyHandler)
        {
            _inputReader = inputReader;
            _safetyHandler = safetyHandler;
        }

        public void ReadData()
        {
            WriteMessage("Enter grid width and height");
            var gridSizeStr = _inputReader.ReadLine();
            if (string.IsNullOrWhiteSpace(gridSizeStr))
            {
                WriteMessage("grid size must be entered correctly");
                throw new ArgumentException("Wrong grid size");
            }
            var gridSizeArray = gridSizeStr.Split(' ');
            if (gridSizeArray.Length != 2)
            {
                WriteMessage("Grid size must be entered like {width} {height}");
                throw new ArgumentException("Wrong grid size");
            }

            var gridWidthStr = gridSizeArray[0];
            var gridHeightStr = gridSizeArray[1];

            var gridWidth = 0;
            var gridHeight = 0;

            if (!int.TryParse(gridWidthStr, out gridWidth) ||
                !int.TryParse(gridHeightStr, out gridHeight))
            {
                WriteMessage("Grid size must consist integer");
                throw new ArgumentException("Invalid grid size");
            }

            if (gridWidth < 0 || gridHeight < 0)
            {
                WriteMessage("Grid size must consist positive integers");
                throw new ArgumentException("Invalid grid size");
            }

            if (gridWidth > 50 || gridHeight > 50)
            {
                WriteMessage("Grid width and height should not exceed 50 tiles");
                throw new ArgumentException("Invalid grid size");
            }
            gridWidth++;
            gridHeight++;
            WriteMessage("Enter initial robot and movement data. Enter / to start process");
            var robotData = new List<RobotDataInput>();

            var index = 0;
            while (true)
            {
                WriteMessage($"Enter initial position and movement data for Robot {++index}");
                var positionStr = _inputReader.ReadLine();

                if (string.IsNullOrWhiteSpace(positionStr))
                {
                    WriteMessage("Initial position for robot can't be empty");
                    throw new ArgumentException("Wrong robot position");
                }
                var positionArray = positionStr.Split(' ');
                if (positionArray.Length != 3)
                {
                    WriteMessage("Robot position must be entered like {X} {Y} {Orientation}");
                    throw new ArgumentException("Wrong robot position");
                }

                var positionXStr = positionArray[0];
                var positionYStr = positionArray[1];

                var x = 0;
                var y = 0;
                if (!int.TryParse(positionXStr, out x) ||
                    !int.TryParse(positionYStr, out y))
                {
                    WriteMessage("Robot position must consist integers");
                    throw new ArgumentException("Invalid robot position");
                }

                var initialOrientation = positionArray[2];

                if (x < 0 || y < 0)
                {
                    WriteMessage("Position must consist positive integers");
                    throw new ArgumentException("Invalid robot position");
                }

                var movementData = _inputReader.ReadLine();
                if (string.IsNullOrWhiteSpace(movementData))
                {
                    WriteMessage("Movement data for robot can't be empty");
                    throw new ArgumentException("Wrong robot movement data");
                }

                if (movementData.Length > 100)
                {
                    WriteMessage("Movement data string should not be more than 100");
                    throw new ArgumentException("Wrong robot movement data");
                }

                robotData.Add(new RobotDataInput()
                {
                    StartX = x,
                    StartY = y,
                    MovementData = movementData,
                    InitialOrientation = ConvertToOrientation(initialOrientation)
                });

                var next = _inputReader.ReadLine();
                if (next == "/")
                {
                    break;
                }
            }

            _input = new ExecutionInput()
            {
                GridWidth = gridWidth,
                GridHeight = gridHeight,
                RobotsData = robotData
            };
        }

        public void Execute()
        {
            var robots = RobotMovementFactory.CreateRobotData(_input.GridWidth, _input.GridHeight, _input.RobotsData, _safetyHandler);
            foreach (var robot in robots)
            {   
                var failed = false;
                foreach (var movement in robot.Movements)
                {
                    var result = movement.MoveRobot(robot);
                    if (!result.Success)
                    {
                        failed = true;
                        break;
                    }
                }
                Console.WriteLine($"{robot.X} {robot.Y} {robot.CurrentOrientation} {(!failed ? "" : "LOST")}");
            }
        }

        private void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }

        private Orientation ConvertToOrientation(string orientationStr)
        {
            if (string.IsNullOrWhiteSpace(orientationStr))
            {
                WriteMessage("Initial orientation for robot can't be empty");
                throw new ArgumentException("Wrong robot orientation");
            }
            switch (orientationStr.ToLower())
            {
                case "e":
                    return Orientation.East;
                case "w":
                    return Orientation.West;
                case "n":
                    return Orientation.North;
                case "s":
                    return Orientation.South;
                default:
                    WriteMessage("Initial orientation for robot must be one of e,s,w,n");
                    throw new ArgumentException("Wrong robot orientation");
            }
        }
    }
}
