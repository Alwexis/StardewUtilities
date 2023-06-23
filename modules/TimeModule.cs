using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StardewUtilities.modules;

namespace StardewUtilities.modules {
    public class TimeModule {
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
            if (farmer.currentLocation.Name == "Woods") {
                TimeModifier = this.Config.TimeModule_DeepWoods;
            } else if (farmer.currentLocation.Name == "MineShaft" || farmer.currentLocation.Name == "Mine") {
                TimeModifier = this.Config.TimeModule_Mines;
            } else if (farmer.currentLocation.IsOutdoors) {
                TimeModifier = this.Config.TimeModule_Outdoors;
            } else {
                TimeModifier = this.Config.TimeModule_Indoors;
            }
            int ActualTime;
            if (Game1.gameTimeInterval < LastTime) {
                ActualTime = Game1.gameTimeInterval;
                LastTime = 0;
            } else {
                ActualTime = Game1.gameTimeInterval - LastTime;
            }
            // TimeModifier = Modifier;
            double Modifier = CalculateModifier(TimeModifier);
            Game1.gameTimeInterval = LastTime + (int)(ActualTime * Modifier);
            LastTime = Game1.gameTimeInterval;
        }

        private double CalculateModifier(int rawTimeModifier) {
            if (rawTimeModifier < 0) {
                return 1.0;
            } else {
                return 1 - (rawTimeModifier / 10.0);
            }
        }
    }
}
