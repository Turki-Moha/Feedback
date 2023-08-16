using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.Models
{
    public class Request
    {
        public int Id { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public string Title { get; set; }
        [Display(Name = "Description2")]
        public string Description { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid Gid { get; set; }

        public string ImageUrl { get; set; }
        public byte[] ImageUrlNew { get; set; }
    }
}
