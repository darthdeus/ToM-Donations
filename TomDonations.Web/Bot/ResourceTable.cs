using System.Collections.Generic;

namespace TomDonations.Web {
    public class ResourceTable {
        public static Dictionary<string, float> MultiplierMapping;
        public static Dictionary<string, int> TierMapping;

        static ResourceTable() {
            MultiplierMapping = new Dictionary<string, float> {
                {"hide", 1},
                {"leather", 2},

                {"wood", 1},
                {"plank", 2},

                {"fiber", 1},
                {"cloth", 2},

                {"ore", 1},
                {"bar", 2},

                {"stone", 1},
                {"block", 2},

                {"food", 1}
            };

            TierMapping = new Dictionary<string, int> {
                {"carrot", 1},
                {"bean", 2},
                {"wheat", 3},
                {"turnip", 4},
                {"cabbage", 5},
                {"potato", 6},
                {"corn", 7},
                {"pumpkin", 8},

                {"cotton", 2},
                {"flax", 3},
                {"hemp", 4},
                {"skyflower", 5},
                {"redleaf", 6},
                {"sunflax", 7},
                {"ghosthemp", 8},

                {"copper", 2},
                {"bronze", 3},
                {"iron", 4},
                {"titanium", 5},
                {"runite", 6},
                {"meteorite", 7},
                {"adamantium", 8},

                {"rugged", 2},
                {"medium", 4},
                {"heavy", 5},
                {"robust", 6},
                // TODO: edge case
                //{"thick", 7},
                {"resilient", 8},

                {"birch", 2},
                {"chestnut", 3},
                {"pine", 4},
                {"cedar", 5},
                {"bloodoak", 6},
                {"ashenbark", 7},
                {"whitewood", 8},

                {"limestone", 2},
                {"sandstone", 3},
                {"travertine", 4},
                {"granite", 5},
                {"slate", 6},
                {"basalt", 7},
                {"marble", 8},

                {"simple", 2},
                {"neat", 3},
                {"fine", 4},
                {"ornate", 5},
                {"lavish", 6},
                {"opulent", 7},
                {"baroque", 8},

                {"stiff", 2},
                {"thick", 3},
                {"worked", 4},
                {"cured", 5},
                {"hardened", 6},
                {"reinforced", 7},
                {"fortified", 8}
            };
        }

        public static float? PointMultiplier(string type) {
            if (MultiplierMapping.ContainsKey(type)) {
                return MultiplierMapping[type];
            } else {
                return null;
            }
        }

        public static int? Tier(string name) {
            if (TierMapping.ContainsKey(name)) {
                return TierMapping[name];
            } else {
                return null;
            }
        }
    }
}