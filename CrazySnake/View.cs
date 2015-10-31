using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CrazySnake
{
    public partial class View : UserControl
    {
        private Model model;
        private Label scoreLabel;

        public View(Model model, Label scoreLabel)
        {
            InitializeComponent();
            this.model = model;
            this.scoreLabel = scoreLabel;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawApple(e);
            DrawSnake(e);
            ShowScore();
            if (model.gameStatus != GameStatus.Playing)
                return;
            Thread.Sleep(Setting.SpeedGame);
            Invalidate();
        }

        private void DrawSnake(PaintEventArgs e)
        {
            SolidBrush sb = new SolidBrush(Color.Brown);
            foreach (var cell in model.snake)
            {
                e.Graphics.FillRectangle(sb, new Rectangle(cell.X * Setting.S, cell.Y * Setting.S, Setting.S, Setting.S));
            }
        }

        private void DrawApple(PaintEventArgs e)
        {
            SolidBrush sb = new SolidBrush(Color.Green);
            foreach (var a in model.apples)
            {
                e.Graphics.FillRectangle(sb, new Rectangle(a.X * Setting.S, a.Y * Setting.S, Setting.S, Setting.S));
            }
        }

        private void ShowScore()
        {
            this.scoreLabel.Text = Score.Result.ToString();
        }
    }
}
