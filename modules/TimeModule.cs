using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewUtilities.modules {
    internal class TimeModule : IModule {
        public string Name => "TimeModule";

        private ModConfig Config;
        public static int LastTime { get; set; }
        private int TimeModifier { get; set; }
        public void Load(IModHelper helper) {
            Config = helper.ReadConfig<ModConfig>();
            LastTime = 0;
            helper.Events.GameLoop.SaveLoaded += (sender, e) => LastTime = 0;
            helper.Events.GameLoop.ReturnedToTitle += (sender, e) => LastTime = 0;
            helper.Events.GameLoop.UpdateTicked += Alwexis_TimeHandler;
        }
        private void Alwexis_TimeHandler(object sender, EventArgs e) {
            if (!Context.IsWorldReady) return;

            Farmer farmer = Game1.player;
            long rawTimeModifier;
            if (farmer.currentLocation.Name == "Woods") {
                rawTimeModifier = (long)this.Config.TimeModule[ "DeepWoods" ];
            } else if (farmer.currentLocation.Name == "MineShaft" || farmer.currentLocation.Name == "Mine") {
                rawTimeModifier = (long)this.Config.TimeModule[ "DeepWoods" ];
            } else if (farmer.currentLocation.IsOutdoors) {
                rawTimeModifier = (long)this.Config.TimeModule[ "DeepWoods" ];
            } else {
                rawTimeModifier = (long)this.Config.TimeModule[ "DeepWoods" ];
            }
            TimeModifier = Convert.ToInt32(rawTimeModifier);

            int ActualTime;
            if (Game1.gameTimeInterval < LastTime) {
                ActualTime = Game1.gameTimeInterval;
                LastTime = 0;
            } else {
                ActualTime = Game1.gameTimeInterval - LastTime;
            }
            double Modifier = TimeModifier < 2 ? 1 : 1 - (TimeModifier / 10);
            Game1.gameTimeInterval = LastTime + (int)(ActualTime * Modifier);
            LastTime = Game1.gameTimeInterval;
        }
    }
}
