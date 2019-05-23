using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portfolio.Models;

namespace Portfolio.RealTimeBackend
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
