using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            string pv = "Test";
            string[] pvs = pv.Split(',');
            if (pvs != null && pvs.Count() > 0 && pvs.Any(p => p.Equals("AC")))
            {
                foreach (var item in pvs)
                {
                    Console.WriteLine("This has AC in it : {0}",item);
                }
            }
                
            Console.ReadKey();
        }
    }
}
