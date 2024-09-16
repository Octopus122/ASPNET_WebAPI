using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    // Created to help with search for certain dataobject
    //DbContext - bridge among the database and application
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dBContextOptions) : base(dBContextOptions)
        {
            
        }
        public DbSet<Stock> Stock { get; set; }
        // DbSet - manipulating the hole table
        public DbSet<Comment> Comment {get; set; }
    }
}