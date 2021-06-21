using DetectionLibrary.Contracts;
using DetectionLibrary.DataModel;
using DetectionLibrary.HelperClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DetectionLibrary
{
    public class OutlierCalculator:IMeanValueCalculator, IDeviationCalculator
    {
        private const string FilePath = @"Outliers.csv";
        public static DataList GetDataFromFile(string filePath)
        {
            DataList dataList = new DataList();
            DataTable dt = FileReadWrite.ConvertCSVtoDataTable(filePath);
            dataList.DataPointsList = (from DataRow dr in dt.Rows
                           select new DataPoints
                           {
                               Date = Convert.ToString(dr[0]),
                               Price = Convert.ToDouble(dr[1])
                           }).ToList();

            return dataList;
        }

        public double GetMeanValue(double[] Data)
        {
            return Data.Average();
        }

        public double GetDeviationValue(double[] Data, double mean)
        {
            double result = 0;
            for (int i = 0; i < Data.Length; i++)
            {
                result += ((Data[i] - mean) * (Data[i] - mean));
            }
            result = result / Data.Length;
            return Math.Sqrt(result);
        }

        public DataList GetOutLierFromData()
        {
            int i = 0;
            DataList dataList = GetDataFromFile(FilePath);
            double[] priceData = new double[dataList.DataPointsList.Count];
            foreach (var data in dataList.DataPointsList)
            {
                priceData[i] = data.Price;
                i++;
            }
            double mean = GetMeanValue(priceData);
            double deviation = GetDeviationValue(priceData, mean);
            int threshold = 3;
            DataList outlierList = new DataList();
            List<DataPoints> lstPoint = new List<DataPoints>();
            foreach (var dataPoints in dataList.DataPointsList)
            {
                double price = (dataPoints.Price - mean) / deviation;
                if (price > threshold)
                {
                    lstPoint.Add(dataPoints);
                }
            }
            outlierList.DataPointsList = lstPoint;
            return outlierList;
        }
    }
}
