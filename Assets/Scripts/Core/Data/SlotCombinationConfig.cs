using System;
using Core.Enum;
using UnityEngine;

namespace Core.Data
{
    [Serializable]
    public class SlotCombinationConfig
    {
        #region Inspector

        [SerializeField] private SlotItemType[] _SlotItems;
        [SerializeField] private int _Percentage;

        #endregion

        #region Properties

        public SlotItemType[] SlotItems => _SlotItems;
        public int Percentage => _Percentage;

        #endregion
    }
}