using System.Collections.Generic;

namespace MartianRobots.Input
{
    public class ExecutionInput
    {
        public int GridWidth { get; set; }
        public int GridHeight { get; set; }
        public List<RobotDataInput> RobotsData { get; set; }
    }
}
