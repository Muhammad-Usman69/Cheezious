namespace Cheezious.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Image { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

        public DateTime AddedOn { get; set; }

        public virtual ProductStatus ProductStatus { get; set; }

        public int ProductStatusId { get; set; }

        public virtual Category Category { get; set; }

        public int CategoryId { get; set; }

    }
}
