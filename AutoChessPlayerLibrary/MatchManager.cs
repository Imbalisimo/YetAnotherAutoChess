using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoChessPlayerLibrary
{
    public static class MatchManager
    {
        private static List<PlayerPackage> _players = new List<PlayerPackage>();
        private static List<PlayerPackage> _activePlayers = new List<PlayerPackage>();
        private static List<PlayerPackage> _matches;
        private static List<PlayerPackage> _playersWhoDidNotPickedUpOpponent = new List<PlayerPackage>();

        public static List<PlayerPackage> Players { get => _players; set => _players = value; }

        public static void RegisterPlayer(PlayerPackage player)
        {
            _players.Add(player);
            _activePlayers.Add(player);
        }

        public static PlayerPackage PlayerWithUsername(string username)
        {
            return _players.Find(p => { return username == p.Username; });
        }

        public static void ReducePlayerHP(string username, int hp)
        {
            PlayerPackage player = PlayerWithUsername(username);
            if ((player.Hp -= hp) <= 0)
            {
                _activePlayers.Remove(player);
                MatchMaker.Reshuffle(_activePlayers);
            }
        }

        public static PlayerPackage FindOpponent(PlayerPackage player)
        {
            if (_matches == null)
                _matches = MatchMaker.Shuffle(_activePlayers);

            if (_playersWhoDidNotPickedUpOpponent.Count == 0)
            {
                _matches = MatchMaker.Shuffle(_activePlayers);
                _playersWhoDidNotPickedUpOpponent = _activePlayers;
            }
            else
            {
                if (!_playersWhoDidNotPickedUpOpponent.Contains(player))
                {
                    foreach (PlayerPackage p in _playersWhoDidNotPickedUpOpponent)
                        _activePlayers.Remove(p);

                    _matches = MatchMaker.Shuffle(_activePlayers);
                    _playersWhoDidNotPickedUpOpponent = _activePlayers;
                }

            }
            _playersWhoDidNotPickedUpOpponent.Remove(player);
            int index = _matches.IndexOf(player);
            return _matches[++index];
        }
    }
}
