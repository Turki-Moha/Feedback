using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.Models
{
    public class FeedBackDB : DbContext
    {

        public FeedBackDB(DbContextOptions<FeedBackDB> options)
            : base(options)
        {
        }


        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Test> Tests { get; set; }

    }
}
