using AsparagusTest.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsparagusTest.Models
{
    public class Food
    {
        public int Id { get; set; }
        public DateTime EatingDate { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}