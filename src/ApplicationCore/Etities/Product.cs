namespace ApplicationCore.Etities
{
    public class Product : BaseEntity
    {

        public string ProductName { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; } // dosyanın yolunu tutacağız.

        public int CategoryId { get; set; }
        public Category category { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

    }
}
