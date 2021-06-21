using System;
using System.Collections.Generic;
using System.Text;

namespace DetectionLibrary.Contracts
{
    public interface IDeviationCalculator
    {
        double GetDeviationValue(double[] Data, double mean);
    }
}
