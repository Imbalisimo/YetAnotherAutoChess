using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace YetAnotherAutoChess
{
    class PlayersUI
    {
        private class PlayerUI
        {
            public ProgressBar HealthBar { get; set; }
            public TextBox PlayerName { get; set; }
            public int Money { get; set; }

            public void UpdateHealth(int damage)
            {
                if (damage == 0) return;
                HealthBar.Value -= damage;
            }

            public void UpdateMoney(int difference)
            {
                if (difference == 0) return;
                Money += difference;
            }

            public void SetName(string Name)
            {
                PlayerName.Text = Name;
            }
        }
        private List<PlayerUI> playerUIs;
        public PlayersUI(Grid MainGrid)
        {
            playerUIs = new List<PlayerUI>(8);
            Grid playerStatusGrid = Application.Current.Resources["PlayersStatus"] as Grid;
            MainGrid.Children.Add(playerStatusGrid);
            //listPlayerNamesProgressBars(playerStatusGrid);

        }

        private void listPlayerNamesProgressBars(Grid playerStatusGrid)
        {
            int barCounter, nameCounter, moneyCounter;
            barCounter = nameCounter = moneyCounter = 0;
            foreach (UIElement child in playerStatusGrid.Children)
            {
                if (child is ProgressBar progressBar)
                {
                    playerUIs[barCounter].HealthBar = progressBar;
                }
                else if (child is TextBox playerName)
                {
                    playerUIs[nameCounter].PlayerName = playerName;
                }
                else if (child is TextBox moneyCounterText)
                {
                    playerUIs[moneyCounter].PlayerName = moneyCounterText;
                }
            }
        }
    }
}