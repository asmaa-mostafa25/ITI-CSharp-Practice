
using L2O___D09;
using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // ============================================================
        // LINQ - Restriction Operators
        // ============================================================

        // 1. Find all products that are out of stock.
        var result1 = ListGenerators.ProductList
            .Where(p => p.UnitsInStock == 0);


        // 2. Find all products that are in stock and cost more than 3.00 per unit.
        var result2 = ListGenerators.ProductList
            .Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3);


        // 3. Returns digits whose name is shorter than their value.
        string[] arr =
        {
            "zero", "one", "two", "three", "four",
            "five", "six", "seven", "eight", "nine"
        };

        var result3 = arr
            .Where((word, index) => word.Length < index);


        // ============================================================
        // LINQ - Element Operators
        // ============================================================

        // 1. Get first Product out of Stock
        var result4 = ListGenerators.ProductList
            .First(p => p.UnitsInStock == 0);


        // 2. First product whose Price > 1000,
        //    or null if there is no match.
        var result5 = ListGenerators.ProductList
            .FirstOrDefault(p => p.UnitPrice > 1000);


        // 3. Retrieve the second number greater than 5
        int[] numbers =
        {
            5, 4, 1, 3, 9, 8, 6, 7, 2, 0
        };

        var result6 = numbers
            .Where(x => x > 5)
            .ElementAt(1);


        // ============================================================
        // LINQ - Set Operators
        // ============================================================

        // 1. Find the unique Category names from Product List
        var result7 = ListGenerators.ProductList
            .Select(p => p.Category)
            .Distinct();


        // First letters of product names
        var productLetters = ListGenerators.ProductList
            .Select(p => p.ProductName[0]);


        // First letters of customer names
        var customerLetters = ListGenerators.CustomerList
            .Select(c => c.CompanyName[0]);


        // 2. Unique first letters from products and customers
        var result8 = productLetters
            .Union(customerLetters);


        // 3. Common first letters
        var result9 = productLetters
            .Intersect(customerLetters);


        // 4. Product first letters that are not customer first letters
        var result10 = productLetters
            .Except(customerLetters);


        // 5. Last three characters from all customer and product names
        //    including duplicates
        var result11 = ListGenerators.ProductList
            .Select(p => p.ProductName.Substring(p.ProductName.Length - 3))
            .Concat(
                ListGenerators.CustomerList
                    .Select(c => c.CompanyName.Substring(c.CompanyName.Length - 3))
            );


        // ============================================================
        // LINQ - Aggregate Operators
        // ============================================================

        // 1. Number of odd numbers
        var result12 = numbers
            .Count(x => x % 2 != 0);


        // 2. Customers and number of orders each has
        var result13 = ListGenerators.CustomerList
            .Select(c => new
            {
                CustomerName = c.CompanyName,
                OrderCount = c.Orders.Count()
            });


        // 3. Categories and number of products each has
        var result14 = ListGenerators.ProductList
            .GroupBy(p => p.Category)
            .Select(g => new
            {
                Category = g.Key,
                ProductCount = g.Count()
            });


        // 4. Total of the numbers in an array
        var result15 = numbers.Sum();


        // Read dictionary file
        string[] words = File.ReadAllLines("dictionary_english.txt");


        // 5. Total number of characters of all words
        var result16 = words
            .Sum(w => w.Length);


        // 6. Total units in stock for each product category
        var result17 = ListGenerators.ProductList
            .GroupBy(p => p.Category)
            .Select(g => new
            {
                Category = g.Key,
                TotalUnitsInStock = g.Sum(p => p.UnitsInStock)
            });


        // 7. Length of the shortest word
        var result18 = words
            .Min(w => w.Length);


        // 8. Cheapest price among each category's products
        var result19 = ListGenerators.ProductList
            .GroupBy(p => p.Category)
            .Select(g => new
            {
                Category = g.Key,
                CheapestPrice = g.Min(p => p.UnitPrice)
            });


        // 9. Products with the cheapest price in each category (Use Let)
        var result20 =
            from p in ListGenerators.ProductList
            group p by p.Category into g
            let minPrice = g.Min(p => p.UnitPrice)
            from p in g
            where p.UnitPrice == minPrice
            select p;


        // 10. Length of the longest word
        var result21 = words
            .Max(w => w.Length);


        // 11. Most expensive price among each category's products
        var result22 = ListGenerators.ProductList
            .GroupBy(p => p.Category)
            .Select(g => new
            {
                Category = g.Key,
                MostExpensivePrice = g.Max(p => p.UnitPrice)
            });


        // 12. Products with the most expensive price in each category
        var result23 =
            from p in ListGenerators.ProductList
            group p by p.Category into g
            let maxPrice = g.Max(p => p.UnitPrice)
            from p in g
            where p.UnitPrice == maxPrice
            select p;


        // 13. Average length of the words
        var result24 = words
            .Average(w => w.Length);


        // 14. Average price of each category's products
        var result25 = ListGenerators.ProductList
            .GroupBy(p => p.Category)
            .Select(g => new
            {
                Category = g.Key,
                AveragePrice = g.Average(p => p.UnitPrice)
            });


        // ============================================================
        // LINQ - Ordering Operators
        // ============================================================

        // 1. Sort products by name
        var result26 = ListGenerators.ProductList
            .OrderBy(p => p.ProductName);


        // 2. Case-insensitive sort
        string[] arr2 =
        {
            "aPPLE", "AbAcUs", "bRaNcH",
            "BlUeBeRrY", "ClOvEr", "cHeRry"
        };

        var result27 = arr2
            .OrderBy(x => x, StringComparer.OrdinalIgnoreCase);


        // 3. Sort products by units in stock from highest to lowest
        var result28 = ListGenerators.ProductList
            .OrderByDescending(p => p.UnitsInStock);


        // 4. Sort digits first by length,
        //    then alphabetically
        string[] arr3 =
        {
            "zero", "one", "two", "three", "four",
            "five", "six", "seven", "eight", "nine"
        };

        var result29 = arr3
            .OrderBy(x => x.Length)
            .ThenBy(x => x);


        // 5. Sort first by word length,
        //    then case-insensitive
        string[] words1 =
        {
            "aPPLE", "AbAcUs", "bRaNcH",
            "BlUeBeRrY", "ClOvEr", "cHeRry"
        };

        var result30 = words1
            .OrderBy(w => w.Length)
            .ThenBy(w => w, StringComparer.OrdinalIgnoreCase);


        // 6. Sort products first by category,
        //    then unit price highest to lowest
        var result31 = ListGenerators.ProductList
            .OrderBy(p => p.Category)
            .ThenByDescending(p => p.UnitPrice);


        // 7. Sort by word length,
        //    then case-insensitive descending
        var result32 = words1
            .OrderBy(w => w.Length)
            .ThenByDescending(w => w, StringComparer.OrdinalIgnoreCase);


        // 8. Digits whose second letter is 'i',
        //    reversed from original order
        var result33 = arr3
            .Where(x => x[1] == 'i')
            .Reverse();


        // ============================================================
        // LINQ - Partitioning Operators
        // ============================================================

        // 1. Get first 3 orders from customers in Washington
        var result34 = ListGenerators.CustomerList
            .Where(c => c.Region == "WA")
            .SelectMany(c => c.Orders)
            .Take(3);


        // 2. Get all but the first 2 orders from customers in Washington
        var result35 = ListGenerators.CustomerList
            .Where(c => c.Region == "WA")
            .SelectMany(c => c.Orders)
            .Skip(2);


        // 3. Elements from beginning until a number
        //    is less than its position
        var result36 = numbers
            .TakeWhile((x, index) => x >= index);


        // 4. Elements starting from first number divisible by 3
        var result37 = numbers
            .SkipWhile(x => x % 3 != 0);


        // 5. Elements starting from first element
        //    that is less than its position
        var result38 = numbers
            .SkipWhile((x, index) => x >= index);


        // ============================================================
        // LINQ - Projection Operators
        // ============================================================

        // 1. Sequence of only product names
        var result39 = ListGenerators.ProductList
            .Select(p => p.ProductName);


        // 2. Uppercase and lowercase versions
        var words2 =
            new string[]
            {
                "aPPLE", "BlUeBeRrY", "cHeRry"
            };

        var result40 = words2
            .Select(w => new
            {
                Upper = w.ToUpper(),
                Lower = w.ToLower()
            });


        // 3. Some Product properties,
        //    UnitPrice renamed to Price
        var result41 = ListGenerators.ProductList
            .Select(p => new
            {
                p.ProductName,
                p.Category,
                Price = p.UnitPrice
            });


        // 4. Determine if numbers match their position
        var result42 = numbers
            .Select((number, index) => new
            {
                Number = number,
                InPlace = number == index
            });


        // 5. All pairs where number from A is less than number from B
        int[] numbersA =
        {
            0, 2, 4, 5, 6, 8, 9
        };

        int[] numbersB =
        {
            1, 3, 5, 7, 8
        };

        var result43 =
            from a in numbersA
            from b in numbersB
            where a < b
            select new
            {
                A = a,
                B = b
            };


        // 6. Select all orders where total < 500
        var result44 = ListGenerators.CustomerList
            .SelectMany(c => c.Orders)
            .Where(o => o.Total < 500.00m);


        // 7. Select all orders made in 1998 or later
        var result45 = ListGenerators.CustomerList
            .SelectMany(c => c.Orders)
            .Where(o => o.OrderDate.Year >= 1998);


        // ============================================================
        // LINQ - Quantifiers
        // ============================================================

        // 1. Determine if any word contains "ei"
        var result46 = words
            .Any(w => w.Contains("ei"));


        // 2. Categories that have at least one out-of-stock product
        var result47 = ListGenerators.ProductList
            .GroupBy(p => p.Category)
            .Where(g => g.Any(p => p.UnitsInStock == 0));


        // 3. Categories where all products are in stock
        var result48 = ListGenerators.ProductList
            .GroupBy(p => p.Category)
            .Where(g => g.All(p => p.UnitsInStock > 0));


        // ============================================================
        // LINQ - Grouping Operators
        // ============================================================

        // 1. Group numbers by remainder when divided by 5
        int[] groupingNumbers =
        {
            0, 1, 2, 3, 4,
            5, 6, 7, 8, 9,
            10, 11, 12, 13, 14
        };

        var result49 = groupingNumbers
            .GroupBy(x => x % 5);


        // 2. Group words by their first letter
        var result50 = words
            .GroupBy(w => w[0]);


        // 3. Group words that consist of the same characters
        string[] arr4 =
        {
            "from",
            "salt",
            "earn",
            "last",
            "near",
            "form"
        };

        var result51 = arr4
            .GroupBy(w => w, new AnagramComparer());


        // ============================================================
        // Display examples
        // ============================================================

        Console.WriteLine("LINQ Questions Completed Successfully!");
    }


    // ================================================================
    // Custom Comparer for Grouping Anagrams
    // ================================================================

    class AnagramComparer : IEqualityComparer<string>
    {
        public bool Equals(string? x, string? y)
        {
            if (x == null || y == null)
                return x == y;

            return string.Concat(x.OrderBy(c => c))
                .Equals(string.Concat(y.OrderBy(c => c)));
        }

        public int GetHashCode(string obj)
        {
            return string.Concat(obj.OrderBy(c => c))
                .GetHashCode();
        }
    }
}

