using System;
using System.Collections.Generic;
using System.Text;

namespace DetectionLibrary.Contracts
{
   public interface IMeanValueCalculator
    {
        double GetMeanValue(double[] Data);
    }
}
