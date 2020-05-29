using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using YetAnotherAutoChess.PlayerServiceReference;

namespace YetAnotherAutoChess
{
    public static class PlayerClient
    {
        private static PlayerServiceReference.PlayerServiceClient client;

        public static void Start(string hostIPAdress = "localhost")
        {
            UriBuilder ub = new UriBuilder(hostIPAdress);
            ub.Port = 8733;
            ub.Path = "AutoChessPlayerLibrary/PlayerService";

            //Console.WriteLine("Client started for address: " + ub.Uri.ToString());
            client = new PlayerServiceClient(
                        new BasicHttpBinding(),
                        new EndpointAddress(ub.Uri.ToString()));
            client.Open();
        }

        public static void RegisterPlayer(PlayerPackage player)
        {
            client.RegisterPlayer(player);
        }

        public static List<BaseUnitPackage> RequestRandomUnitsFromPool(int level, int unitCount)
        {
            return new List<BaseUnitPackage>(client.RequestRandomUnitsFromPool(level, unitCount));
        }

        public static void ReturnUnitToPool(BaseUnitPackage unit)
        {
            client.ReturnUnitToPool(unit);
        }

        public static void MoveUnit(FigurePackage figure)
        {
            client.MoveUnit(Business.GameAssets.Player.Instance.Name, figure);
        }

        public static List<FigurePackage> RequestEnemyFigures()
        {
            return new List<FigurePackage>(client.GetOpponentsUnits(Business.GameAssets.Player.Instance.Name));
        }

        public static void TakeDamage(string username, int damage)
        {
            client.TakeDamage(username, damage);
        }

        public static void Close()
        {
            client.Close();
        }
    }
}
