using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewUtilities {
    public sealed class ModConfig {
        // Time Module
        public Dictionary<string, object> TimeModule { get; set; }
        public ModConfig() {
            TimeModule = new Dictionary<string, object> {
                { "Enabled", true },
                { "Indoors", 1 },
                { "Outdoors", 1 },
                { "Mines", 1 },
                { "DeepWoods", 1 }
            };
        }
    }
}
