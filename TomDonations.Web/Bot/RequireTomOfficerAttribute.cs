using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace TomDonations.Web {
    public class RequireTomOfficerAttribute : PreconditionAttribute {
        public async override Task<PreconditionResult> CheckPermissions(ICommandContext context, CommandInfo command, IServiceProvider services) {
            Console.WriteLine($"Username: {context.User.Username}, Discriminator: {context.User.Discriminator}, DValue: {context.User.DiscriminatorValue}");

            if (context.User.Username == "Jay.Cee") {
                return PreconditionResult.FromSuccess();
            } else {
                return PreconditionResult.FromError("You are not allowed to modify donation points.");
            }
        }
    }
}