using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NDViet.UT.WS.AppConsole.Orders;
using NDViet.UT.WS.AppConsole.Payment;
using NDViet.UT.WS.AppConsole.Products;
using NDViet.UT.WS.AppConsole.Sessions;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static NDViet.UT.WS.AppConsole.Orders.Order;

namespace MISA.UT.WS.AppConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;
      
            var host = CreateHostBuilder(args).Build();
            
            var order = new Order()
            {
                CardId = new Guid("26d2ade9-95c4-42d4-adae-7ffb803ce8e6"),
                Details = new List<Detail>()
                {

                        new Detail()
                        {
                            ProductID = Guid.NewGuid(),
                            Product = "Huong duong",
                            Price = 1000,
                            Quantity = 10
                        },
                        new Detail()
                        {
                            ProductID = Guid.NewGuid(),
                            Product = "Dau phong",
                            Price = 2500,
                            Quantity = 2
                        }


                }
            };
            host.Services.GetService<IOrderService>().Create(order);
            Console.Read();
        }



        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builderConfig = new ConfigurationBuilder();
            builderConfig.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();
            var builderConfigData = builderConfig.Build();
            Log.Logger = new LoggerConfiguration() // initiate the logger configuration
                   .ReadFrom.Configuration(builderConfigData) // connect serilog to our configuration folder
                   .Enrich.FromLogContext() //Adds more information to our logs from built in Serilog 
                   .WriteTo.Console() // decide where the logs are going to be shown
                   .CreateLogger(); //initialise the logger
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddConfiguration(builderConfigData);
                })
                .ConfigureServices((context, services) =>
                {
                    //add your service registrations
                    services.AddSingleton<IProductService, ProductService>();
                    services.AddSingleton<IOrderService, OrderService>();
                    services.AddSingleton<IOrderRepository, OrderRepository>();
                    services.AddSingleton<ISessionService, SessionService>();
                    services.AddSingleton<IPaymentService, PaymentService>();

                }).UseSerilog();
           
            return hostBuilder;
        }
    }

}
