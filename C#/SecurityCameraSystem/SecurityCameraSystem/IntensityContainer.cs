using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCameraSystem
{
    class IntensityContainer
    {
        List<double> container = new List<double>();

        public IntensityContainer()
        {
            //initialize to 30 elements of 0
            for(int i = 0; i < 60; i++)
            {
                container.Add(0);
            }
        }

        public void Add(double val)
        {
            container.Insert(0, val);
            container.RemoveAt(container.Count - 1);
        }

        public double GetAverage()
        {
            return container.Average();
        }
    }
}
