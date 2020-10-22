using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AutoChessPlayerLibrary
{
    public class MessageEventArgs : EventArgs
    {
        public string Sender;
        public string Message;
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class DuplexChatService : IDuplexChatService
    {
        Dictionary<IDuplexChatClient, string> _users = new Dictionary<IDuplexChatClient, string>();
        public delegate void MessageSent(object sender, MessageEventArgs e);
        public static event MessageSent OnMessageSent;

        IDuplexChatClient callback = null;

        MessageSent messageSent = null;

        public void Join(string username)
        {
            callback = OperationContext.Current.GetCallbackChannel<IDuplexChatClient>();
            _users[callback] = username;

            messageSent = new MessageSent(MessageSentHandler);
            OnMessageSent += messageSent;
        }

        public void SendMessage(string message)
        {
            callback = OperationContext.Current.GetCallbackChannel<IDuplexChatClient>();

            string username;
            if (!_users.TryGetValue(callback, out username))
                return;

            MessageEventArgs e = new MessageEventArgs();
            e.Message = message;
            e.Sender = username;
            OnMessageSent(this, e);
        }

        public void MessageSentHandler(object sender, MessageEventArgs e)
        {
            callback.ReciveMessage(e.Sender, e.Message);
        }

        public void Unsubscribe()
        {
            callback = OperationContext.Current.GetCallbackChannel<IDuplexChatClient>();
            _users.Remove(callback);
            OnMessageSent -= messageSent;
        }
    }
}
