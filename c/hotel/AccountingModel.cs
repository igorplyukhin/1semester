using System;
using System.ComponentModel;

namespace HotelAccounting
{
    class AccountingModel : ModelBase
    {
        private double price;

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

        private int nightsCount;

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

        private double discount;

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

        private double total;

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