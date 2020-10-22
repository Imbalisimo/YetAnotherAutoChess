using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoChessPlayerLibrary
{
    public static class MatchMaker
    {
        private static List<PlayerPackage> _previousListOfMatches = new List<PlayerPackage>();

        public static List<PlayerPackage> Shuffle(List<PlayerPackage> ListOfPlayers)
        {
            List<PlayerPackage> listOfMatches = Reshuffle(ListOfPlayers);
            _previousListOfMatches = listOfMatches;
            return listOfMatches;
        }

        public static List<PlayerPackage> Reshuffle(List<PlayerPackage> ListOfPlayers)
        {
            if (ListOfPlayers == null) return null; //Skinuti kasnije

            List<PlayerPackage> coppiedListOfPlayers = new List<PlayerPackage>(ListOfPlayers);
            List<PlayerPackage> listOfMatches = new List<PlayerPackage>();
            int j = 0;

            System.Random random = new System.Random(System.DateTime.Now.Millisecond);
            PlayerPackage player = coppiedListOfPlayers[0];

            if (coppiedListOfPlayers.Count == 2)
                return coppiedListOfPlayers;

            while (coppiedListOfPlayers.Count > 0)
            {
                PlayerPackage enemy;
                PlayerPackage lastEnemy;
                coppiedListOfPlayers.Remove(player);
                if (_previousListOfMatches.Count > 0)
                {
                    if (_previousListOfMatches.Contains(player))
                    {
                        j = _previousListOfMatches.IndexOf(player);
                    }
                    if (_previousListOfMatches.Count == j + 1)
                    {
                        j = -1;
                    }
                    lastEnemy = _previousListOfMatches[j + 1];
                    if (coppiedListOfPlayers.Contains(lastEnemy))
                    {
                        coppiedListOfPlayers.Remove(lastEnemy);
                        enemy = coppiedListOfPlayers[random.Next(coppiedListOfPlayers.Count)];
                        coppiedListOfPlayers.Add(lastEnemy);
                    }
                    else
                    {
                        if (coppiedListOfPlayers.Count == 0)
                        {
                            listOfMatches.Add(player);
                            continue;
                        }
                        enemy = coppiedListOfPlayers[random.Next(coppiedListOfPlayers.Count)];
                    }
                }
                else
                {
                    return ListOfPlayers;
                }
                listOfMatches.Add(player);
                player = enemy;
            }
            return listOfMatches;
        }
    }
}
