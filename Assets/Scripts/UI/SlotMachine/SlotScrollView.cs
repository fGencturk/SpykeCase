using System;
using System.Collections.Generic;
using Common;
using Core.Enum;
using UI.SlotMachine.Animation;
using UnityEngine;

namespace UI.SlotMachine
{
    public class SlotScrollView : MonoBehaviour, ISlotScrollView
    {
        #region Inspector

        [SerializeField] private List<SlotItemView> _SlotItemViews;
        [SerializeField] private Transform _ScrollContent;

        #endregion
        
        private const float MaximumBlurrySpeed = 1f;
        private const float BlurryStartSpeed = .2f;

        private int _viewIndexAtTop = -1;

        #region Properties

        public int CurrentItemIndex { get; private set; }
        public List<SlotItemType> OrderedSlotItemTypes { get; private set; }
        public float SlotViewSize { get; private set; }
        public Transform ScrollContent => _ScrollContent;
        private int MiddleItemIndex => _SlotItemViews.Count / 2;

        #endregion

        #region Unity event functions

        private void Start()
        {
            OrderedSlotItemTypes = new List<SlotItemType>();
            for (var i = 0; i < _SlotItemViews.Count; i++)
            {
                OrderedSlotItemTypes.Add(_SlotItemViews[i].ItemType);
            }
            SlotViewSize = Constants.SlotItemHeightInWorldUnits;
            UpdateViewPositions();
        }

        #endregion

        public void OnPositionChange(float heightDiff)
        {
            var blurryAmount = (Mathf.Abs(heightDiff) - BlurryStartSpeed) / (MaximumBlurrySpeed - BlurryStartSpeed);
            var normalizedBlurryAmount = Mathf.Clamp(blurryAmount, 0f, 1f);
            foreach (var slotItemView in _SlotItemViews)
            {
                slotItemView.UpdateBlurryAmount(normalizedBlurryAmount);
            }

            UpdateViewPositions();
        }

        public float GetHeightDiffToTarget(int targetIndex, int loopCount)
        {
            return (CurrentItemIndex - targetIndex + loopCount * _SlotItemViews.Count) * -SlotViewSize;
        }

        private void UpdateViewPositions()
        {
            GetItemAtIndexAndPosition((int)_ScrollContent.localPosition.y, _SlotItemViews.Count, (int)SlotViewSize,
                out var newTopItemIndex, out var startHeight);
            if (_viewIndexAtTop == newTopItemIndex)
            {
                return;
            }

            for (var i = 0; i < _SlotItemViews.Count; i++)
            {
                var itemIndex = (newTopItemIndex + i) % _SlotItemViews.Count;
                var position = _SlotItemViews[itemIndex].transform.localPosition;
                position.y = startHeight - i * SlotViewSize;
                _SlotItemViews[itemIndex].transform.localPosition = position;
            }

            _viewIndexAtTop = newTopItemIndex;
            CurrentItemIndex = (_viewIndexAtTop + MiddleItemIndex) % _SlotItemViews.Count;
        }
        private void GetItemAtIndexAndPosition(int parentCurrentPosition, int itemCount, int itemSize, out int topItemIndex, out int topItemLocalPosition)
        {
            int totalContentHeight = itemCount * itemSize;
            int normalizedParentPosition = PositiveModulus(parentCurrentPosition, totalContentHeight);
            topItemIndex = (int)Math.Floor((double)normalizedParentPosition / itemSize);
            topItemLocalPosition = MiddleItemIndex * itemSize + Mathf.CeilToInt(Mathf.Abs(parentCurrentPosition) / (float)itemSize) * itemSize;
        }
        
        private int PositiveModulus(int number, int modulus)
        {
            return (number % modulus + modulus) % modulus;
        }
    }
}