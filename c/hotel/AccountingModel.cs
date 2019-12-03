using System;

namespace HotelAccounting
{
    class AccountingModel : ModelBase
    {
        private double Price
        {
            get { return Price; }
            set
            {
                if (value > 0)
                {
                    Price = value;
                    Notify(nameof(Price));
                }
                else
                {
                    
                }
                {
                    throw new ArgumentException();
                }
            }
        }

        private int NightsCount
        {
            get { return NightsCount; }
            set
            {
                if (value >= 0)
                {
                    NightsCount = value;
                    Notify(nameof(NightsCount));
                }
            }
        }

        private double Discount
        {
            get { return Discount;}
            set
            {
                Discount = value;
                Notify(nameof(Discount));
            }
        }

        private double Total
        {
            get { return Total; }
            set
            {
                Discount = Total * NightsCount * (1 - Price / 100);
                Notify(nameof(Total));
            }
        }
        
    };
}