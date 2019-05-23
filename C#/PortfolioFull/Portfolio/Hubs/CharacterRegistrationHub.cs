using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Portfolio.RealTimeBackend;

namespace Portfolio.Hubs
{
    //https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-2.2
    public class CharacterRegistrationHub : Hub
    {
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
            await Clients.All.SendAsync("RegistrationReceived", ClientManager.TankCount, ClientManager.HealerCount, ClientManager.DamageCount);

            //Code that determines if we have a team here
            if (ClientManager.CanFormTeam())
            {
                List<Client> team = ClientManager.FormTeam();
                foreach (Client c in team)
                {
                    string message =  team.Aggregate("", (total, next) => total += "<p>" + next.Model.Name + ", " + next.Model.Level + " " + next.Model.Role.ToString() + " " + next.Model.Class.ToString() + "</p>");
                    await Clients.Client(c.GUID).SendAsync("TeamFormedReceived", message);
                }
            }
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            ClientManager.RemoveClientIfExists(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
