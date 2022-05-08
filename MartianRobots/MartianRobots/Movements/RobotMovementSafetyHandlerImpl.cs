using MartianRobots.Model;
using System;
using System.Collections.Generic;

namespace MartianRobots.Movements
{
    public class RobotMovementSafetyHandlerImpl : IRobotMovementSafetyHandler
    {
        private Dictionary<Tuple<int, int>, List<Orientation>> _failedMovements = new Dictionary<Tuple<int, int>, List<Orientation>>();

        public void AddFailedMove(int x, int y, Orientation orientation)
        {
            var key = new Tuple<int, int>(x, y);
            if (_failedMovements.ContainsKey(key))
            {
                var value = _failedMovements[key];
                if (!value.Contains(orientation))
                {
                    value.Add(orientation);
                }
            }
            else
            {
                _failedMovements[key] = new List<Orientation>() { orientation };
            }
        }

        public bool IsSafeToMove(int x, int y, Orientation orientation)
        {
            var key = new Tuple<int, int>(x, y);
            if (!_failedMovements.ContainsKey(key))
            {
                return true;
            }
            var directions = _failedMovements[key];
            return !directions.Contains(orientation);
        }
    }
}
