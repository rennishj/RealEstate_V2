using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Web.Models
{
    public class JsonResponseModel
    {
        public JsonResponseModel(bool sucess,string message = null)
        {
            this.Success = sucess;
            this.Message = message;
            this.Errors = new List<string>();
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}