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
        public async Task RequestCharts()
        {
            await UpdateClients(Context.ConnectionId);
        }

        public async Task Register(string cls, string role, string level, string faction, string name)
        {
            ClientManager.AddClient(new Client(Context.ConnectionId,
                new Models.CharacterRegistrationModel()
                {
                    Class = (Enums.Class)Enum.Parse(typeof(Enums.Class), cls),
                    Role = (Enums.Role)Enum.Parse(typeof(Enums.Role), role),
                    Level = int.Parse(level),
                    Faction = (Enums.Faction)Enum.Parse(typeof(Enums.Faction), faction),
                    Name = name,
                }));

            //Code that determines if we have a team here
            List<Client> team;
            if ((team = ClientManager.FormTeam()) != null)
            {
                foreach (Client c in team)
                {
                    string message = team.Aggregate("", (total, next) => total += "<p>" + next.Model.Name + ", " + next.Model.Level + " " + next.Model.Role.ToString() + " " + next.Model.Class.ToString() + "</p>");
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
                for (int i = 0; i < ClientManager.ClientQueue.Count; i++)
                {
                    await Clients.Client(ClientManager.ClientQueue[i].GUID).SendAsync(
                        "UpdateStatus",
                        i + 1,
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
            else
            {
                await Clients.Client(guid).SendAsync(
                    "UpdateStatus",
                    "N/A",
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
