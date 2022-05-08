using MartianRobots.Movements;

namespace MartianRobots
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var inputReader = new ConsoleInputReader();
            var safetyHandler = new RobotMovementSafetyHandlerImpl();
            var controlManager = new RobotControlManager(inputReader, safetyHandler);
            controlManager.ReadData();
            controlManager.Execute();
        }
    }
}
