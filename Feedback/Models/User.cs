using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Error")]
        [StringLength(50)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="Number")]
        [Required(ErrorMessage = "Error")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "MM")]
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int Status { get; set; }
    }
}
