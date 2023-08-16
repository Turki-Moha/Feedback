using Feedback.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Feedback.ViewModels
{
	public class RequestViewModel
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

        public List<Request> Requests { get; set; }

        public Request Request { get; set; }
    }
}
