using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicLookingForGroup.Models;

namespace ClassicLookingForGroup.RealTimeBackend
{
    public static class ClientManager
    {
        public static List<Client> ClientQueue = new List<Client>();

        //The following can be optimized by storing it on insertion/deletion
        public static int TankCount { get { return ClientQueue.Sum(t => t.Model.Role == Enums.Role.Tank ? 1 : 0); } }

        public static int HealerCount { get { return ClientQueue.Sum(t => t.Model.Role == Enums.Role.Healer ? 1 : 0); } }

        public static int DamageCount { get { return ClientQueue.Sum(t => t.Model.Role == Enums.Role.Damage ? 1 : 0); } }

        static List<Models.Chart.SimpleReportViewModel> allianceChart;
        public static List<Models.Chart.SimpleReportViewModel> AllianceChart { get { if (allianceChart == null) { GenerateAllianceChart(); } return allianceChart; } }

        static List<Models.Chart.SimpleReportViewModel> hordeChart;
        public static List<Models.Chart.SimpleReportViewModel> HordeChart { get { if (hordeChart == null) { GenerateHordeChart(); } return hordeChart; } }

        public static void AddClient(Client c)
        {
            ClientQueue.Add(c);
        }

        public static bool RemoveClientIfExists(string guid)
        {
            Client c;
            if ((c = ClientQueue.FirstOrDefault(t => t.GUID == guid)) != null)
            {
                ClientQueue.Remove(c);
                return true;
            }
            return false;
        }

        public static bool CanFormTeam()
        {
            return ClientQueue.Count(t => t.Model.Role == Enums.Role.Tank) >= 1
                && ClientQueue.Count(t => t.Model.Role == Enums.Role.Healer) >= 1
                && ClientQueue.Count(t => t.Model.Role == Enums.Role.Damage) >= 3;
        }

        public static List<Client> FormTeam()
        {
            if (!CanFormTeam())
                return null;
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

        public static void GenerateAllianceChart()
        {
            List<Models.Chart.SimpleReportViewModel> chart = new List<Models.Chart.SimpleReportViewModel>();
            for (int i = 15; i <= 60; i++)
            {
                chart.Add(new Models.Chart.SimpleReportViewModel
                {
                    DimensionOne = i.ToString(),
                    TankCount = ClientQueue.Count(t => t.Model.Faction == Enums.Faction.Alliance && t.Model.Level == i && t.Model.Role == Enums.Role.Tank),
                    HealerCount = ClientQueue.Count(t => t.Model.Faction == Enums.Faction.Alliance && t.Model.Level == i && t.Model.Role == Enums.Role.Healer),
                    DamageCount = ClientQueue.Count(t => t.Model.Faction == Enums.Faction.Alliance && t.Model.Level == i && t.Model.Role == Enums.Role.Damage),
                });
            }
            allianceChart = chart;
        }

        public static void GenerateHordeChart()
        {
            List<Models.Chart.SimpleReportViewModel> chart = new List<Models.Chart.SimpleReportViewModel>();
            for (int i = 15; i <= 60; i++)
            {
                chart.Add(new Models.Chart.SimpleReportViewModel
                {
                    DimensionOne = i.ToString(),
                    TankCount = ClientQueue.Count(t => t.Model.Faction == Enums.Faction.Horde && t.Model.Level == i && t.Model.Role == Enums.Role.Tank),
                    HealerCount = ClientQueue.Count(t => t.Model.Faction == Enums.Faction.Horde && t.Model.Level == i && t.Model.Role == Enums.Role.Healer),
                    DamageCount = ClientQueue.Count(t => t.Model.Faction == Enums.Faction.Horde && t.Model.Level == i && t.Model.Role == Enums.Role.Damage),
                });
            }
            hordeChart = chart;
        }
    }
}
