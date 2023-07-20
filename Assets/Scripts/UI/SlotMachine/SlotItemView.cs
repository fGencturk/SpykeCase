using Core.Enum;
using UnityEngine;

namespace UI.SlotMachine
{
    public class SlotItemView : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private SlotItemType _ItemType;
        [SerializeField] private SpriteRenderer _Image;
        [SerializeField] private SpriteRenderer _BlurredImage;

        #endregion

        #region Properties

        public SlotItemType ItemType => _ItemType;


        #endregion

        public void UpdateBlurryAmount(float normalizedBlurryAmount)
        {
            _Image.color = new Color(1f, 1f, 1f, 1f - normalizedBlurryAmount);
            _BlurredImage.color = new Color(1f, 1f, 1f, normalizedBlurryAmount);
        }
        
    }
}