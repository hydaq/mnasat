using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mnasat.Models
{
    public enum Privilege { Admin,Employee,Customer}
    public class Usr
    {
        public int UsrID { get; set; }

        [StringLength(20)]
        [Index(IsUnique = true)]
        public String Username { get; set; }
        public String Password { get; set; }
        public Privilege? Privilges { get; set; }
    }
}