using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight.Database;
using System.Data;
using System.Configuration;

namespace RealEstate.DAL
{
    public class ConnectionHelper
    {
        public static IDbConnection CreateConnection()
        {
            return ConfigurationManager.ConnectionStrings["GiftsToKerala"].Connection();
        }
    }
}
