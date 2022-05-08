
using MartianRobots.Movements;
using System.Collections.Generic;

namespace MartianRobots.Model
{
    public class Robot
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Orientation CurrentOrientation { get; set; }
        public List<IMovement> Movements { get; set; }
    }
}
