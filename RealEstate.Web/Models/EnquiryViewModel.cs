using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstate.Web.Models
{
    
    public class EnquiryViewModel
    {
        public int CommentId { get; set; }

        [Display(Name="First Name")]
        [Required(ErrorMessage="Firstname is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Display(Name = "Phone Numebr")]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }
    }
}