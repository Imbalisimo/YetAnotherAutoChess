using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace YetAnotherAutoChess
{
    class StatusBarInfo
    {
        private float _currentHealthValue;
        private float _currentManaValue;
        public float CurrentHealth { get { return _currentHealthValue; } }
        public float CurrentMana { get { return _currentManaValue; } }
        public Canvas canvas { get; }
        private ProgressBar HealthBar;
        private ProgressBar ManaBar;
        public Image Stars;
        public StatusBarInfo(double PositionLeft_To_Right = 100, double PositionBottom_To_Top = 400, double healthBar = 100, double manaBar = 100, int numOfStars = 0)
        {
            canvas = (Application.Current.Resources["unitStatusBar"] as Canvas);
            setCanvasPosition(PositionLeft_To_Right, PositionBottom_To_Top);
            SetChildren();
            SetHealth(100);
            SetMana(100);
        }
        public void SetHealth(int healthBar = 100)
        {
            if (healthBar != CurrentHealth) 
            {
                HealthBar.Value = healthBar;
            }
        }

        public void SetMana(int manaBar)
        {
            if (manaBar != CurrentMana)
            {
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
                if (child is ProgressBar progress)
                {
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
}
