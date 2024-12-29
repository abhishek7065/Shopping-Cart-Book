namespace Shopping_Cart_Book.Models.DTOs
{
    public class OrderDetailModelDto
    {
        public string DivId { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
    }
}
