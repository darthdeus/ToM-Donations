using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace TomDonations.Web {
    public class Points : ModuleBase {
        private readonly DatabaseService _database;

        public Points(DatabaseService database) {
            _database = database;
        }

        // !addpoints copper ore 100 darthdeus
        [Command("addpoints"), Summary("Awards points to a player based on donated resources")]
        public async Task AddPoints([Summary("Tier of the item")] string tier,
            [Summary("Resource type")] string type,
            [Summary("Donated amount")] int amount,
            [Remainder, Summary("Player name")] string player) {
            try {
                var parsed = ParsePoints(tier, type, amount);

                if (parsed.IsOk) {
                    if (parsed.Amount.HasValue) {
                        await AddPointsToPlayer(player, parsed.Amount.Value);
                    } else {
                        await ReplyAsync("Something went wrong when parsing the command.");
                    }
                } else {
                    await ReplyAsync($"ERROR: ${parsed.ErrorMessage}");
                }
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        [Command("addbonuspoints")]
        public async Task AddBonusPoints([Summary("Amount of points to award")] int points,
            [Remainder, Summary("Player name")] string player) {
            await AddPointsToPlayer(player, points);
        }

        [Command("querypoints")]
        public async Task QueryPoints(string player) {
            int? points = _database.QueryPoints(player);

            if (points.HasValue) {
                await ReplyAsync($"Player '{player}' has {points} points");
            } else {
                await PlayerDoesNotExist(player);
            }
        }

        private async Task AddPointsToPlayer(string player, int points) {
            if (_database.Award(player, points)) {
                int? currentPoints = _database.QueryPoints(player);

                if (currentPoints.HasValue) {
                    await ReplyAsync(
                        $"The player '{player}' was awarded {points} points. They now have {currentPoints.Value} points total.");
                } else {
                    await ReplyAsync("Something went wrong.");
                }
            } else {
                await PlayerDoesNotExist(player);
            }
        }

        private async Task PlayerDoesNotExist(string player) {
            await ReplyAsync($"Player '{player}' doesn't exist");
        }

        private PointParseResult ParsePoints(string tier, string type, int amount) {
            int? tierNumber = ResourceTable.Tier(tier);
            float? typeMultiplier = ResourceTable.PointMultiplier(type);

            if (tierNumber.HasValue) {
                if (typeMultiplier.HasValue) {
                    int totalPoints = (int) Math.Round(amount * tierNumber.Value * typeMultiplier.Value);
                    return PointParseResult.Success(totalPoints);
                } else {
                    string validMultipliers = string.Join(",", ResourceTable.MultiplierMapping.Select(p => p.Key));
                    return PointParseResult.Error(
                        $"Invalid value of item type '{type}', valid values are: {validMultipliers}");
                }
            } else {
                string validTiers = string.Join(",", ResourceTable.TierMapping.Select(p => p.Key));
                return PointParseResult.Error($"Invalid value of item tier '{tier}', valid values are: {validTiers}");
            }
        }
    }
}