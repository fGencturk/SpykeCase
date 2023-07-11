using System.Collections.Generic;
using UnityEngine;

namespace Core.Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
    public class SlotMachineConfig : ScriptableObject
    {
        #region Inspector

        [SerializeField] private List<SlotCombinationConfig> _SlotCombinationConfigs;

        #endregion

        #region Properties

        public IList<SlotCombinationConfig> SlotCombinationConfigs => _SlotCombinationConfigs;

        #endregion

    }
}
