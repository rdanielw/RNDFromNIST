using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNDFromNISTLib
{
    public class Record
    {
        public string Version { get; set; }
        public int Frequency { get; set; }
        public long TimeStamp { get; set; }
        public string SeedValue { get; set; }
        public string PreviousOutputValue { get; set; }
        public string SignatureValue { get; set; }
        public string OutputValue { get; set; }
        public string StatusCode { get; set; }  /* 0 good, 1 start of a new chain of values, 2 time between value is over freq but chain is good */

        public override string ToString()
        {
            return OutputValue;
        }
    }
}
