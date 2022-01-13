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
            basket = UpdateBasket(basket, 'D', 1);
            DisplayBasket(basket);
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
                Console.WriteLine($"{item.SKU} (£{item.UnitPrice})");
            }
        }
        public List<CheckoutItem> UpdateBasket(List<CheckoutItem> Basket,char SKU,int Quantity)
        {

            if (Basket.Count == 0)
            {
                CheckoutItem item = new CheckoutItem(SKU, Quantity);
                Basket.Add(item);
            }

            return Basket;
        }

    }
}
