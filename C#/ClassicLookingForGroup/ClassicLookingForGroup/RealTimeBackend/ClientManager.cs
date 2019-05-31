using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicLookingForGroup.Models;

namespace ClassicLookingForGroup.RealTimeBackend
{
    public static class ClientManager
    {
        /// <summary>
        /// List of incomplete parties
        /// </summary>
        public static List<Party> Parties = new List<Party>();

        static List<Models.Chart.SimpleReportViewModel> allianceChart;
        /// <summary>
        /// Essentially JSON data for generating the alliance chart
        /// </summary>
        public static List<Models.Chart.SimpleReportViewModel> AllianceChart { get { if (allianceChart == null) { GenerateAllianceChart(); } return allianceChart; } }

        static List<Models.Chart.SimpleReportViewModel> hordeChart;
        /// <summary>
        /// Essentially JSON data for generating the horde chart
        /// </summary>
        public static List<Models.Chart.SimpleReportViewModel> HordeChart { get { if (hordeChart == null) { GenerateHordeChart(); } return hordeChart; } }

        /// <summary>
        /// Add a client to first available party. 
        /// If they are the 5th person of that party, the party is returned and handled in CharacterRegistrationHub.cs. Otherwise, null is returned.
        /// </summary>
        /// <param name="c">The client to be added.</param>
        /// <returns>
        /// If a list is returned, it is a full party.
        /// If null is returned, they just joined a team and no full party is formed.
        /// </returns>
        public static List<Client> AddClient(Client c)
        {
            //Try to add the client to every party.
            for (int i = 0; i < Parties.Count; i++)
            {
                if (Parties[i].InsertPartyMember(c))
                {
                    if (Parties[i].IsValidParty())
                    {
                        List<Client> party = Parties[i].GetParty();
                        Parties.Remove(Parties[i]);
                        return party;
                    }
                    return null;
                }
            }

            //Client couldn't be added to a party, create their own party
            Parties.Add(new Party());
            Parties[Parties.Count - 1].InsertPartyMember(c);
            return null;
        }

        /// <summary>
        /// Removes a specific client from any collection. 
        /// Usually called when they disconnect.
        /// </summary>
        /// <param name="guid">The guid of the client to disconnect.</param>
        /// <returns></returns>
        public static bool RemoveClientIfExists(string guid)
        {
            Client c;
            foreach (Party p in Parties)
            {
                if((c = p.GetParty().FirstOrDefault(t => t.GUID == guid)) != null)
                {
                    p.RemoveMember(c);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Generates the json data for the alliance chart.
        /// </summary>
        public static void GenerateAllianceChart()
        {
            List<Models.Chart.SimpleReportViewModel> chart = new List<Models.Chart.SimpleReportViewModel>();
            List<Client> clients = GetClientsInListForm();
            for (int i = 15; i <= 60; i++)
            {
                chart.Add(new Models.Chart.SimpleReportViewModel
                {
                    DimensionOne = i.ToString(),
                    TankCount = clients.Count(t => t.Model.Faction == Enums.Faction.Alliance && t.Model.Level == i && t.Model.Role == Enums.Role.Tank),
                    HealerCount = clients.Count(t => t.Model.Faction == Enums.Faction.Alliance && t.Model.Level == i && t.Model.Role == Enums.Role.Healer),
                    DamageCount = clients.Count(t => t.Model.Faction == Enums.Faction.Alliance && t.Model.Level == i && t.Model.Role == Enums.Role.Damage),
                });
            }
            allianceChart = chart;
        }

        /// <summary>
        /// Generates the json data for the horde chart.
        /// </summary>
        public static void GenerateHordeChart()
        {
            List<Models.Chart.SimpleReportViewModel> chart = new List<Models.Chart.SimpleReportViewModel>();
            List<Client> clients = GetClientsInListForm();
            for (int i = 15; i <= 60; i++)
            {
                chart.Add(new Models.Chart.SimpleReportViewModel
                {
                    DimensionOne = i.ToString(),
                    TankCount = clients.Count(t => t.Model.Faction == Enums.Faction.Horde && t.Model.Level == i && t.Model.Role == Enums.Role.Tank),
                    HealerCount = clients.Count(t => t.Model.Faction == Enums.Faction.Horde && t.Model.Level == i && t.Model.Role == Enums.Role.Healer),
                    DamageCount = clients.Count(t => t.Model.Faction == Enums.Faction.Horde && t.Model.Level == i && t.Model.Role == Enums.Role.Damage),
                });
            }
            hordeChart = chart;
        }

        /// <summary>
        /// Iterates through the parties, putting each individual client in a list that can be iterated itself.
        /// </summary>
        /// <returns>A list of every client currently connected</returns>
        public static List<Client> GetClientsInListForm()
        {
            List<Client> clients = new List<Client>();
            foreach(Party p in Parties)
            {
                clients.AddRange(p.GetParty());
            }
            return clients;
        }
    }
}
