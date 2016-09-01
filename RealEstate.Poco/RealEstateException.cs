using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Poco
{
    /// <summary>
    /// This is used to create custom exceptions in the Real Estate App
    /// </summary>
   public class RealEstateException : Exception
    {
       public RealEstateException()
       {

       }

       public RealEstateException(string message) : base(message)
       {

       }

       public RealEstateException(string message,Exception innerException)
           : base(message, innerException)
       {

       }
    }
}
