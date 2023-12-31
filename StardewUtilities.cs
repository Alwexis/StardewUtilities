﻿using StardewModdingAPI;
using StardewUtilities.modules;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StardewUtilities {
    internal class StardewUtilities : Mod {
        ModConfig Config;
        public override void Entry(IModHelper helper) {
            this.Config = helper.ReadConfig<ModConfig>();
            helper.Events.GameLoop.GameLaunched += Alwexis_OnGameLaunched;
            Monitor.Log("StardewUtilities loaded!", LogLevel.Info);
            this.LoadModules(helper);
        }

        public void Alwexis_OnGameLaunched(object sender, EventArgs e) {
            // get Generic Mod Config Menu's API (if it's installed)
            var configMenu = this.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
                return;

            // register mod
            configMenu.Register(
                mod: this.ModManifest,
                reset: () => this.Config = new ModConfig(),
                save: () => this.Helper.WriteConfig(this.Config)
            );

            // add some config options
            configMenu.AddSectionTitle(
                mod: this.ModManifest,
                text: () => "Time Settings",
                tooltip: () => "These settings change how time passes in the game."
            );
            configMenu.AddParagraph(
                mod: this.ModManifest,
                text: () => "Manage how time passes in the game."
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enabled",
                tooltip: () => "Turn on or off the Time Module.",
                getValue: () => (bool)this.Config.TimeModule_Enabled,
                setValue: value => this.Config.TimeModule_Enabled = value
            );
            configMenu.AddNumberOption(
                mod: this.ModManifest,
                name: () => "Outdoors Modifier",
                tooltip: () => "How much time passes in the Outdoors.",
                getValue: () => (int)this.Config.TimeModule_Outdoors,
                setValue: value => this.Config.TimeModule_Outdoors = value,
                min: 1,
                max: 8
            );
            configMenu.AddNumberOption(
                mod: this.ModManifest,
                name: () => "Indoors Modifier",
                tooltip: () => "How much time passes Indoors.",
                getValue: () => (int)this.Config.TimeModule_Indoors,
                setValue: value => this.Config.TimeModule_Indoors = value,
                min: 1,
                max: 8
            );
            configMenu.AddNumberOption(mod: this.ModManifest,
                                       name: () => "Mines Modifier",
                                       tooltip: () => "How much time passes in the Mines.",
                                       getValue: () => (int)this.Config.TimeModule_Mines,
                                       setValue: value => this.Config.TimeModule_Mines = value,
                                       min: 1,
                                       max: 8);
            configMenu.AddNumberOption(mod: this.ModManifest,
                                       name: () => "Deep Woods Modifier",
                                       tooltip: () => "How much time passes in the Deep Woods.",
                                       getValue: () => (int)this.Config.TimeModule_DeepWoods,
                                       setValue: value => this.Config.TimeModule_DeepWoods = value,
                                       min: 1,
                                       max: 8);
        }

        private void LoadModules(IModHelper helper) {
            if ((bool)Config.TimeModule_Enabled) {
                TimeModule timeModule = new();
                timeModule.Load(helper);
            }
        }
    }
}
