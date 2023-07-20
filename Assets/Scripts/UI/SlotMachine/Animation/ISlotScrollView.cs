using System.Collections.Generic;
using Core.Enum;
using UnityEngine;

namespace UI.SlotMachine.Animation
{
    public interface ISlotScrollView
    {
        List<SlotItemType> OrderedSlotItemTypes { get; }
        Transform ScrollContent { get; }
        float GetHeightDiffToTarget(int targetIndex, int loopCount);
    }
}