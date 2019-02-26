using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mnasat.Models
{
    public class MnasatDb   :   DbContext
    {
        public DbSet<Usr> Usrs { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Request> Requests { get; set; }

       
    }
}