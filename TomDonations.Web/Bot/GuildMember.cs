using LiteDB;

namespace TomDonations.Web {
    public class GuildMember {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }

        public GuildMember() {
        }

        public GuildMember(string name, int points) {
            Name = name;
            Points = points;
        }
    }
}