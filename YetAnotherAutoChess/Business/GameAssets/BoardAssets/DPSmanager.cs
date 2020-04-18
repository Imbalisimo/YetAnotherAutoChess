using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.GameAssets.BoardAssets
{
    public class DPSmanager : GameObject
    {
        public List<Figure> AllyFigures;
        public List<Figure> EnemyFigures;
        //public Text DpsText;

        private string text;

        // Start is called before the first frame update
        void Start()
        {
            AllyFigures = new List<Figure>();
            EnemyFigures = new List<Figure>();
            MatchManager.Instance.OnStateChage += state => { if (state == Enums.MatchState.Battle) Reset(); };
        }

        // Update is called once per frame
        public override void Update()
        {
            
        }

        private void UpdateDPS(Figure item)
        {
            if (!AllyFigures.Contains(item))
                AllyFigures.Add(item);

            // Default List.Sort for less items than 16 is insertion sort
            AllyFigures.Sort((p, n) => { return p.DPS - n.DPS; });

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (Figure figure in AllyFigures)
                sb.AppendLine(figure.Unit.GetType().Name + ": " + figure.DPS.ToString());

            text = sb.ToString();
            //DpsText.text = text;
        }

        private void Reset()
        {
            AllyFigures.Clear();
            EnemyFigures.Clear();
        }

        void Awake()
        {
            SetupSingleton();
        }

        #region Singleton
        public static DPSmanager Instance;
        /// <summary>
        /// Creates a single instance of this object
        /// </summary>
        private void SetupSingleton()
        {
            if (DPSmanager.Instance != null && DPSmanager.Instance != this)
            {
                Destroy();
                return;
            }

            DPSmanager.Instance = this;
        }
        #endregion

        public void SetEnemyFigures(List<Figure> enemyFigures)
        {
            foreach (Figure figure in enemyFigures)
            {
                this.EnemyFigures.Add(figure);
            }
        }

        internal void SetAllyFigures(List<Figure> allyFigures)
        {
            foreach (Figure figure in allyFigures)
            {
                this.EnemyFigures.Add(figure);
            }
        }
    }
}
