using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace NoFeatherFalling
{
    public class NoFeatherFalling : Mod, IMenuMod
    {
        private bool toggleOption;

        public bool ToggleButtonInsideMenu => throw new NotImplementedException();

        new public string GetName() => "No Feather Falling";
        public override string GetVersion() => "v1.3";
        public override void Initialize()
        {
            On.HeroController.DoHardLanding += HeroController_DoHardLanding;
        }

        private void HeroController_DoHardLanding(On.HeroController.orig_DoHardLanding orig, HeroController self)
        {
            if(toggleOption == false)
            {
                self.TakeDamage(self.gameObject, GlobalEnums.CollisionSide.bottom, 1, 2);
            };
        }

        public List<IMenuMod.MenuEntry> GetMenuData(IMenuMod.MenuEntry? toggleButtonEntry)
        {
            return new List<IMenuMod.MenuEntry>
            {
                new IMenuMod.MenuEntry {
                    Name = "Feather Falling",
                    Description = "Turn Feather Falling on/off",
                    Values = new string[] {
                        "Off",
                        "On"
                    },
                    Saver = opt => this.toggleOption = opt switch {
                        0 => false,
                        1 => true,
                        _ => throw new InvalidOperationException()
                    },
                    Loader = () => this.toggleOption switch {
                        false => 0,
                        true => 1,
                    }
                }
            };
        }
    }
}