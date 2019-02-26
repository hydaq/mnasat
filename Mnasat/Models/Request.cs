using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mnasat.Models
{
    public enum RequestState { Posted,Assigned,Handled }
    public class Request
    {
        public int RequestID { get; set; }
        public String RequestDescription { get; set; }
        public RequestState? CurrentState { get; set; }
        public DateTime RequestDate { get; set; }
        public int Customer { get; set; }
        public String CustomerName { get; set; }
        public int Admin { get; set; }
        public DateTime AssigningDate { get; set; }
        public String AssignedTeamName { get; set; }
        public int AssignedTeam { get; set; }
        public int HandlingEmployee { get; set; }
        public String HandlingEmployeeName { get; set; }
        public DateTime HandledDate { get; set; }
    }
}