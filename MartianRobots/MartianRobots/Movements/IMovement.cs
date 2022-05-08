using MartianRobots.Model;

namespace MartianRobots.Movements
{
    public interface IMovement
    {
        Result MoveRobot(Robot robot);
    }
}
