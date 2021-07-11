using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using UnityEngine;
using Color = System.Drawing.Color;
using System.IO;

namespace ForGeneratingOutput
{
    public static class GenerateOutput
    {
        public static void Generate(UnityEngine.Color[,] Result, Dictionary<UnityEngine.Color, int> ColorUseCount)
        {

        }

        private static Color ToSystemColor(UnityEngine.Color target)
        {
            return Color.FromArgb((int)(target.a * 255), (int)(target.g * 255), (int)(target.b * 255));
        }
    }
}
