using System;
using System.Collections.Generic;
//using System.Linq;
using System.Threading;

namespace CrazySnake
{
    public class Model
    {
        public Snake snake;
        public GameStatus gameStatus;
        private Random rnd;
        public List<Apple> apples;
        public Model()
        {
            this.gameStatus = GameStatus.Stopping;
            this.rnd = new Random();
            this.snake = new Snake();
            this.apples = new List<Apple>() { new Apple(rnd.Next(2, 29), rnd.Next(2, 34)),
                                              new Apple(rnd.Next(2, 29), rnd.Next(2, 34)),
                                              new Apple(rnd.Next(2, 29), rnd.Next(2, 34)),
                                              new Apple(rnd.Next(2, 29), rnd.Next(2, 34)),
                                              new Apple(rnd.Next(2, 29), rnd.Next(2, 34)),
                                              new Apple(rnd.Next(2, 29), rnd.Next(2, 34))};
        }
    }
}
