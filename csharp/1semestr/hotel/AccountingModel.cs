using System;

namespace HotelAccounting
{
    class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double discount;
        private double total;
        
        public double Price
        {
            get => price;
            set
            {
                if (value > 0)
                {
                    price = value;
                    UpdateTotal();
                    Notify(nameof(Price));
                }
                else
                    throw new ArgumentException();
            }
        }

        public int NightsCount
        {
            get => nightsCount;
            set
            {
                if (value > 0)
                {
                    nightsCount = value;
                    UpdateTotal();
                    Notify(nameof(NightsCount));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public double Discount
        {
            get => discount;
            set
            {
                if (Math.Abs(value) <= 100)
                {
                    discount = value;
                    UpdateTotal();
                    Notify(nameof(Discount));
                }
                else
                    throw new ArgumentException();
            }
        }

        public double Total
        {
            get => total;
            set
            {
                if (value >= 0)
                {
                    total = value;
                    UpdateDiscount();
                    Notify(nameof(Total));
                }
                else
                    throw new ArgumentException();
            }
        }

        private void UpdateTotal()
        {
            total = price * nightsCount * (1 - discount / 100);
            Notify(nameof(Total));
        }

        private void UpdateDiscount()
        {
            discount = 100 - (100 * total) / (price * nightsCount);
            Notify(nameof(Discount));
        }
    };
}