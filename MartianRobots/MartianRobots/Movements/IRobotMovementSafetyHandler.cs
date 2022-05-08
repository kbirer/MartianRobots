using MartianRobots.Model;

namespace MartianRobots.Movements
{
    public interface IRobotMovementSafetyHandler
    {
        bool IsSafeToMove(int x, int y, Orientation orientation);
        void AddFailedMove(int x, int y, Orientation orientation);
    }
}
