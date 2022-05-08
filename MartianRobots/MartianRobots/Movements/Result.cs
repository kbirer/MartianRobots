using MartianRobots.Model;

namespace MartianRobots.Movements
{
    public class Result
    {
        public bool Success { get; set; }
        public FailedMovementScent Scent { get; set; }

        public static Result SuccessfullResult
        {
            get
            {
                return new Result()
                {
                    Success = true
                };
            }
        }

        public static Result FailedResult(Robot robot)
        {
            return new Result()
            {
                Success = false,
                Scent=new FailedMovementScent()
                {
                    FailedDirection = robot.CurrentOrientation,
                    X = robot.X,
                    Y = robot.Y
                }
            };
        }
    }
}
