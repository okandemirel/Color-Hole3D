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

        public bool IsTouching, IsAvailableForTouch;
        public Vector3 TouchPositionForMovement;

        #endregion

        #region Private Variables

        private float TouchPosX, TouchPosY;

        #endregion

        private void Start()
        {
            EventManager.Instance.LevelTouchConditions = TouchActivation;
        }

        private void Update()
        {
            if (!IsAvailableForTouch) return;
            #if UNITY_EDITOR

            IsTouching = Input.GetMouseButton(0);

            if (!IsTouching) return;
            GetTouchPositions();
            #else
             IsTouching = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved; 

            if (!IsTouching) return;
            GetTouchPositions();
            #endif
        }

        private void GetTouchPositions()
        {
            TouchPosX = Input.GetAxis("Mouse X");
            TouchPosY = Input.GetAxis("Mouse Y");

            TouchPositionForMovement = new Vector3(TouchPosX, 0, TouchPosY);
        }

        private void TouchActivation(bool touchConditionValue)
        {
            IsAvailableForTouch = touchConditionValue;
        }
    }
}