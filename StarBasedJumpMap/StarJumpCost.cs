using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleTech.SimGameSpaceController;

namespace StarBasedJumpMap
{
    public class StarJumpCost
    {
        /// <summary>
        /// The minimum amount of jump charge time per planet type.
        /// As per Pode, this is the lore for jump ship spin up time.
        /// </summary>
        public static Dictionary<StarType, int> StarTypeJumpMap = new Dictionary<StarType, int>()
        {
            { StarType.M, 9 },
            { StarType.K, 9 },
            { StarType.G, 8 },
            { StarType.F, 8 },
            { StarType.A, 7 },
            { StarType.B, 7 },
            { StarType.O, 6 },
        };

        public bool IsFastJump(int jumpDistance, StarType starType)
        {
            int starJumpCost;
            if(StarTypeJumpMap.TryGetValue(starType, out starJumpCost) == false)
            {
                //default to G star, like the battletech code.
                starJumpCost = 8;
            }

            return jumpDistance * 2 <= starJumpCost;
        }
    }
}
