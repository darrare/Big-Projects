using System;
using System.ComponentModel.DataAnnotations;
using Portfolio.RealTimeBackend;

namespace Portfolio.Models
{
    public class CharacterRegistrationModel
    {
        //Checks for requirement done in characterregistration.js
        public Enums.Class Class { get; set; }

        public Enums.Role Role { get; set; }

        public int Level { get; set; }

        public Enums.Faction Faction { get; set; }

        public string Name { get; set; }
    }
}