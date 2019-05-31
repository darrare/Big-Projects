using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ClassicLookingForGroup.RealTimeBackend;

namespace ClassicLookingForGroup.Hubs
{
    //https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-2.2
    public class CharacterRegistrationHub : Hub
    {
        /// <summary>
        /// Updates the charts on a specific client.
        /// Called only once when they first open the page.
        /// </summary>
        public async Task RequestCharts()
        {
            await UpdateClients(Context.ConnectionId);
        }

        /// <summary>
        /// Register a new client to a party (aka, puts them in queue).
        /// If new client finalizes a party, that party is taken out of the queue and they are notified of their group.
        /// Afterwards, updates all connected clients chart information.
        /// </summary>
        /// <param name="cls">Clients class</param>
        /// <param name="role">Clients role</param>
        /// <param name="level">Clients level</param>
        /// <param name="faction">Clients faction</param>
        /// <param name="name">Clients name</param>
        public async Task Register(string cls, string role, string level, string faction, string name)
        {
            List<Client> newParty;
            //If a new list is formed, it means that party is complete and ready to go. They have already been removed from the collection.
            if ((newParty = ClientManager.AddClient(new Client(Context.ConnectionId,
                new Models.CharacterRegistrationModel()
                {
                    Class = (Enums.Class)Enum.Parse(typeof(Enums.Class), cls),
                    Role = (Enums.Role)Enum.Parse(typeof(Enums.Role), role),
                    Level = int.Parse(level),
                    Faction = (Enums.Faction)Enum.Parse(typeof(Enums.Faction), faction),
                    Name = name,
                }))) != null)
            {
                foreach (Client c in newParty)
                {
                    string message = newParty.Aggregate("", (total, next) => total += "<p>" + next.Model.Name + ", " + next.Model.Level + " " + next.Model.Role.ToString() + " " + next.Model.Class.ToString() + "</p>");
                    await Clients.Client(c.GUID).SendAsync("TeamFormedReceived", message);
                }
            }

            if (faction == "Alliance")
                ClientManager.GenerateAllianceChart();
            else
                ClientManager.GenerateHordeChart();

            //Updates every user with their position in queue
            await UpdateClients();
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// Called whenever the client disconnects.
        /// This can be anything from navigating to another page on the site
        /// or leaving the site completely via navigating to another domain or closing their browser
        /// or they lose internet?!?
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (ClientManager.RemoveClientIfExists(Context.ConnectionId))
            {
                ClientManager.GenerateAllianceChart();
                ClientManager.GenerateHordeChart();
                await UpdateClients();
            }

            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Sends a message to every client to update their graphs
        /// </summary>
        /// <param name="guid">Optional param. If input only sends update to that person.</param>
        public async Task UpdateClients(string guid = "")
        {
            string aLabels = Newtonsoft.Json.JsonConvert.SerializeObject(ClientManager.AllianceChart.Select(t => t.DimensionOne).ToList());
            string aTankData = Newtonsoft.Json.JsonConvert.SerializeObject(ClientManager.AllianceChart.Select(t => t.TankCount).ToList());
            string aHealerData = Newtonsoft.Json.JsonConvert.SerializeObject(ClientManager.AllianceChart.Select(t => t.HealerCount).ToList());
            string aDamageData = Newtonsoft.Json.JsonConvert.SerializeObject(ClientManager.AllianceChart.Select(t => t.DamageCount).ToList());
            string hLabels = Newtonsoft.Json.JsonConvert.SerializeObject(ClientManager.HordeChart.Select(t => t.DimensionOne).ToList());
            string hTankData = Newtonsoft.Json.JsonConvert.SerializeObject(ClientManager.HordeChart.Select(t => t.TankCount).ToList());
            string hHealerData = Newtonsoft.Json.JsonConvert.SerializeObject(ClientManager.HordeChart.Select(t => t.HealerCount).ToList());
            string hDamageData = Newtonsoft.Json.JsonConvert.SerializeObject(ClientManager.HordeChart.Select(t => t.DamageCount).ToList());

            if (guid == "")
            {
                foreach(Party p in ClientManager.Parties)
                {
                    string message = p.GeneratePartyInfoHTML();
                    foreach(Client c in p.GetParty())
                    {
                        await Clients.Client(c.GUID).SendAsync("UpdatePartyInfo", message);
                    }
                }
                await Clients.All.SendAsync(
                        "UpdateStatus",
                        aLabels,
                        aTankData,
                        aHealerData,
                        aDamageData,
                        hLabels,
                        hTankData,
                        hHealerData,
                        hDamageData
                        );
            }
            else
            {
                await Clients.Client(guid).SendAsync(
                    "UpdateStatus",
                    aLabels,
                    aTankData,
                    aHealerData,
                    aDamageData,
                    hLabels,
                    hTankData,
                    hHealerData,
                    hDamageData
                    );
            }
        }
    }
}
