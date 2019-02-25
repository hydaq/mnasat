using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mnasat.Models
{
    public class TeamMember
    {
        public int ID { get; set; }
        public Usr MemberID { get; set; }
        public Team TeamID { get; set; }
    }
}