using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mnasat.Models
{
    public class DbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MnasatDb>
    {
        protected override void Seed(MnasatDb context)
        {
            var Usrs = new List<Usr>
            {
                new Usr { Username="Admin",Password="Admin",Privilege= Privileges.Admin},
                new Usr { Username="Employee",Password="Emp",Privilege= Privileges.Employee},
                new Usr { Username="Employee2",Password="Emp",Privilege= Privileges.Employee},
                new Usr { Username="Customer",Password="Customer",Privilege= Privileges.Customer},
            };

            Usrs.ForEach(input => context.Usrs.Add(input));
            context.SaveChanges();
            var Teams = new List<Team>
            {
                new Team {TeamName="Eagles" },
                new Team {TeamName="Falcons" },
                new Team {TeamName="Fighters" }
            };
            Teams.ForEach(input1 => context.Teams.Add(input1));
            context.SaveChanges();

            var TeamMembers = new List<TeamMember>
            {
                new TeamMember {MemberID=2,TeamID=1 },
                new TeamMember {MemberID=2,TeamID=2 },
                new TeamMember {MemberID=3,TeamID=1 },
                new TeamMember {MemberID=3,TeamID=3 }
            };
            TeamMembers.ForEach(input2 => context.TeamMembers.Add(input2));
            context.SaveChanges();
        }
    }
}