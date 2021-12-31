using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{
    public static class ExtensionMethods
    {
        public static bool AreEqual(this List<int> obj, List<int> obj2)
        {
            return !obj.Except(obj2).Any();
        }
    }
}
