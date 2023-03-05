using UnityEngine;

namespace Happyflow.Core
{
    /// <summary>
    /// Scales a game object to fit a target aspect ratio.
    /// </summary>
    public class AspectRatioScaler : MonoBehaviour
    {
        [Tooltip("The target width to fit the object to.")]
        [SerializeField]
        private float m_TargetWidth = 1080;

        [Tooltip("The target height to fit the object to.")]
        [SerializeField]
        private float m_TargetHeight = 1920;

        private void Update()
        {
            Resize();
        }

        private void Resize()
        {
            float screenHeight = Screen.height;
            float screenWidth = Screen.width;
            float currentAspectRatio = screenWidth / screenHeight;
            float desiredAspectRatio = m_TargetWidth / m_TargetHeight;
            float scaleRatio = currentAspectRatio / desiredAspectRatio;

            if (float.IsNaN(scaleRatio) || float.IsInfinity(scaleRatio))
            {
                Debug.LogWarning("Unable to determine screen aspect ratio. Scaling disabled.");
                return;
            }

            transform.localScale = new Vector3(scaleRatio, scaleRatio, 1);
        }
    }
}