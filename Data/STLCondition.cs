using System.Collections.Generic;
using Terraria;

namespace IntegratedInfo.Data
{
    public static class STLCondition
    {
        public static Dictionary<int, Condition[]> ConditionsByItemID { get; private set; }
        public static void Load()
        {
            ConditionsByItemID = [];

        }
    }
}
