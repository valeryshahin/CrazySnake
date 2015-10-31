using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CrazySnake
{
    public partial class ControllerForm : Form
    {
        private View view;
        private Model model;

        public ControllerForm()
        {
            InitializeComponent();

            model = new Model();
            this.view = new View(model, lblScoreValue);
            this.Controls.Add(view);
            Setting.Height = this.view.Height;
            Setting.Width = this.Width;
        }

        private void StartStop_btn_Click(object sender, EventArgs e)
        {
            if (model.gameStatus == GameStatus.Playing)
            {
                model.gameStatus = GameStatus.Stopping;
                model.snake.Stop();
                model.apples.ForEach(a => a.Stop());
            }
            else
            {
                model.gameStatus = GameStatus.Playing;
                model.snake.Run(model.apples);
                model.apples.ForEach(a => a.Run(model.snake));
                view.Invalidate(); // вызывется для, первоначального вызова события OnPaint() для класса View
            }
        }

        private void StartStop_btn_KeyPress(object sender, KeyPressEventArgs e)
        {
            string buttonPress = e.KeyChar.ToString();
            switch (buttonPress)
            {
                case "w":
                    {
                        if (model.snake.Direct != Direction.Down)
                            model.snake.Direct = Direction.Up;
                    }
                    break;
                case "s":
                    {
                        if (model.snake.Direct != Direction.Up)
                            model.snake.Direct = Direction.Down;
                    }
                    break;
                case "a":
                    {
                        if (model.snake.Direct != Direction.Right)
                            model.snake.Direct = Direction.Left;
                    }
                    break;
                case "d":
                    {
                        if (model.snake.Direct != Direction.Left)
                            model.snake.Direct = Direction.Right;
                    }
                    break;
            }
        }
    }
}
