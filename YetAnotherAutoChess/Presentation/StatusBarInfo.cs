using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace YetAnotherAutoChess
{
    class StatusBarInfo
    {
        public Canvas canvas { get; }
        private ProgressBar HealthBar;
        private ProgressBar ManaBar;
        public StatusBarInfo(double PositionLeft_To_Right = 100, double PositionBottom_To_Top = 400, double healthBar = 100, double manaBar = 100, int numOfStars = 0)
        {
            canvas = (Application.Current.Resources["unitStatusBar"] as Canvas);
            setCanvasPosition(PositionLeft_To_Right, PositionBottom_To_Top);
            SetChildren();
            setValues(healthBar,manaBar);
        }
        public void setValues(double healthBar = 100, double manaBar = 100)
        {
            if (healthBar <= 100 && healthBar >= 0 && manaBar >= 0 && manaBar <= 100)
            {
                HealthBar.Value = healthBar;
                ManaBar.Value = manaBar;
            }
        }

        public void setCanvasPosition(double PositionLeft_To_Right = 100, double PositionBottom_To_Top = 100)
        {
            Canvas.SetLeft(canvas, PositionLeft_To_Right);
            Canvas.SetBottom(canvas, PositionBottom_To_Top);
        }

        public void SetChildren()
        {
            foreach (UIElement child in canvas.Children)
            {
                Viewbox viewbox = child as Viewbox;
                ProgressBar progress = viewbox.Child as ProgressBar;
                if (progress.Name == "unitHealthBar")
                {
                    HealthBar = progress;
                }
                else if (progress.Name == "unitManaBar")
                {
                    ManaBar = progress;
                }
            }
        }
    }
}
