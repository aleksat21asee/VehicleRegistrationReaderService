using CommandLine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using VehicleRegistrationReaderService;

namespace IdCardReaderService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(o =>
                   {
                       HostConfig.PortNumber = o.PortNumber ?? "61408";
                   });

            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    webBuilder.ConfigureKestrel(options =>
                    {
                            var localhost = System.Net.Dns.GetHostEntry("localhost");
                            options.Listen(localhost.AddressList[0], Int32.Parse(HostConfig.PortNumber));
                    });
                }).UseWindowsService();
    }

    public class Options
    {
        [Option(longName: "port-number", Required = false, HelpText = "Custom port number.")]
        public string PortNumber { get; set; }
    }

    public static class HostConfig
    {
        public static string PortNumber { get; set; }
    }
}