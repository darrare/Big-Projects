using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicLookingForGroup.Models.Chart
{
    public class SimpleReportViewModel
    {
        public string DimensionOne { get; set; }
        public int TankCount { get; set; }
        public int HealerCount { get; set; }
        public int DamageCount { get; set; }
    }
}
