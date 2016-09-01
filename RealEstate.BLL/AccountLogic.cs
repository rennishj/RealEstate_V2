﻿using RealEstate.DAL;
using RealEstate.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.BLL
{
    public class AccountLogic
    {
        public AccountLogic()
        {
            this.AccountDAL = new AccountAccess();
        }

        public AccountAccess AccountDAL { get; set; }

        public async  Task RealEstateEnquiryCreate(RealEstateEnquiry enquiry)
        {
            await AccountDAL.CreateRealEstateEnquiry(enquiry);
        }
    }
}
