using EventBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;
using UserRoles.ViewModels;

namespace UserRoles.Controllers
{
    //[Authorize(Roles = "User")]

    public class ShoppingCartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
            //return View();
        }
        //
        [Authorize]        
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            
            // Retrieve the album from the database
            var addedItem = db.ItemsHires
                .Single(itemsHire => itemsHire.ProductID == id);
           

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedItem);

            // Go back to the main store page for more shopping
            return RedirectToAction("ItemsInventory","ItemsHires");
        }

       

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string itemName = db.Carts
                .Single(item => item.RecordID == id).ItemsHire.ProductName;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(itemName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        

        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}