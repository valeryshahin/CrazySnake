using System;
using System.Threading;
using System.Linq;
namespace CrazySnake
{
    public class Apple : Cell, IRunnable
    {
        private Random rand;
        private Thread appleThread;
        private Direction Direct;
        private GameStatus gameStatus;

        public Apple(int x, int y): base(x, y)
        {
            this.rand = new Random();
            this.Direct = (Direction)rand.Next(0, 3);
        }

        public void ChangePosition()
        {
            this.X = rand.Next(2,Setting.Width / 20);
            this.Y = rand.Next(2,Setting.Height / 20);
        }

        public void Run(object gameParameter)
        {
            this.gameStatus = GameStatus.Playing;
            this.appleThread = new Thread(RunApple);
            appleThread.Start(gameParameter);
        }

        public void Stop()
        {
            this.gameStatus = GameStatus.Stopping;
            this.appleThread.Abort();
        }

        private void RunApple(object gameParameter)
        {
            Snake snake = gameParameter as Snake;

            int speedApple = Setting.SpeedGame;
            while (this.gameStatus == GameStatus.Playing)
            {
                this.Move();
                Thread.Sleep(speedApple);
            }
        }

        private void Move()
        {
            bool nearWall = false;
            if (this.NearWall())
            {
                this.TurnApple();
                nearWall = true;
                //return;// если надо останавливать яблоки у границ игрового поля
            }
            if (!nearWall && ChangeDirection())
                this.Direct = RandomProvider.RandomDirect();
            if (Direct == Direction.Right)
                this.X++;
            else if (Direct == Direction.Left)
                this.X--;
            else if (Direct == Direction.Up)
                this.Y--;
            else if (Direct == Direction.Down)
                this.Y++;
        }

        private bool NearWall()
        {
            if (Y * Setting.S <= 0 || Y * Setting.S >= (Setting.Height - 10)||
                X * Setting.S <= 0 || X * Setting.S >= (Setting.Width - 10))
                return true;

            return false;
        }

        private void TurnApple()
        {
            if (this.Direct == Direction.Right)
                this.Direct = Direction.Left;
            else if (this.Direct == Direction.Left)
                this.Direct = Direction.Right;
            else if (this.Direct == Direction.Up)
                this.Direct = Direction.Down;
            else if (this.Direct == Direction.Down)
                this.Direct = Direction.Up;
        }

        private bool NearSnake(Cell head)
        {
            double x = Math.Abs(Math.Abs(head.X) - Math.Abs(X));
            double y = Math.Abs(Math.Abs(head.Y) - Math.Abs(Y));
            double distanceForSnake = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            if (distanceForSnake < 30)
                return true;

            return false;
        }

        private Direction DirectionFromSnake(Direction snakeDirect)
        {
            Direction newDirection = Direction.Up;
            if (X > Y && (snakeDirect == Direction.Left || snakeDirect == Direction.Right))
            {
                if (X > 30 && snakeDirect == Direction.Left)
                    newDirection = Direction.Left;
                else
                    newDirection = Direction.Right;
            }
            else
            {
                if (Y > 22 && snakeDirect == Direction.Up)
                    newDirection = Direction.Up;
                else
                    newDirection = Direction.Down;
            }

            return newDirection;
        }

        private bool ChangeDirection()
        {
            int randNumber = rand.Next(0,10);

            if (randNumber > 4)
                return true;

            return false;
        }
    }
}
