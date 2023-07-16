using System;
using System.Collections.Generic;
using Infrastructure.Event;
using SlotMachine.Event;
using UI.Event;
using UI.SlotMachine.Animation;
using UnityEngine;

namespace UI.SlotMachine
{
    public class SlotMachineView : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private List<SlotScrollView> _SlotScrollViews;
        [SerializeField] private SlotScrollViewAnimator _FastSlotScrollViewAnimator;
        [SerializeField] private SlotScrollViewAnimator _NormalSlotScrollViewAnimator;
        [SerializeField] private SlotScrollViewAnimator _SlowSlotScrollViewAnimator;

        #endregion

        #region Unity event functions

        private void OnEnable()
        {
            EventManager.Register<SlotMachineRolledEvent>(OnSlotMachineRolled);
        }

        private void OnDisable()
        {
            EventManager.Unregister<SlotMachineRolledEvent>(OnSlotMachineRolled);
        }

        #endregion

        #region Handlers

        private void OnSlotMachineRolled(SlotMachineRolledEvent data)
        {
            EventManager.Send(SlotMachineRollStartedEvent.Create(data.SelectedCombination));
            for (var i = 0; i < data.SelectedCombination.SlotItems.Length - 1; i++)
            {
                var selectedItem = data.SelectedCombination.SlotItems[i];
                var slotView = _SlotScrollViews[i];
                _FastSlotScrollViewAnimator.Animate(slotView, selectedItem, i * .1f, slotView.OnPositionChange, null);
            }

            var selectedItems = data.SelectedCombination.SlotItems;
            ISlotScrollViewAnimator lastScrollViewAnimator = _FastSlotScrollViewAnimator;
            if (selectedItems[0] == selectedItems[1])
            {
                lastScrollViewAnimator = UnityEngine.Random.Range(0, 2) == 0 ? _NormalSlotScrollViewAnimator : _SlowSlotScrollViewAnimator;
            }

            void OnAnimationComplete()
            {
                EventManager.Send(SlotMachineRollEndedEvent.Create(data.SelectedCombination));
            }
            
            lastScrollViewAnimator.Animate(_SlotScrollViews[2], selectedItems[2], 0.2f, _SlotScrollViews[2].OnPositionChange, OnAnimationComplete);
        }

        #endregion
    }
}