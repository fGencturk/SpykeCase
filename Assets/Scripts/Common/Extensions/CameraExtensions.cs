using UnityEngine;

namespace Common.Extensions
{
    public static class CameraExtensions
    {
        public static Bounds GetOrthographicBounds(this Camera camera)
        {
            var screenAspect = (float)Screen.width / (float)Screen.height;
            var cameraHeight = camera.orthographicSize * 2;
            var bounds = new Bounds(
                camera.transform.position,
                new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
            return bounds;
        }
    }
}