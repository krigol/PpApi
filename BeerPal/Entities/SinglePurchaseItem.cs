namespace BeerPal.Entities
{
    public class SinglePurchaseItem : BaseEntity
    {
        public string Description { get; set; }

        public int Price { get; set; } //Always calculate in cents
    }
}
