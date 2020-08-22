using UnityEngine;
using DG.Tweening;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Singleton

        public static CameraManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            _mainCamera = Camera.main;
        }

        #endregion

        #region Private Variables

        private Camera _mainCamera;

        #endregion

        public void ShakeCamera()
        {
            _mainCamera.DOShakeRotation(.3f, 2, 45, 90);
        }
    }
}