using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.GameAssets
{
    public class MatchManager : GameObject
    {
        public Player Player;

        public MatchManager()
        {
            SetupSingleton();

            _stateStartTime = DateTime.Now;
            MatchState = Enums.MatchState.Preparation;
            _statesDuration = new List<int>();
            foreach (Enums.MatchState state in System.Enum.GetValues(typeof(Enums.MatchState)))
                _statesDuration.Add(0);
            _statesDuration[(int)Enums.MatchState.Preparation] = 30;
            _statesDuration[(int)Enums.MatchState.Disposal] = 1;
            _statesDuration[(int)Enums.MatchState.Blessing] = 2;
            _statesDuration[(int)Enums.MatchState.Duplication] = 2;
            _statesDuration[(int)Enums.MatchState.Stretching] = 1;
            _statesDuration[(int)Enums.MatchState.Battle] = 60;

            _round = 1;

            Player = Player.Instance;
        }

        #region Singleton
        public static MatchManager Instance;
        /// <summary>
        /// Creates a single instance of this object
        /// </summary>
        private void SetupSingleton()
        {
            if (MatchManager.Instance != null && MatchManager.Instance != this)
            {
                Destroy();
                return;
            }

            MatchManager.Instance = this;
        }
        #endregion

        //public Text Text;

        private int _round;
        public int Level { get => _round > 38 ? 10 : (_round <= 3 ? _round : (_round - 3) / 5 + 3); }
        public int Round { get => _round; }

        private DateTime _stateStartTime;
        private List<int> _statesDuration;

        public Enums.MatchState MatchState;

        public delegate void StateChange(Enums.MatchState matchState);
        public event StateChange OnStateChage;

        private void MatchPlayers()
        {
            if (Round < 3 || (Round - 3) % 5 == 0)
            {
                SpawnWave();
                return;
            }
            //List<Player> players = MatchMaking.GenerateMatches(Players);
            //for (int i = 0; i < players.Count - 1; ++i)
            //    players[i + 1].BoardManager.SpawnEnemyFigures(players[i].BoardManager.CopyActiveFigures());
            //players[0].BoardManager.SpawnEnemyFigures(players[players.Count - 1].BoardManager.CopyActiveFigures());
        }

        private void SpawnWave()
        {
            List<Figure> wave = new List<Figure>();

            switch (Round)
            {
                case 1:
                    //Round1Wave(wave);
                    break;
                case 2:
                    //Round2Wave(wave);
                    break;
                case 3:
                    //Round3Wave(wave);
                    break;
                case 8:
                case 13:
                case 18:
                case 23:
                case 28:
                case 33:
                case 38:
                case 43:
                case 48:
                    break;
            }

            foreach (Figure figure in wave)
            {
                figure.CarryEnemyColors();
                figure.Owner = "admin";
            }

            Board.SpawnEnemyFigures(wave);
        }

        private void Round1Wave(List<Figure> wave)
        {
            wave.Add(CreateFigureFromUnitName("EarthSmallGolem", Enums.Piece.Pawn, 2, 4));
        }

        private void Round2Wave(List<Figure> wave)
        {
            wave.Add(CreateFigureFromUnitName("EarthBigGolem", Enums.Piece.Pawn, 2, 3));
        }

        private void Round3Wave(List<Figure> wave)
        {
            wave.Add(CreateFigureFromUnitName("EarthSmallGolem", Enums.Piece.Pawn, 2, 4));

            wave.Add(CreateFigureFromUnitName("EarthBigGolem", Enums.Piece.Pawn, 2, 3));
        }

        private Figure CreateFigureFromUnitName(string name, Enums.Piece piece, int row, int column)
        {
            Unit unit = FigureManager.CreateUnit(name);
            unit.MainViewModel.SetActive(true);
            Figure figure = FigureManager.CreateFigure(unit, piece);

            figure.Position.Row = row;
            figure.Position.Column = column;

            return figure;
        }

        private void ChangeState()
        {
            switch (MatchState)
            {
                case Enums.MatchState.Preparation:
                    MatchState = Enums.MatchState.Disposal;
                    break;
                case Enums.MatchState.Disposal:
                    MatchState = Enums.MatchState.Blessing;
                    break;
                case Enums.MatchState.Blessing:
                    MatchState = Enums.MatchState.Duplication;
                    MatchPlayers();
                    break;
                case Enums.MatchState.Duplication:
                    MatchState = Enums.MatchState.Stretching;
                    break;
                case Enums.MatchState.Stretching:
                    MatchState = Enums.MatchState.Battle;
                    break;
                case Enums.MatchState.Battle:
                    MatchState = Enums.MatchState.Preparation;
                    ++_round;
                    if (!UnitShop.ShopLocked)
                        UnitShop.Reroll();
                    break;
            }
            OnStateChage(MatchState);
        }
        public override void Update()
        {
            if(_stateStartTime == new DateTime())
                _stateStartTime = DateTime.Now;

            if (Time.TimeDifferenceFromNowInSeconds(_stateStartTime) >= _statesDuration[(int)MatchState])
            {
                _stateStartTime = DateTime.Now;
                ChangeState();
            }

            int secondsLeft = (int) (_statesDuration[(int)MatchState] - Time.TimeDifferenceFromNowInSeconds(_stateStartTime));

            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

            stringBuilder.Append(MatchState.ToString());
            stringBuilder.Append(" ");
            stringBuilder.Append(Level.ToString());
            stringBuilder.Append("-");
            stringBuilder.Append(Round.ToString());
            stringBuilder.Append(": ");
            stringBuilder.Append(secondsLeft.ToString());

            //Text.text = stringBuilder.ToString();
        }
    }
}
