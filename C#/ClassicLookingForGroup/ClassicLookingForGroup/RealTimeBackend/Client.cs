using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicLookingForGroup.Models;

namespace ClassicLookingForGroup.RealTimeBackend
{
    public class Client
    {
        public string GUID { get; set; }

        public CharacterRegistrationModel Model;

        public Client(string guid, CharacterRegistrationModel model)
        {
            GUID = guid;
            Model = model;
        }
    }
}
