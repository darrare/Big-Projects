using System;
using System.ComponentModel.DataAnnotations;
using ClassicLookingForGroup.RealTimeBackend;

namespace ClassicLookingForGroup.Models
{
    public class CharacterRegistrationModel
    {
        //Checks for requirement done in characterregistration.js
        public Enums.Class Class { get; set; }

        public Enums.Role Role { get; set; }

        public int Level { get; set; }

        public Enums.Faction Faction { get; set; }

        public string Name { get; set; }

        public static string VerifyEligibility(Enums.Class cls, Enums.Role role, Enums.Faction faction)
        {
            string retVal = "";
            if (faction == Enums.Faction.Horde && cls == Enums.Class.Paladin)
            {
                retVal += "<p style=\"color:red;\">Paladins cannot be horde</p>";
            }

            if (faction == Enums.Faction.Alliance && cls == Enums.Class.Shaman)
            {
                retVal += "<p style=\"color:red;\">Shaman cannot be alliance</P>";
            }

            switch (role)
            {
                case Enums.Role.Tank:
                    switch (cls)
                    {
                        case Enums.Class.Hunter:
                            retVal += "<p style=\"color:red;\">" + cls.ToString() + "'s cannot queue as a tank";
                            break;
                        case Enums.Class.Mage:
                            retVal += "<p style=\"color:red;\">" + cls.ToString() + "'s cannot queue as a tank";
                            break;
                        case Enums.Class.Priest:
                            retVal += "<p style=\"color:red;\">" + cls.ToString() + "'s cannot queue as a tank";
                            break;
                        case Enums.Class.Rogue:
                            retVal += "<p style=\"color:red;\">" + cls.ToString() + "'s cannot queue as a tank";
                            break;
                        case Enums.Class.Shaman:
                            retVal += "<p style=\"color:red;\">" + cls.ToString() + "'s cannot queue as a tank";
                            break;
                        case Enums.Class.Warlock:
                            retVal += "<p style=\"color:red;\">" + cls.ToString() + "'s cannot queue as a tank";
                            break;
                    }
                    break;
                case Enums.Role.Healer:
                    switch (cls)
                    {
                        case Enums.Class.Hunter:
                            retVal += "<p style=\"color:red;\">" + cls.ToString() + "'s cannot queue as a healer";
                            break;
                        case Enums.Class.Mage:
                            retVal += "<p style=\"color:red;\">" + cls.ToString() + "'s cannot queue as a healer";
                            break;
                        case Enums.Class.Rogue:
                            retVal += "<p style=\"color:red;\">" + cls.ToString() + "'s cannot queue as a healer";
                            break;
                        case Enums.Class.Warlock:
                            retVal += "<p style=\"color:red;\">" + cls.ToString() + "'s cannot queue as a healer";
                            break;
                        case Enums.Class.Warrior:
                            retVal += "<p style=\"color:red;\">" + cls.ToString() + "'s cannot queue as a healer";
                            break;
                        default:
                            break;
                    }
                    break;
            }
            return retVal;
        }
    }
}