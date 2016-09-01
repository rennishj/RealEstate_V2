using RealEstate.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.BLL
{
    public class AccountBLL
    {
        public AccountBLL()
        {
            this.AccountDatabase = new AccountAccess();
        }

        public AccountAccess AccountDatabase { get; set; }
    }
}
