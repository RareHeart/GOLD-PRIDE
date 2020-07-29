using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EventBook.Models
{
    public class Event
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }


        public string EventType { get; set; }

        public string Fname { get; set; }

        public System.DateTime Start { get; set; }

        public System.DateTime End { get; set; }
        public string contactNum { get; set; }
    }
}