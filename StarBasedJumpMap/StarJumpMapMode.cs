using BattleTech;
using NavigationComputer.Features;
using NavigationComputer.Features.MapModes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StarBasedJumpMap
{
    public class StarJumpMapMode : IMapMode
    {
        public string Name => "Star Fast Jump";

        public StarJumpCost StarJumpCost { get; private set; } = new StarJumpCost();

        public MethodInfo DimSystemMethodInfo { get; set; }

        private void DimSystem(string system, float dimLevel)
        {

            if(DimSystemMethodInfo == null)
            {
                DimSystemMethodInfo = Harmony.AccessTools.Method(typeof(MapModesUI), "DimSystem", new Type[] { typeof(string), typeof(float) });
            }

            DimSystemMethodInfo.Invoke(null, new object[] { system, dimLevel });
        }

        public void Apply(SimGameState simGameState)
        {

            try
            {
                foreach (string system in simGameState.StarSystemDictionary.Keys)
                {
                    var starSystem = simGameState.StarSystemDictionary[system];

                    float dimValue;

                    dimValue = StarJumpCost.IsFastJump(starSystem.JumpDistance, starSystem.StarType) ? 1f : 10f;

                    DimSystem(system, dimValue);
                }
}
            catch(Exception ex)
            {
                Logger.Log(ex);
            }   
        }

        public void Unapply(SimGameState simGameState)
        {
        }
    }
}
