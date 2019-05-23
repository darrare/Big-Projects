using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portfolio.Models;

namespace Portfolio.RealTimeBackend
{
    public static class ClientManager
    {
        static List<Client> ClientQueue = new List<Client>();

        //The following can be optimized by storing it on insertion/deletion
        public static int TankCount { get { return ClientQueue.Sum(t => t.Model.Role == Enums.Role.Tank ? 1 : 0); } }

        public static int HealerCount { get { return ClientQueue.Sum(t => t.Model.Role == Enums.Role.Healer ? 1 : 0); } }

        public static int DamageCount { get { return ClientQueue.Sum(t => t.Model.Role == Enums.Role.Damage ? 1 : 0); } }


        public static void AddClient(Client c)
        {
            ClientQueue.Add(c);
        }

        public static void RemoveClientIfExists(string guid)
        {
            Client c;
            if ((c = ClientQueue.FirstOrDefault(t => t.GUID == guid)) != null)
            {
                ClientQueue.Remove(c);
            }
        }

        public static bool CanFormTeam()
        {
            return ClientQueue.Count(t => t.Model.Role == Enums.Role.Tank) >= 1
                && ClientQueue.Count(t => t.Model.Role == Enums.Role.Healer) >= 1
                && ClientQueue.Count(t => t.Model.Role == Enums.Role.Damage) >= 3;
        }

        public static List<Client> FormTeam()
        {
            List<Client> newTeam = new List<Client>();
            newTeam.Add(ClientQueue.First(t => t.Model.Role == Enums.Role.Tank));
            newTeam.Add(ClientQueue.First(t => t.Model.Role == Enums.Role.Healer));
            newTeam.AddRange(ClientQueue.Where(t => t.Model.Role == Enums.Role.Damage).Take(3));
            foreach (Client c in newTeam)
            {
                ClientQueue.Remove(c);
            }
            return newTeam;
        }
    }
}
