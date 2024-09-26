
namespace admin_panel2.Models
{
    internal class Bought
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Product> Products { get; set; }
        public double TotalAmount { get; set; }
    }
}
