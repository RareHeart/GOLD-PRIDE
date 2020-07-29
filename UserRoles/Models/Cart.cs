using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace UserRoles.Models
{
    public class Cart
    {
        [Key]
        public int RecordID { get; set; }
        public string CartID { get; set; }
        public int ProductID { get; set; }
        public byte [] Image { get; set; }
        [Required(AllowEmptyStrings = true, ErrorMessage = " ")]
        [Range(0, 100, ErrorMessage = "Quantity must be between 0 and 100")]
        [DisplayName("Quantity")]
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }
        public virtual ItemsHire ItemsHire { get; set; }
    }
}