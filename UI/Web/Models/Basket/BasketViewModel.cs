namespace Web.Models.Basket
{
    public class BasketViewModel
    {
        public BasketViewModel()
        {
            _basketItems = new List<BasketItemViewModel>();
        }
        public string? UserId { get; set; }
        public int? DiscountRate { get; set; }
        public string? DiscountCode { get; set; }       

        private List<BasketItemViewModel> _basketItems { get; set; }

        public List<BasketItemViewModel> BasketItems
        {
            get
            {
                if (HasDiscount)
                {
                    //Örnek kurs fiyat 100 TL indirim %10
                    _basketItems.ForEach(x =>
                    {
                        var discountPrice = x.Price * ((decimal)DiscountRate.Value / 100);
                        x.AppliedDiscount(Math.Round(x.Price - discountPrice, 2));
                    });
                }
                return _basketItems;
            }
            set
            {
                _basketItems = value;
            }
        }
        public decimal TotalPrice
        {
             get => BasketItems.Sum(x => x.Price * x.Quantity);
            //get => _basketItems.Sum(x => x.GetCurrentPrice);
        }
        public bool HasDiscount
        {
            get => !string.IsNullOrEmpty(DiscountCode);
        }

      

    }
}
