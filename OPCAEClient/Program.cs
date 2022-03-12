using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCAEClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Opc.URL url = new Opc.URL("opcae://localhost/Kepware.KEPServerEX_AE.V6");
            Opc.Factory factory = new OpcCom.Factory(true);
            Opc.Ae.Server AeServer = new Opc.Ae.Server(factory, url);
            AeServer.Connect();

            Opc.Ae.SubscriptionState subState = new Opc.Ae.SubscriptionState();
            subState.Active = true;
            subState.ClientHandle = Guid.NewGuid().ToString();
            subState.Name = "MyOpcAEClient";
            Opc.Ae.Subscription aeSub = (Opc.Ae.Subscription)AeServer.CreateSubscription(subState);
            aeSub.EventChanged += AeSub_EventChanged;
            Opc.Ae.SubscriptionFilters filters = new Opc.Ae.SubscriptionFilters();
            filters.EventTypes = 7;
            aeSub.SetFilters(filters);
            aeSub.Refresh();
            Console.ReadLine();
        }

        private static void AeSub_EventChanged(Opc.Ae.EventNotification[] notifications, bool refresh, bool lastRefresh)
        {
            foreach (Opc.Ae.EventNotification n in notifications)
            {
                Console.WriteLine(n.SourceID + " " + n.Message);
            }
        }
    }
}
