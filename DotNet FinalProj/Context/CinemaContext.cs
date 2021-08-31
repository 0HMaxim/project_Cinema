using DotNet_FinalProj.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_FinalProj.Context
{
    public class CinemaContext : DbContext
    {
        public CinemaContext() : base("name=CinemaDB") { }
        public virtual DbSet<CinemaHall> CinemaHall { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Genre> Gengre { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        //public virtual DbSet<Producer> Producer { get; set; }
    }
}
