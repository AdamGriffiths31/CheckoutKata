using CheckoutKata.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    class Program
    {
        static void Main(string[] args)
        {
            Program P = new Program();
            P.CreateBasket();
        }
        public void CreateBasket()
        {
            List<CheckoutItem> basket = new List<CheckoutItem>();
            basket = UpdateBasket(basket, 'D', 2);
            basket = UpdateBasket(basket, 'B', 3);
            DisplayBasket(basket);
            var price= CalculatePrice(basket);
            Console.WriteLine($"Price: £{price}");
            Console.ReadLine();
        }
        public void DisplayBasket(List<CheckoutItem> Basket)
        {
            if (Basket == null)
            {
                Console.WriteLine("Your basket is null");
            }
            if (Basket.Count == 0)
            {
                Console.WriteLine("Your basket is empty");
            }
            foreach(var item in Basket)
            {
                Console.WriteLine($"{item.SKU} - {item.Quantity} (£{item.UnitPrice})");
            }
        }
        public List<CheckoutItem> UpdateBasket(List<CheckoutItem> Basket,char SKU,int Quantity)
        {
            //TODO The ability to subtract and remove an item from the basket
            //TODO List probably isn't the best choice for a proper implementation - invetigate other options e.g. hashmap?

            CheckoutItem item = new CheckoutItem(SKU, Quantity);
            if (Basket.Count == 0)
            {
                Basket.Add(item);
                return Basket;
            }
            if (Basket.Any(x => x.SKU == SKU))
            {
                Basket.Where(i => i.SKU == SKU).ToList().ForEach(s => s.Quantity = Quantity+s.Quantity);
                return Basket;
            }
            Basket.Add(item);
            return Basket;
        }
        public double CalculatePrice(List<CheckoutItem> Basket)
        {
            if (Basket == null)
            {
                return 0;
            }
            if (Basket.Count==0)
            {
                return 0;
            }
            double result = 0;

            //A = no discount
            //B = 3 for 40  -- foreach batch of 3 its -5 off the total price
            //C = no dscount
            //D = 25% off for a pair 
            foreach(var itemGroup in Basket)
            {
                switch (itemGroup.SKU)
                {
                    case 'B':
                        if (itemGroup.Quantity < 3) //No discount can be applied (1-2 range)
                        {
                            result += (itemGroup.Quantity * itemGroup.UnitPrice);
                        }
                        int batchesOfThree = itemGroup.Quantity / 3;
                        int remainder = itemGroup.Quantity % 3;
                        result += (batchesOfThree * 40);
                        result += (remainder * itemGroup.UnitPrice);
                        break;
                    case 'D':
                        if (itemGroup.Quantity < 2) //No discount can be applied (1 range)
                        {
                            result += (itemGroup.Quantity * itemGroup.UnitPrice);
                        }
                        int batchesOfTwo = itemGroup.Quantity / 2;
                        remainder = itemGroup.Quantity % 2;
                        double discountedPrice = (itemGroup.UnitPrice * 2) * 0.75;
                        result += (batchesOfTwo * discountedPrice);
                        result += (remainder * itemGroup.UnitPrice);
                        break;
                    default:
                        result += (itemGroup.Quantity * itemGroup.UnitPrice);
                        break;
                }
            }
            return result;
        }

    }
}
