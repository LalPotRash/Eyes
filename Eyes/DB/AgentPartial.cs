using System.Linq;

namespace Eyes.DB
{
    public partial class Agent
    {
        public int SellsCount { get => ProductSale.Sum(x => x.ProductCount); }
        public int Discount
        {
            get
            {
                var sum = ProductSale.Sum(x => x.ProductCount * x.Product.MinCostForAgent);
                return sum < 10000 ? 0 : sum < 50000 ? 5 : sum < 150000 ? 10 : sum < 500000 ? 20 : 25;
            }
        }

        public string Background => Discount >= 25 ? "LightGreen" : "#FFE9F9";
    }

}
