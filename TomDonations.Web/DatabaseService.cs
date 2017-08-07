using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LiteDB;

namespace TomDonations.Web {
    public class DatabaseService {
        private readonly object _lock = new object();
        private readonly LiteDatabase _db;

        private static DatabaseService _instance;

        public static DatabaseService Instance => _instance ?? (_instance = new DatabaseService());

        private DatabaseService() {
            _db = new LiteDatabase(@"data.db");
        }

        public bool Award(string player, int points) {
            lock (_lock) {
                var member = FindByName(player);

                if (member == null) {
                    var newMember = new GuildMember(player, points);
                    GuildMembers().Insert(newMember);
                } else {
                    member.Points += points;
                    GuildMembers().Update(member);
                }

                return true;
            }
        }

        public int? QueryPoints(string player) {
            lock (_lock) {
                return FindByName(player)?.Points;
            }
        }

        public IEnumerable<GuildMember> AllMembers() {
            return GuildMembers().FindAll();
        }

        private LiteCollection<GuildMember> GuildMembers() {
            return _db.GetCollection<GuildMember>();
        }

        private GuildMember FindByName(string name) {
            return Enumerable.FirstOrDefault<GuildMember>(GuildMembers().Find(member => member.Name == name));
        }
    }
}