// Program.cs
using CsvHelper;
using System.Globalization;
using System.Text;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Lumel_Assessment.Models;
using Lumel_Assesment.Models;
using Microsoft.Extensions.Logging;

SalesData salesData = new SalesData();
await salesData.ImportSalesData("");
public class SalesData()
{
    ILogger _logger;
    public async Task ImportSalesData(string filePath)
    {
        try
        {
            var db = new SalesDbContext();

            filePath = "C:\\Users\\muthu\\Downloads\\sales.csv"; // Path to your file

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.Trim(),
                HeaderValidated = null,
                MissingFieldFound = null,
            };

            using var reader = new StreamReader(filePath, Encoding.UTF8);
            using var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<SaleCsvRecord>().ToList();

            foreach (var r in records)
            {
                // Get or Add Customer
                var customer = await db.Customers.FirstOrDefaultAsync(c => c.CustomerId == r.CustomerID);
                if (customer == null)
                {
                    customer = new Customer
                    {
                        CustomerId = r.CustomerID,
                        Name = r.CustomerName,
                        Email = r.CustomerEmail,
                        Address = r.CustomerAddress
                    };
                    db.Customers.Add(customer);
                    await db.SaveChangesAsync();
                }

                // Get or Add Product
                var product = await db.Products.FirstOrDefaultAsync(p => p.ProductId == r.ProductID);
                if (product == null)
                {
                    product = new Product
                    {
                        ProductId = r.ProductID,
                        Name = r.ProductName,
                        Category = r.Category
                    };
                    db.Products.Add(product);
                    await db.SaveChangesAsync();
                }

                // Get or Add Order
                var order = await db.Orders.FirstOrDefaultAsync(o => o.OrderId == r.OrderID);
                if (order == null)
                {
                    order = new Order
                    {
                        OrderId = r.OrderID,
                        CustomerRefId = r.CustomerID,
                        DateOfSale = r.DateOfSale,
                        PaymentMethod = r.PaymentMethod,
                        Region = r.Region,
                        ShippingCost = r.ShippingCost,
                        CustomerId = customer.Id
                    };
                    db.Orders.Add(order);
                    await db.SaveChangesAsync();
                }

                // Add OrderDetail
                var orderDetail = new OrderDetail
                {
                    OrderRefId = Convert.ToInt32(r.OrderID),
                    ProductRefId = r.ProductID,
                    QuantitySold = r.QuantitySold,
                    UnitPrice = r.UnitPrice,
                    Discount = r.Discount,
                    ProductId = product.Id,
                    OrderId = order.Id
                };
                db.OrderDetails.Add(orderDetail);
            }

            await db.SaveChangesAsync();
            Console.WriteLine("CSV import completed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Data import failed at {Time}", DateTime.UtcNow);
        }
    }

}




