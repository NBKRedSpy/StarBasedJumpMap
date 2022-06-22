using BattleTech;
using BattleTech.UI;
using Harmony;
using NavigationComputer.Features;
using NavigationComputer.Features.MapModes;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StarBasedJumpMap.Patches
{

    [HarmonyPatch(typeof(SGNavigationScreen), "Init", typeof(SimGameState), typeof(SGRoomController_Navigation))]
    public static class MapSearchInit
    {

        public static bool Inited { get; private set; }

        public static void Postfix(SGNavigationScreen __instance, SimGameState simGame)
        {
            try
            {
                if (Inited) return;


                FieldInfo mapModesInfo = AccessTools.Field(typeof(MapModesUI), "DiscreteMapModes");

                Dictionary<KeyCode, IMapMode> discreteMapModes = (Dictionary < KeyCode, IMapMode >) mapModesInfo.GetValue(null);

                discreteMapModes.Add(KeyCode.F3, new StarJumpMapMode());

                Inited = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

    }
}
