using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using UserRoles.Models;

namespace UserRoles.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
       // public byte[] Image { get; set; }
        public decimal CartTotal { get; set; }
    }
}