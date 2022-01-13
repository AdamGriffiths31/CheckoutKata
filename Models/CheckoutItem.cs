using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutKata.Models
{
    class CheckoutItem
    {
        public char SKU { get; set; } //Char works for the current examples but will cause issues after z.
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }

        public CheckoutItem(char productSKU, int ProductQuantity)
        {
            SKU = productSKU;
            Quantity = ProductQuantity;
            switch (SKU)
            {
                case 'A':
                    UnitPrice = 10;
                    break;
                case 'B':
                    UnitPrice = 15;
                    break;
                case 'C':
                    UnitPrice = 40;
                    break;
                case 'D':
                    UnitPrice = 55;
                    break;
            }
        }

    }
}
