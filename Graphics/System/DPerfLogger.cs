using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bio.Graphics
{
    public static class DPerfLogger
    {
        // Static variables
        public static StringBuilder sb;
        public static int TestTimeInSeconds = 5;

        // static properties
        public static int TotalSamples { get; set; }
        public static List<float> SampleFloatSet { get; set; }

        // Static Methods
        public static void Initialize(string TestTitleType)
        {
            sb = new StringBuilder();
            sb.AppendLine("" + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + " - " + TestTitleType);
            SampleFloatSet = new List<float>(TestTimeInSeconds * 5000);
        }
        internal static void Frame(float frameTime)
        {
            SampleFloatSet.Insert(SampleFloatSet.Count, 1000.0f / frameTime);
        }
        public static void WriteFPSTest()
        {
            File.AppendAllText("..\\..\\..\\Test.txt", sb.ToString());
        }
        public static void CalcualteFPSMetrics(List<int> sampleSet)
        {
            // If we recieve the Samplset with values pertaining to the elaped times for each frame sample instead of the calculated FPS, we can determine a Sub-Set of samples representing the initial loading or ramp-up time taken, before the execution reachwa an optimal average FPS during a Test. Therefore creating a standdard deviation for the first say 3 second of execution and a seperate more accurate standard deviation calulation fore the remaning FPS during the rest of the execution of a Test. This should prvide a closer representation of calulations that represent this execution speed of a particular feature being tested. So we will Trim off the first 3 seconds or a specified amount of time from the Samplset, Perform calculations regarding the startup, then proceed with the rest of the sample and perform the caluclations below for inresed accuracy.


            double fPSAverage = 0.0, FPSMinimum = 0.0, FPSMaximum = 0.0, standardDeviation = 0.0;

            if (sampleSet.Any())
            {
                fPSAverage = sampleSet.Average();
                FPSMinimum = sampleSet.Min();
                FPSMaximum = sampleSet.Max();

                // Calculate Stanrd Deviation.
                double sumOfSquaresOfDifferences = sampleSet.Select(val => (val - fPSAverage) * (val - fPSAverage)).Sum();
                standardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / sampleSet.Count);
            }

            sb.AppendLine(string.Format("FPS Stats:   # Samples:  {0}\tAve: {1}\tMin: {2}\tMax: {3}\tStdDev: {4}/{5:p2}", sampleSet.Count, fPSAverage.ToString("f4"), FPSMinimum, FPSMaximum, standardDeviation.ToString("f2"), standardDeviation / fPSAverage));
        }
        public static double CalcualteFPSMetrics(List<float> sampleFloatSet, bool writeToFile, bool cleanDirtyData, double percentageAllowance)
        {
            double fPSAverage = 0.0, FPSMinimum = 0.0, FPSMaximum = 0.0, standardDeviation = 0.0, standardDeviation2 = 0.0, newfPSAverage2 = 0.0;

            // Time Test
            double timeInBeforeDirtyDataRemoved = sampleFloatSet.Select(e => 1000.0 / e).Sum() / 1000.0;

            if (sampleFloatSet.Any())
            {
                fPSAverage = sampleFloatSet.Average();
                FPSMinimum = sampleFloatSet.Min();
                FPSMaximum = sampleFloatSet.Max();

                // List<float> sameplSet = sampleFloatSet.ToList();
                if (cleanDirtyData)
                {
                    // Record the count of SDamples before reducing Samples that represent Dirty Data.
                    TotalSamples = sampleFloatSet.Count;

                    // Perform Dirty Data cleaning algorythum
                    newfPSAverage2 = RemoveUpperLowerLimits(FPSMinimum, FPSMaximum, sampleFloatSet, percentageAllowance);

                    // Update the fpsMin & Max values since Dirty data has now been removed.
                    FPSMinimum = sampleFloatSet.Min();
                    FPSMaximum = sampleFloatSet.Max();
                }
                else
                    newfPSAverage2 = fPSAverage;
                //sameplSet.RemoveAll( (e => e <= (FPSMinimum * 1.1) ));
                //sameplSet.RemoveAll((e => e >= (FPSMaximum * 0.9) ));
                //float newfPSAverage = sameplSet.Average();
                //float newFPSMinimum = sameplSet.Min();
                //float newFPSMaximum = sameplSet.Max();
                //sameplSet.RemoveAll((e => e <= (newFPSMinimum * 1.1)));
                //sameplSet.RemoveAll((e => e >= (newFPSMaximum * 0.9)));
                //float newfPSAverage2 = sameplSet.Average();
                //float newFPSMinimum2 = sameplSet.Min();
                //float newFPSMaximum2 = sameplSet.Max();

                // Calculate Stanrd Deviation, we are adding each value against the average instead of subtracting like above.
                //double sumOfSquaresOfDifferences = sampleFloatSet.Select(val => (val - fPSAverage) * (val - fPSAverage)).Sum();
                //standardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / sampleFloatSet.Count);

                double sumOfSquaresOfDifferences2 = sampleFloatSet.Select(val => (val - newfPSAverage2) * (val - newfPSAverage2)).Sum();
                standardDeviation2 = Math.Sqrt(sumOfSquaresOfDifferences2 / sampleFloatSet.Count);
            }

            if (writeToFile)
                sb.AppendLine(string.Format("FPS Stats: # Samples: {0}\tTime: {8}\tAve: {1}\tMin: {2}\tMax: {3}\tStdDev: {4}/{5:p2}\tDirtyData: {6}/{7:p2}", sampleFloatSet.Count, newfPSAverage2.ToString("f4"), (FPSMinimum < 100) ? FPSMinimum.ToString("f0") + "  " : FPSMinimum.ToString("f0"), FPSMaximum.ToString("f0"), ((standardDeviation2 / newfPSAverage2) * newfPSAverage2).ToString("f2"), standardDeviation2 / newfPSAverage2, (TotalSamples == 0) ? " - " : (TotalSamples - sampleFloatSet.Count).ToString(), (TotalSamples == 0) ? " - " : ((double)(TotalSamples - sampleFloatSet.Count) / (double)TotalSamples).ToString("p2"), (timeInBeforeDirtyDataRemoved < 10.0) ? timeInBeforeDirtyDataRemoved.ToString("f3") : timeInBeforeDirtyDataRemoved.ToString("f2")));

            //sb.AppendLine(string.Format("{0}\t{8}\t{1}\t{2}\t{3}\t{4}\t{5:p2}\t{6}\t{7:p2}", sampleFloatSet.Count, newfPSAverage2.ToString("f12"), FPSMinimum.ToString("f12"), FPSMaximum.ToString("f12"), ((standardDeviation2 / newfPSAverage2) * newfPSAverage2).ToString("f12"), standardDeviation2 / newfPSAverage2, (TotalSamples == 0) ? " - " : (TotalSamples - sampleFloatSet.Count).ToString(), (TotalSamples == 0) ? " - " : ((float)(TotalSamples - sampleFloatSet.Count) / (float)TotalSamples).ToString("p2"), (timeInBeforeDirtyDataRemoved < 10.0) ? timeInBeforeDirtyDataRemoved.ToString("f12") : timeInBeforeDirtyDataRemoved.ToString("f12")));

            return standardDeviation2;
        }

       

        private static double RemoveUpperLowerLimits(double fPSMinimum, double fPSMaximum, List<float> sameplSet, double percentageAllowance)
        {
            double doubleAverage = 0.0;
            float tempfPSAverage = sameplSet.Average();

            // Ensure we only Trim Dirty Data that needs to be Max or min values.
            if (fPSMinimum < (tempfPSAverage * (1.0 - percentageAllowance) )) // .7
                sameplSet.RemoveAll((e => e <= (fPSMinimum * 1.1)));
            if (fPSMaximum > (tempfPSAverage * (1.0 + percentageAllowance) )) // 1.3
                sameplSet.RemoveAll((e => e >= (fPSMaximum * 0.9)));

            // Re-Evaluate the Minimum and Max values.
            float newfPSAverage = sameplSet.Average();
            float newFPSMinimum = sameplSet.Min();
            float newFPSMaximum = sameplSet.Max();

            // .70 to 1.3 default values.
            if (newFPSMinimum < (newfPSAverage * (1.0 - percentageAllowance)) || newFPSMaximum > newfPSAverage * (1.0 + percentageAllowance) )
                doubleAverage = RemoveUpperLowerLimits(newFPSMinimum, newFPSMaximum, sameplSet, percentageAllowance);
            else
                return newfPSAverage;

            return doubleAverage;
        }

        internal static void ShutDown()
        {
            int rampupIndexEnd = 1;
            float percentAllowance = 0.25f;
            double standardDeveiation, startDeviation;

            standardDeveiation = CalcualteFPSMetrics(SampleFloatSet.Skip(SampleFloatSet.Count / 2).Take((SampleFloatSet.Count / 2) - (int)(SampleFloatSet.Count * 0.1)).ToList(), false, true, 0.3);
            float fpsAverage = SampleFloatSet.Skip(SampleFloatSet.Count / 2).Take((SampleFloatSet.Count / 2) - (int)(SampleFloatSet.Count * 0.1)).Average();

            // Now loop through the SamepleSet and when the standard deviation is grater then the above, its still rampming up.
            for (int i = 1; i < SampleFloatSet.Count; i++)
            {
                // Isolate a sliding second for ramp analysis
                startDeviation = CalcualteFPSMetrics(SampleFloatSet.Skip(i - 1).Take(100).ToList(), false, true, percentAllowance);
                float startAverage = SampleFloatSet.Skip(i - 1).Take(100).Average();
                bool tester = ((fpsAverage - startAverage) <= Math.Abs(fpsAverage * 0.05));

                if (startDeviation <= (standardDeveiation * (1.0f + 0.05f)))  // was 1.1  then  1.3  then  percentAllowance.
                {
                    rampupIndexEnd = i + 99;// - 1;
                    break;
                }
            }

            // Reset the Total Count before writting any results.
            TotalSamples = 0;

            startDeviation = CalcualteFPSMetrics(SampleFloatSet.Take(rampupIndexEnd + 1).ToList(), true, false, 0.3);
            standardDeveiation = CalcualteFPSMetrics(SampleFloatSet.Skip(rampupIndexEnd).ToList(), true, true, 0.45); // was .5
            WriteFPSTest();

            SampleFloatSet.Clear();
            sb.Clear();
        }
    }
}
