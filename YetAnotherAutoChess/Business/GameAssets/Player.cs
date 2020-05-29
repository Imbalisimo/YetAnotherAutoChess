using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.GameAssets
{
    public class Player : GameObject
    {
        public bool LastMatchWon = true;
        public int WinningOrLoosingStrike;

        private string _name;
        public string Name { get => _name; set => _name = value; }

        public string DisplayDataString;

        //private PlayerHPManager _playerHPManager;

        public int Pawns;

        public int Health;

        public Player()
        {
            SetupSingleton();

            Health = 100;
            Pawns = 5;
            Board.OnMatchEnd += damage => TakeDamage(damage);
            PlayerClient.RegisterPlayer(ToPlayerPackage());

            MatchManager.Instance.OnStateChage += state =>
            {
                if (state != Enums.MatchState.Preparation)
                    return;

                if (Pawns % 10 > 5)
                {
                    Pawns += 5;
                }
                else
                {
                    Pawns = Pawns + (Pawns % 10);
                }

                Pawns += 5;

                switch (WinningOrLoosingStrike)
                {
                    case 1:
                        break;
                    case 2:
                    case 3:
                        ++Pawns;
                        break;
                    case 4:
                    case 5:
                        Pawns += 2;
                        break;
                    default:
                        Pawns += 3;
                        break;


                }
            };
        }


        #region Singleton
        public static Player Instance;
        /// <summary>
        /// Creates a single instance of this object
        /// </summary>
        private void SetupSingleton()
        {
            if (Player.Instance != null && Player.Instance != this)
            {
                Destroy();
                return;
            }

            Player.Instance = this;
        }
        #endregion

        public void AttributePlayerDisplay(string DName)
        {
            DisplayDataString = DName;
            //Change the name on display
            //PlayerHPManager playerHPManager = DisplayData.GetComponent<PlayerHPManager>();
            //playerHPManager.SetName(DisplayData, Name);
        }

        void ChangeName(string name)
        {
            Name = name;
        }

        void TakeDamage(int damage)
        {
            UpdateHealth(this.Health - damage);
            PlayerClient.TakeDamage(Name, damage);
        }

        void UpdateHealth(int setHealth)
        {
            LastMatchIsWon(Health == setHealth ? true : false);
            if (setHealth < 0)
            {
                this.Health = 0;
            }
            Health = setHealth;

            //playerHPManager.SetHealth(this.Health);
        }

        void UpdatePawnsNET(int setPawns)
        {
            this.Pawns = setPawns;

            //GameObject PawnsGameObject = DisplayData.transform.Find("GoldPieces").gameObject;
            //Text PawnsText = PawnsGameObject.GetComponent<Text>();

            //PawnsText.text = setPawns.ToString();
        }

        void LastMatchIsWon(bool lastMatchWon)
        {
            if (LastMatchWon != lastMatchWon)
            {
                WinningOrLoosingStrike = 1;
            }
            else
            {
                WinningOrLoosingStrike += 1;
            }
        }

        public PlayerServiceReference.PlayerPackage ToPlayerPackage()
        {
            PlayerServiceReference.PlayerPackage player = new PlayerServiceReference.PlayerPackage();
            player.Hp = Health;
            player.Money = Pawns;
            player.Username = Name;
            //player.Password = password                           PASSWORD NEEDS TO BE REMOVED!!!!!!!!!!!!!!
            player.IP_adress = NetworkInformation.IPAddress;
            player.Port = NetworkInformation.Port.ToString();   // PORT NEEDS TO BE INTEGER!!!!!!!!!!
            player.Figures = Board.CopyActiveFigures().ToArray();
            return player;
        }

        public override void Update()
        {
            
        }
    }
}
