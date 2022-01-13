using CheckoutKata.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    class Program
    {
        //TODO Add the option for the user to enter their own items
        //TODO Add the option for the user to see their basket, showing the unit price and the nett price (post discounts)
        static void Main(string[] args)
        {
            Program P = new Program();
            P.CreateBasket();
        }
        public void CreateBasket()
        {
            List<CheckoutItem> basket = new List<CheckoutItem>();
            basket = UpdateBasket(basket, 'D', 2);
            basket = UpdateBasket(basket, 'D', 2);
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
                Console.WriteLine($"{item.SKU} - {item.Quantity} (£{item.UnitPrice})");
            }
            var price = CalculatePrice(Basket);
            Console.WriteLine($"Total Price: £{price}");
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
        //Tests - would be better inside their own project in the solution with proper test methods
        public void Test_CanAddToBasket() 
        {
            // Test = 1.	Given I have selected to add an item to the basket Then the item should be added to the basket

            //Arrange
            List<CheckoutItem> basket = new List<CheckoutItem>();
            //Act
            basket = UpdateBasket(basket, 'D', 2);
            //Assert
            if (basket.Count == 1)
            {
                Console.WriteLine("Your basket is not empty -- passed");
            }
            else
            {
                Console.WriteLine("Your basket is empty -- failed");
            }
        }
        public void Test_BasketPriceCalculated()
        {
            // Test = 2.	2.	Given items have been added to the basket Then the total cost of the basket should be calculated

            //Arrange
            List<CheckoutItem> basket = new List<CheckoutItem>();
            //Act
            basket = UpdateBasket(basket, 'A', 1);
            basket = UpdateBasket(basket, 'D', 2);
            //Assert
            DisplayBasket(basket); //Not a proper test result
        }
        public void Test_ValidatePriceOfItemB()  //This test in reality may not be suitable as the discount on 'B' may be ended or prices may change ect 
        {
            // Test = 3.	3.	Given I have added a multiple of 3 lots of item ‘B’ to the basket Then a promotion of ‘3 for 40’ should be applied to every multiple of 3 

            //Arrange
            List<CheckoutItem> basket = new List<CheckoutItem>();
            //Act
            basket = UpdateBasket(basket, 'B', 9);
            var price = CalculatePrice(basket);
            //Assert
            if (price == 120) // (3*40) = 120 
            {
                Console.WriteLine("passed");
            }
            else
            {
                Console.WriteLine("failed");
            }
        }
        public void Test_ValidatePriceOfItemD()  //This test in reality may not be suitable as the discount on 'D' may be ended or prices may change ect 
        {
            // Test = 4.	4.	Given I have added a multiple of 2 lots of item ‘D’ to the basket Then a promotion of ‘25% off’ should be applied to every multiple of 2 

            //Arrange
            List<CheckoutItem> basket = new List<CheckoutItem>();
            //Act
            basket = UpdateBasket(basket, 'D', 5);
            var price = CalculatePrice(basket);
            //Assert
            if (price == 220) // (£82.50 * 2) + 55 == 220 (+55 as its not a pair)
            {
                Console.WriteLine("passed");
            }
            else
            {
                Console.WriteLine("failed");
            }
        }
    }
}
