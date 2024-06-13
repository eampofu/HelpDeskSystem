﻿using System.ComponentModel.DataAnnotations;

namespace HelpDeskSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public  string Priority { get; set; }
        [Display(Name ="Created By")]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public DateTime CreatedOn { get; set;}
    }
}
