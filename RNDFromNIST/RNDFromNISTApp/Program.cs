using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNDFromNISTApp
{
    class Program
    {
        static void Main(string[] args)
        {
            long timestamp = 0;
            var c = new RNDFromNISTLib.Client();

            if (args.Length == 0)
            {
                var jan11970 = new DateTimeOffset(year:1970, month:1, day:1, hour:0, minute:0, second:0, offset:new TimeSpan(0));
                var ts = DateTimeOffset.UtcNow - jan11970;

                var timestampDouble = ts.TotalSeconds;
                timestamp = (long)timestampDouble;
            }
            else
            {
                Int64.TryParse(args[0], out timestamp);
            }
            

            var current = c.GetCurrent(timestamp);
            Console.WriteLine("current: " + current.ToString());

            var previous = c.GetPrevious(timestamp);
            Console.WriteLine("previous: " + previous.ToString());

            var next = c.GetNext(timestamp);
            Console.WriteLine("next: " + next.ToString());

            var last = c.GetLast();
            Console.WriteLine("last: " + last.ToString());

            var startChain = c.GetStartChain(timestamp);
            Console.WriteLine("startChain: " + startChain.ToString());
        }
    }
}
