namespace UserRoles.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual ItemsHire ItemsHire { get; set; }
        public virtual Order Order { get; set; }
    }
}