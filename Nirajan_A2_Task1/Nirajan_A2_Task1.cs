using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Class representing a customer and their wiring requirements
class Customer
{
    // Properties to store customer details
    public string Name { get; set; }
    public string BuildingType { get; set; }
    public int Size { get; set; }
    public int LightBulbs { get; set; }
    public int Outlets { get; set; }
    public string CreditCard { get; set; }

    // Constructor to initialize customer details
    public Customer(string name, string buildingType, int size, int lightBulbs, int outlets, string creditCard)
    {
        Name = name;
        BuildingType = buildingType;
        Size = size;
        LightBulbs = lightBulbs;
        Outlets = outlets;
        CreditCard = creditCard;
    }

    // Method to simulate creating a wiring schema
    public void CreateWiringSchema()
    {
        Console.WriteLine($"Creating wiring schema for {Name}'s {BuildingType}.");
    }

    // Method to simulate purchasing required parts
    public void PurchaseParts()
    {
        Console.WriteLine($"Purchasing parts for {Name}'s {BuildingType}.");
    }

    // Method to perform building-specific tasks
    public void PerformSpecialTask()
    {
        switch (BuildingType.ToLower())
        {
            case "house":
                Console.WriteLine("Installing fire alarms.");
                break;
            case "barn":
                Console.WriteLine("Wiring milking equipment.");
                break;
            case "garage":
                Console.WriteLine("Installing automatic doors.");
                break;
        }
    }

    // Method to mask the credit card, showing only the first and last 4 digits
    public string MaskCreditCard()
    {
        return CreditCard.Substring(0, 4) + " XXXX XXXX " + CreditCard.Substring(12, 4);
    }

    // Method to display customer information in a formatted way
    public void DisplayCustomerInfo()
    {
        Console.WriteLine($"{Name}, {BuildingType}, {Size} sq.ft, {LightBulbs} bulbs, {Outlets} outlets, CC: {MaskCreditCard()}");
    }
}

class Program
{
    static void Main()
    {
        // List to store multiple customers
        List<Customer> customers = new List<Customer>();
        bool firstCustomerEntered = false;

        while (true)
        {
            // Customer name with validation
            string customerName;
            do
            {
                Console.Write("Enter customer name (or 'done' to finish): ");
                customerName = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(customerName) || Regex.IsMatch(customerName, "^\\d+$"))
                {
                    Console.WriteLine("Invalid name. Please enter a valid name containing letters.");
                }
                else if (customerName.ToLower() == "done" && !firstCustomerEntered)
                {
                    Console.WriteLine("You must enter at least one customer before finishing.");
                    customerName = ""; // Reset to force loop continuation
                }
                else if (customerName.ToLower() == "done")
                {
                    break;
                }
            } while (string.IsNullOrEmpty(customerName) || Regex.IsMatch(customerName, "^\\d+$"));

            if (customerName.ToLower() == "done") break; // Exit if "done" is entered after at least one valid entry

            firstCustomerEntered = true; // Mark that at least one customer has been entered

            // Building type with validation
            string buildingType;
            do
            {
                Console.Write("Enter building type (house, barn, garage): ");
                buildingType = Console.ReadLine().Trim().ToLower();

                if (string.IsNullOrEmpty(buildingType) ||
                    (buildingType != "house" && buildingType != "barn" && buildingType != "garage"))
                {
                    Console.WriteLine("Invalid building type. Please enter 'house', 'barn', or 'garage'.");
                }
            } while (string.IsNullOrEmpty(buildingType) ||
                     (buildingType != "house" && buildingType != "barn" && buildingType != "garage"));

            // Validating and inputting building size
            int buildingSize;
            do
            {
                Console.Write("Enter size (1000 - 50000 sq.ft): ");
                if (!int.TryParse(Console.ReadLine(), out buildingSize) || buildingSize < 1000 || buildingSize > 50000)
                {
                    Console.WriteLine("Invalid size. Please enter a value between 1000 and 50000 sq.ft.");
                }
            } while (buildingSize < 1000 || buildingSize > 50000);

            // Validating and inputting number of light bulbs
            int lightBulbCount;
            do
            {
                Console.Write("Enter number of light bulbs (max 20): ");
                if (!int.TryParse(Console.ReadLine(), out lightBulbCount) || lightBulbCount < 0 || lightBulbCount > 20)
                {
                    Console.WriteLine("Invalid number. Please enter a value between 0 and 20 light bulbs.");
                }
            } while (lightBulbCount < 0 || lightBulbCount > 20);

            // Validating and inputting number of outlets
            int outletCount;
            do
            {
                Console.Write("Enter number of outlets (max 50): ");
                if (!int.TryParse(Console.ReadLine(), out outletCount) || outletCount < 0 || outletCount > 50)
                {
                    Console.WriteLine("Invalid number. Please enter a value between 0 and 50 outlets.");
                }
            } while (outletCount < 0 || outletCount > 50);

            // Validating credit card input (must be 16 digits and numeric)
            string creditCardNumber;
            do
            {
                Console.Write("Enter credit card number (16-digit): ");
                creditCardNumber = Console.ReadLine();
                if (creditCardNumber.Length != 16 || !long.TryParse(creditCardNumber, out _))
                {
                    Console.WriteLine("Invalid credit card number. Please enter exactly 16 numeric digits.");
                }
            } while (creditCardNumber.Length != 16 || !long.TryParse(creditCardNumber, out _));

            // Creating a new customer object and adding it to the list
            Customer customer = new Customer(customerName, buildingType, buildingSize, lightBulbCount, outletCount, creditCardNumber);
            customers.Add(customer);
        }

        // Displaying customer details and performing their required tasks
        Console.WriteLine("\nCustomer details:");
        foreach (var customer in customers)
        {
            customer.CreateWiringSchema();
            customer.PurchaseParts();
            customer.PerformSpecialTask();
            customer.DisplayCustomerInfo();
            Console.ReadLine();
        }
    }
}
