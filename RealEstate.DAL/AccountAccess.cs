﻿using RealEstate.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight.Database;
using System.Data;

namespace RealEstate.DAL
{
   public class AccountAccess
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDbConnection Connection { get; set; }
        private IAccount RealEstateDB { get; set; }
        public AccountAccess()
        {
            this.Connection = ConnectionHelper.CreateConnection();
            this.RealEstateDB = this.Connection.AsParallel<IAccount>();
        }

        public async Task CreateComment(RealEstateEnquiry comment)
        {
            try
            {
                await RealEstateDB.AddComment(comment.FirstName, comment.LastName, comment.Email, comment.Phone, comment.Comment);
            }
            catch (Exception ex)
            {
                string fullName = comment.FirstName + " " + comment.LastName;
                logger.Error(string.Format("Error creating the Enquiry for customer:{0} with email:{1} and phone:{2}",fullName, comment.Email,comment.Phone), ex);
                throw new RealEstateException("Couldnot create the Enquiry");
            }
        }
    }
}