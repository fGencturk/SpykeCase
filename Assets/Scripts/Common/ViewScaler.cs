using Common.Extensions;
using UnityEngine;

namespace Common
{
    public class ViewScaler : MonoBehaviour
    {

        #region Inspector

        [SerializeField] private Camera _Camera;
        [SerializeField] private Vector2 _ViewSize;

        #endregion

        private void Awake()
        {
            var cameraBounds = _Camera.GetOrthographicBounds();

            var scaleHeight = cameraBounds.size.y / _ViewSize.y;
            var scaleWidth = cameraBounds.size.x / _ViewSize.x;

            transform.localScale = Vector3.one * Mathf.Min(scaleWidth, scaleHeight);
        }
    }
}