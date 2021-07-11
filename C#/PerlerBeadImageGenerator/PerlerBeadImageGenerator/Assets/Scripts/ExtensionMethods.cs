using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Drawing;
using Color = UnityEngine.Color;


public static class ExtensionMethods
{
    public static IEnumerable<Color> ToEnumerable<Color>(this Color[][] target)
    {
        for (int x = 0; x < target.Length; x++)
        {
            for (int y = 0; y < target[x].Length; y++)
            {
                yield return target[x][y];
            }
        }
    }

    public static bool IsColorGrey(this Color target)
    {
        return target.r == target.g && target.r == target.b;
    }

    public static System.Drawing.Color ToSystemColor(this Color target)
    {
        return System.Drawing.Color.FromArgb((int)(target.a == 1 ? 255 : 0), (int)(target.r * 255), (int)(target.g * 255), (int)(target.b * 255));
    }
}
