using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AutoChessPlayerLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class PlayerService : IPlayerService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        #region Units
        public List<BaseUnitPackage> RequestRandomUnitsFromPool(int playerLevel, int count)
        {
            return UnitsPool.GenerateUnits(playerLevel, count);
        }

        public void MoveUnit(string playerUsername, FigurePackage unit)
        {
            PlayerPackage player = MatchManager.PlayerWithUsername(playerUsername);
            FigurePackage figure = player.Figures.Find(f => { return f.NewRow == unit.OriginalRow && f.NewColumn == unit.OriginalColumn; });

            player.Figures.Remove(figure);
            if (unit.NewRow != -1)
                player.Figures.Add(unit);
        }

        public void ReturnUnitToPool(BaseUnitPackage unit)
        {
            UnitsPool.PutUnitInPool(unit);
        }
        #endregion

        #region Players
        public void RegisterPlayer(PlayerPackage myself)
        {
            MatchManager.RegisterPlayer(myself);
        }

        public List<FigurePackage> GetOpponentsUnits(string myUsername)
        {
            return MatchManager.FindOpponent(MatchManager.PlayerWithUsername(myUsername)).Figures;
        }

        public void TakeDamage(string username, int hp)
        {
            MatchManager.ReducePlayerHP(username, hp);
        }
        #endregion
    }
}
