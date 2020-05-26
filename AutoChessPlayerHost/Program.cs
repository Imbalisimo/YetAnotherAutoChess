using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace AutoChessPlayerHost
{
    class Program
    {
        static void Main(string[] args)
        {
            UriBuilder ub = new UriBuilder(GetPublicIP());
            ub.Port = 8733;
            Uri baseAddress = new Uri("http://localhost:8733/"); // For testing purposes
                              //ub.Uri;                            // For deployment purposes

            ServiceHost selfHost = new ServiceHost(typeof(AutoChessPlayerLibrary.PlayerService), baseAddress);

            try
            {
                selfHost.AddServiceEndpoint(
                    typeof(AutoChessPlayerLibrary.IPlayerService),
                    //new WSHttpBinding(),
                    new BasicHttpBinding(),
                    "AutoChessPlayerLibrary/PlayerService"); ;

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                selfHost.Open();
                Console.WriteLine("The service is ready. IP = " + baseAddress.Host);

                Console.WriteLine("Press <Enter> to terminate the service.");
                Console.WriteLine();
                Console.ReadLine();
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
        }

        public static IPAddress GetLocalIPAddress(string hostName)
        {
            Ping ping = new Ping();
            var replay = ping.Send(hostName);

            if (replay.Status == IPStatus.Success)
            {
                return replay.Address;
            }
            return null;
        }

        public static string GetPublicIP()
        {
            string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            return a4;
        }
    }
}
