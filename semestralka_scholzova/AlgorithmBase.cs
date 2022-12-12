using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova
{
    class AlgorithmBase
    {
        private DateTime startTime;
        private DateTime stopTime;

        public readonly List<string> Results = new List<string>();

        public double Duration => (stopTime - startTime).TotalSeconds;

        public void StartTimeMeasurementAndClearResults()
        {
            Results.Clear();

            startTime = DateTime.Now;
        }

        public void StopTimeMeasurement()
        {
            stopTime = DateTime.Now;
        }

        // TODO: fix possible synchronization issue?
        internal void AddResult(string result)
        {
            lock (this) Results.Add(result);
        }
    }
}
