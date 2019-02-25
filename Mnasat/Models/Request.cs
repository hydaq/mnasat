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
        public Usr Customer { get; set; }
        public Usr Admin { get; set; }
        public DateTime AssigningDate { get; set; }
        public Team AssignedTeam { get; set; }
        public Usr HandlingEmployee { get; set; }
        public DateTime HandledDate { get; set; }
    }
}