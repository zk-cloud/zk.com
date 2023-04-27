using CoreCms.Net.Model.FromDate;
using System.Collections.Generic;
using System.Linq;

namespace CoreCms.Net.Utility.YLQCHelper
{
    public class WebsocketClientCollection
    {
        private static List<FMSocketModel> _clients = new List<FMSocketModel>();

        public static void Add(FMSocketModel client)
        {
            _clients.Add(client);
        }

        public static void Remove(FMSocketModel client)
        {
            _clients.Remove(client);
        }

        public static FMSocketModel Get(string clientId)
        {
            var client = _clients.FirstOrDefault(c => c.Id == clientId);

            return client;
        }

        public static List<FMSocketModel> GetRoomClients(string roomNo)
        {
            var client = _clients.Where(c => c.RoomNo == roomNo);
            return client.ToList();
        }
    }
}
