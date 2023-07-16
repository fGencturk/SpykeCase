using System;
using Core.Enum;

namespace UI.SlotMachine.Animation
{
    public interface ISlotScrollViewAnimator
    {
        void Animate(SlotScrollView slotScrollView, SlotItemType targetItemType, float initialDelay, Action<float> onPosChange, Action onComplete);
    }
}