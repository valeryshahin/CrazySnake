using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace CrazySnake
{
    public class Snake : IEnumerable<Cell>, IRunnable
    {
        private List<Cell> snake;
        private Direction direct;
        private GameStatus gameStatus;
        private Thread threadSnake;

        public Snake()
        {
            // последний элемент листа - голова
            snake = new List<Cell>() 
            {
                new Cell(3,2),
                new Cell(4,2),
                new Cell(5,2),
            };

            this.direct = Direction.Right;
            this.gameStatus = GameStatus.Stopping;
        }

        public Snake(List<Cell> snake)
        {
            this.snake = snake;
            this.direct = Direction.Right;
        }

        public Direction Direct
        {
            get { return this.direct; }
            set { this.direct = value; }
        }

        /// <summary>
        /// Перемещение змейки за 1 игровой такт
        /// </summary>
        private void Move()
        {
            MoveBody();
            MoveHead();
        }

        /// <summary>
        /// при движении змейки за 1 игровой такт, она перемещается таким образом, что
        /// координата предыдущего элемента станет равной координате следующего элемента:
        /// кроме головы, она может поменять направление в зависимости от действий пользователя
        /// </summary>
        private void MoveBody()
        {
            for (int i = 0; i < this.snake.Count - 1; i++)
            {
                this.snake[i].X = this.snake[i + 1].X;
                this.snake[i].Y = this.snake[i + 1].Y;
            }
        }

        private void MoveHead()
        {
            if (this.direct == Direction.Right)
                MoveHeadRight();
            else if (this.direct == Direction.Left)
                MoveHeadLeft();
            else if (this.direct == Direction.Up)
                MoveHeadUp();
            else if (this.direct == Direction.Down)
                MoveHeadDown();
            else
                throw new System.ArgumentException("Не корректное направление");

        }

        private void MoveHeadRight()
        {
            this.snake.Last().X++;
        }

        private void MoveHeadLeft()
        {
            this.snake.Last().X--;
        }

        private void MoveHeadUp()
        {
            this.snake.Last().Y--;
        }

        private void MoveHeadDown()
        {
            this.snake.Last().Y++;
        }

        private bool MeetApple(Apple apple)
        {
            Cell head = this.snake.Last();
            if (head.X == apple.X && head.Y == apple.Y)
                return true;

            return false;
        }

        private bool ClosureSnake()
        {
            Cell head = this.snake.Last();
            for (int i = 0; i < this.snake.Count - 1; i++)
            {
                if (Math.Abs(head.X) == Math.Abs(snake[i].X) && Math.Abs(head.Y) == Math.Abs(snake[i].Y))
                    return true;
            }
            return false;
        }

        private bool OutBound()
        {
            Cell head = this.snake.Last();
            if (head.Y * Setting.S <= 0 || head.Y * Setting.S >= (Setting.Height - 10)
                || head.X * Setting.S <= 0 || head.X * Setting.S >= (Setting.Width - 10))
                return true;
            return false;
        }

        private void GrowSnake()
        {
            Cell tail = this.snake.First();
            if (this.Direct == Direction.Right)
                this.snake.Insert(0, new Cell(tail.X - 1, tail.Y));
            else if (this.Direct == Direction.Left)
                this.snake.Insert(0, new Cell(tail.X + 1, tail.Y));
            else if (this.Direct == Direction.Down)
                this.snake.Insert(0, new Cell(tail.X, tail.Y - 1));
            else if (this.Direct == Direction.Up)
                this.snake.Insert(0, new Cell(tail.X, tail.Y + 1));
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            return this.snake.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < this.snake.Count; i++)
            {
                yield return this.snake[i];
            }
        }

        public void Run(object gameParameter)
        {
            this.gameStatus = GameStatus.Playing;
            this.threadSnake = new Thread(RunSnake);
            threadSnake.Start(gameParameter);
        }

        private void RunSnake(object gameParameter)
        {
            List<Apple> apples = gameParameter as List<Apple>;

            while (gameStatus == GameStatus.Playing)
            {
                this.Move();
                // не удаляем яблоко, а перемещаем в другое место
                //apples.ForEach(a => 
                //{
                //    if (this.MeetApple(a))
                //    {
                //        this.GrowSnake();
                //        a.ChangePosition();
                //        Score.Result += 10;
                //    }                
                //});

                // удаляем яблоко, убиваем поток для этого яблока
                for (int i = 0; i < apples.Count; i++)
                {
                    if (this.MeetApple(apples[i]))
                    {
                        this.GrowSnake();
                        apples[i].Stop();
                        apples.Remove(apples[i]);
                        Score.Result += 10;
                    }
                }

                    if (this.ClosureSnake())
                    {
                        gameStatus = GameStatus.GameOver;
                        apples.ForEach(a => a.Stop());
                    }
                if (this.OutBound())
                {
                    gameStatus = GameStatus.GameOver;
                    apples.ForEach(a => a.Stop());
                }
                Thread.Sleep(Setting.SpeedGame);
            }
        }

        public void Stop()
        {
            this.gameStatus = GameStatus.Stopping;
            this.threadSnake.Abort();
        }
    }
}
