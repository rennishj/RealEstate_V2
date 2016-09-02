using RealEstate.Poco;
using RealEstate.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Web.Extensions
{
    public static class ViewModelExtension
    {
        public static RealEstateEnquiry ToPoco(this EnquiryViewModel vm)
        {
            if (vm == null)
                return new RealEstateEnquiry();
            return new RealEstateEnquiry
            {
                Comment = vm.Comment,
                Email = vm.Email,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Phone = vm.Phone
            };
        }
    }
}