using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AutoChessPlayerLibrary
{
    public interface IDuplexChatClient
    {
        [OperationContract(IsOneWay = true)]
        void ReciveMessage(string user, string message);
    }

    [ServiceContract(SessionMode = SessionMode.Required,
          CallbackContract = typeof(IDuplexChatClient))]
    interface IDuplexChatService
    {
        [OperationContract(IsOneWay = true)]
        void Join(string username);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message);
    }
}
