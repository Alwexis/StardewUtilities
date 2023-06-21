using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewUtilities {
    public interface IModule {
        string Name { get; }
        void Load(IModHelper helper);
    }
}
