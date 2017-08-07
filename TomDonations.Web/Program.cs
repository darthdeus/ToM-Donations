using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace TomDonations.Web {
    public class Program {
        public static void Main(string[] args) {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()                
                .Build();

            Task.Run(() => {
                try {
                    new DonationsBot(host.Services).MainAsync().GetAwaiter().GetResult();
                } catch (Exception e) {
                    Console.WriteLine($"Bot crashed, error:\n{e.Message}\n\n{e.StackTrace}");
                    throw e;
                }
            });

            host.Run();
        }
    }
}