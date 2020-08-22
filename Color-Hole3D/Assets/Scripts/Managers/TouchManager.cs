using System;
using UnityEngine;

namespace Managers
{
    public class TouchManager : MonoBehaviour
    {
        #region Singleton

        public static TouchManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        #endregion

        #region Public Variables

        public bool isTouching;

        #endregion

        private void Update()
        {
            isTouching = Input.GetMouseButton(0);
        }
    }
}