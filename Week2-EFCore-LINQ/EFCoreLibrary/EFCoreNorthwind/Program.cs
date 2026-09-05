
using EFCoreNorthwind.Models;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        var context = new EFCoreNorthwind.Models.DESKTOPMS535M4SQLEXPRESSContext();

        // =========================================================
        // 1. Filtering
        // Get Products where UnitPrice is greater than 50
        // =========================================================

        var products = context.Products
            .Where(p => p.UnitPrice > 50)
            .ToList();

        Console.WriteLine("Products with price greater than $50:");

        foreach (var product in products)
        {
            Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
        }


        // =========================================================
        // 2. Eager Loading
        // Get a specific Customer with all their Orders
        // =========================================================

        var customerWithOrders = context.Customers
            .Include(c => c.Orders)
            .FirstOrDefault(c => c.CustomerId == "ALFKI");

        if (customerWithOrders != null)
        {
            Console.WriteLine("\nCustomer and Orders:");

            Console.WriteLine($"Customer: {customerWithOrders.CompanyName}");

            foreach (var order in customerWithOrders.Orders)
            {
                Console.WriteLine(
                    $"Order ID: {order.OrderId}, Order Date: {order.OrderDate}"
                );
            }
        }


        // =========================================================
        // 3. Projection
        // Get Employee name and number of Orders
        // =========================================================

        var employees = context.Employees
            .Select(e => new
            {
                EmployeeName = e.FirstName + " " + e.LastName,
                OrderCount = e.Orders.Count
            })
            .ToList();

        Console.WriteLine("\nEmployees and Order Count:");

        foreach (var employee in employees)
        {
            Console.WriteLine(
                $"Employee: {employee.EmployeeName}, Order Count: {employee.OrderCount}"
            );
        }


        // =========================================================
        // 4. Sorting
        // Sort Customers alphabetically by CompanyName
        // =========================================================

        var customers = context.Customers
            .OrderBy(c => c.CompanyName)
            .ToList();

        Console.WriteLine("\nCustomers sorted alphabetically:");

        foreach (var customer in customers)
        {
            Console.WriteLine($"Customer: {customer.CompanyName}");
        }
    }
}

