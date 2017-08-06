using Discord;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using TomDonations;

namespace Donations {
    class Program {

        public static void Main(string[] args) {

            new Program().MainAsync().GetAwaiter().GetResult();
        }

    }
}