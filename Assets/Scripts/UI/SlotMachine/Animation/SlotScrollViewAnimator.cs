using System;
using System.Collections;
using Core.Enum;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.SlotMachine.Animation
{
    public class SlotScrollViewAnimator : MonoBehaviour, ISlotScrollViewAnimator
    {
        #region Inspector

        [SerializeField] private AnimationCurve _AnimationCurve;
        [SerializeField] private AnimationCurve _OvershootCurve;
        [SerializeField] private int _WholeLoopCount;
        [SerializeField] private float _AnimationDuration = 2f;
        [SerializeField] private float _OvershootDuration = .2f;

        #endregion

        private const float OvershootMultiplier = 2f;
        
        private IEnumerator AnimateCoroutine(ISlotScrollView slotScrollView, SlotItemType targetItemType, float initialDelay, Action<float> onPosChange)
        {
            var targetIndex = slotScrollView.OrderedSlotItemTypes.IndexOf(targetItemType);

            var totalHeightDiff = slotScrollView.GetHeightDiffToTarget(targetIndex, _WholeLoopCount);
            var position = slotScrollView.ScrollContent.anchoredPosition;
            var initialHeight = position.y;
            yield return new WaitForSeconds(initialDelay);

            // Initial scroll animation
            var startTime = Time.time;
            var previousHeightDiff = 0f;
            var lastHeightDiff = 0f;
            while (startTime + _AnimationDuration > Time.time)
            {
                var normalizedDuration = (Time.time - startTime) / _AnimationDuration;
                var currentHeightDiff = _AnimationCurve.Evaluate(normalizedDuration) * totalHeightDiff;
                
                position.y = initialHeight + currentHeightDiff;
                slotScrollView.ScrollContent.anchoredPosition = position;

                lastHeightDiff = currentHeightDiff - previousHeightDiff;
                onPosChange?.Invoke(lastHeightDiff);
                previousHeightDiff = currentHeightDiff;
                
                yield return new WaitForNextFrameUnit();
            }

            // Overshoot
            var overshootAmount = lastHeightDiff * OvershootMultiplier;
            startTime = startTime + _AnimationDuration;
            while (startTime + _OvershootDuration > Time.time)
            {
                var normalizedDuration = (Time.time - startTime) / _OvershootDuration;
                var overshootHeightDiff = _OvershootCurve.Evaluate(normalizedDuration) * overshootAmount;
                    
                position.y = initialHeight + totalHeightDiff + overshootHeightDiff;
                slotScrollView.ScrollContent.anchoredPosition = position;
            
                lastHeightDiff = overshootHeightDiff - previousHeightDiff;
                onPosChange?.Invoke(lastHeightDiff);
                previousHeightDiff = overshootHeightDiff;
                
                yield return new WaitForNextFrameUnit();
            }

            position.y = initialHeight + totalHeightDiff;
            slotScrollView.ScrollContent.anchoredPosition = position;
            onPosChange?.Invoke(0);
        }

        public void Animate(SlotScrollView slotScrollView, SlotItemType targetItemType, float initialDelay, Action<float> onPosChange)
        {
            var coroutine = AnimateCoroutine(slotScrollView, targetItemType, initialDelay, onPosChange);
            slotScrollView.StartCoroutine(coroutine);
        }
    }
}