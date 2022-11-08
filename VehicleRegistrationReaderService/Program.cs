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
                       HostConfig.CertHost = o.CertHost;
                       HostConfig.CertPath = o.CertPath;
                       HostConfig.CertPassword = o.CertPassword;
                       HostConfig.PortNumber = o.PortNumber ?? "61408";
                       HostConfig.PortNumberSsl = o.PortNumberSsl ?? "5002";
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

                        if (!string.IsNullOrEmpty(HostConfig.CertHost))
                        {
                            var host = System.Net.Dns.GetHostEntry(HostConfig.CertHost);

                            options.Listen(host.AddressList[0], Int32.Parse(HostConfig.PortNumber));


                            options.Listen(host.AddressList[0], Int32.Parse(HostConfig.PortNumberSsl), listenOptions =>
                            {
                                listenOptions.UseHttps(httpsOptions =>
                                {
                                    if (!string.IsNullOrEmpty(HostConfig.CertPath) && !string.IsNullOrEmpty(HostConfig.CertPassword))
                                    {
                                        httpsOptions.ServerCertificate = new X509Certificate2(HostConfig.CertPath, HostConfig.CertPassword, X509KeyStorageFlags.MachineKeySet);
                                    }
                                });
                            });
                        }
                        else
                        {
                            var localhost = System.Net.Dns.GetHostEntry("localhost");
                            options.Listen(localhost.AddressList[0], Int32.Parse(HostConfig.PortNumber));
                        }
                    });
                }).UseWindowsService();
    }

    public class Options
    {
        [Option(longName: "cert-host", Required = false, HelpText = "Certificate host.")]
        public string CertHost { get; set; }

        [Option(longName: "cert-path", Required = false, HelpText = "Path to certificate.")]
        public string CertPath { get; set; }

        [Option(longName: "cert-password", Required = false, HelpText = "Certificate password.")]
        public string CertPassword { get; set; }

        [Option(longName: "port-number", Required = false, HelpText = "Custom port number.")]
        public string PortNumber { get; set; }

        [Option(longName: "port-number-ssl", Required = false, HelpText = "Custom port number for https.")]
        public string PortNumberSsl { get; set; }
    }

    public static class HostConfig
    {
        public static string CertHost { get; set; }
        public static string CertPath { get; set; }
        public static string CertPassword { get; set; }
        public static string PortNumber { get; set; }
        public static string PortNumberSsl { get; set; }
    }
}

