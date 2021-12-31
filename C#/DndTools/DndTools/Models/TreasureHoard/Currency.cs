using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DndTools.Models.TreasureHoard.Helpers;

namespace DndTools.Models.TreasureHoard
{
    public class Currency
    {
        List<ICurrency> Pieces { get; set; } = new List<ICurrency>();

        public Currency(int challengeRating)
        {
            
        }
    }
}
