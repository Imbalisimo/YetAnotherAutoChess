using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoChessPlayerLibrary
{
    [DataContract]
    public class PlayerPackage
    {
        private string _username;
        private string _password;
        private string _IP_adress;
        private string _port;

        private int _hp;
        private int _money;

        private List<FigurePackage> _figures = new List<FigurePackage>();

        [DataMember]
        public string Username { get => _username; set => _username = value; }
        [DataMember]
        public string Password { get => _password; set => _password = value; }
        [DataMember]
        public string IP_adress { get => _IP_adress; set => _IP_adress = value; }
        [DataMember]
        public string Port { get => _port; set => _port = value; }
        [DataMember]
        public int Hp { get => _hp; set => _hp = value; }
        [DataMember]
        public int Money { get => _money; set => _money = value; }
        [DataMember]
        public List<FigurePackage> Figures { get => _figures; set => _figures = value; }
    }
}
