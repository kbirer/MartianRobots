using MartianRobots.Model;

namespace MartianRobots.Input
{
    public class RobotDataInput
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
        public Orientation InitialOrientation { get; set; }
        public string MovementData { get; set; }
    }
}
