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
            bool? can_override = true;
            //bool result = can_override ?? false;
            var result = can_override ?? false;
            Console.WriteLine("Result is : {0}",result);            
            Console.ReadKey();
        }
    }
}
