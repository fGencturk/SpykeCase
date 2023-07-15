using System;
using Core.Enum;
using UnityEngine;
using UnityEngine.UI;

namespace UI.SlotMachine
{
    [RequireComponent(typeof(RectTransform))]
    public class SlotItemView : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private SlotItemType _ItemType;
        [SerializeField] private Image _Image;
        [SerializeField] private Image _BlurredImage;

        #endregion

        #region Properties

        public SlotItemType ItemType => _ItemType;

        public RectTransform RectTransform { get; private set; }

        #endregion

        #region Unity event functions

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        #endregion

        public void UpdateBlurryAmount(float normalizedBlurryAmount)
        {
            _Image.color = new Color(1f, 1f, 1f, 1f - normalizedBlurryAmount);
            _BlurredImage.color = new Color(1f, 1f, 1f, normalizedBlurryAmount);
        }
        
    }
}