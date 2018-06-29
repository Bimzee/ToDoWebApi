using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Todo.Domain
{
    public class SchoolContext : DbContext
    {
       
        public SchoolContext() : base()
        {

        }

        public SchoolContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        
    }
}
