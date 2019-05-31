using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicLookingForGroup.RealTimeBackend
{
    public class Party
    {
        public Client Tank { get; set; }
        public Client Healer { get; set; }

        public Client[] Damage { get; set; } = new Client[3];

        /// <summary>
        /// This is what is displayed on the web page after a person has entered the queue. 
        /// Gets updated every time the party is updated.
        /// </summary>
        /// <returns>The string to put as the innerHTML element</returns>
        public string GeneratePartyInfoHTML()
        {
            string retVal = "";
            retVal += "<p>Tank " + (Tank == null ? "not yet " : "") + "found</p>";
            retVal += "<p>Healer " + (Healer == null ? "not yet " : "") + "found</p>";
            retVal += "<p>Damage " + (Damage[0] == null ? "not yet " : "") + "found</p>";
            retVal += "<p>Damage " + (Damage[1] == null ? "not yet " : "") + "found</p>";
            retVal += "<p>Damage " + (Damage[2] == null ? "not yet " : "") + "found</p>";
            return retVal;
        }

        /// <summary>
        /// Should probably refactor?
        /// Simply determines if the party is complete by checking if the count is 5.
        /// </summary>
        /// <returns>
        /// True: Party has 5 members and is complete.
        /// False: Party does not yet have 5 members.
        /// </returns>
        public bool IsValidParty()
        {
            return GetParty().Count == 5;
        }

        /// <summary>
        /// Inserts a party member into the party.
        /// When a client is added to the system, this method is called on every available party to attempt to place them in the first available spot.
        /// </summary>
        /// <param name="c">The client to insert into the party</param>
        /// <returns>
        /// True: Member was successfully inserted into the party.
        /// False: Member could not join this party, either due to being out of the level range or unable to fill the needed role.
        /// </returns>
        public bool InsertPartyMember(Client c)
        {
            //If they aren't within a reasonable level range, don't bother adding them
            if (!IsPotentialNewMemberInLevelRange(c)) return false;

            bool isSuccess = false;
            switch (c.Model.Role)
            {
                case Enums.Role.Damage:
                    for(int i = 0; i < 3; i++)
                        if (Damage[i] == null) { Damage[i] = c; isSuccess = true; break; }
                    break;
                case Enums.Role.Tank:
                    if (Tank == null) { Tank = c; isSuccess = true; }
                    break;
                case Enums.Role.Healer:
                    if (Healer == null) { Healer = c; isSuccess = true; }
                    break;
            }
            return isSuccess;
        }

        /// <summary>
        /// Averages the levels of the party so only parties with close level ranges are accepted.
        /// </summary>
        /// <param name="c">The client to check if they are compatible with this party (level wise)</param>
        /// <returns>
        /// True: Client is within the average range of this party.
        /// False: Client is not within the average range of this party.
        /// </returns>
        private bool IsPotentialNewMemberInLevelRange(Client c)
        {
            float levelAverage = 0;
            int playerCount = 0;

            if (Tank != null) { levelAverage += Tank.Model.Level; playerCount++; }
            if (Healer != null) { levelAverage += Healer.Model.Level; playerCount++; }
            for (int i = 0; i < 3; i++)
            {
                if (Damage[i] != null) { levelAverage += Damage[i].Model.Level; playerCount++; }
            }

            if (playerCount == 0) return true;

            levelAverage /= playerCount;
            return MathF.Abs(levelAverage - c.Model.Level) <= 5;
        }

        /// <summary>
        /// Returns the party in list format.
        /// Does not return null elements
        /// </summary>
        /// <returns>Every active player of the party</returns>
        public List<Client> GetParty()
        {
            return new List<Client>() { Tank, Healer, Damage[0], Damage[1], Damage[2] }.Where(t => t != null).ToList();
        }

        /// <summary>
        /// Removes a specific client from the party (likely due to disconnect)
        /// </summary>
        /// <param name="c">Client to remove from the party</param>
        public void RemoveMember(Client c)
        {
            if (Tank == c) { Tank = null; }
            if (Healer == c) { Healer = null;  }
            for (int i = 0; i < 3; i++)
            {
                if (Damage[i] == c) { Damage[i] = null; }
            }
        }
    }
}
