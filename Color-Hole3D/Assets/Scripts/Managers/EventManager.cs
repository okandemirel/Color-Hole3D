using System;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class EventManager : MonoBehaviour
    {
        #region Singleton

        public static EventManager Instance;

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

        #region Unity Actions

        //public UnityAction ChangeCameraOffsetToPhaseTwoCameraCondition = delegate { };

        public UnityAction StopAllPoliceCauseLevelFailed = delegate { };

        public UnityAction DestroyAllPoliceCauseLevelSuccessfull = delegate { };

        public UnityAction<int> AssignCinemachinesTargetPlayer = delegate { };
        public UnityAction StopCameraFollow = delegate { };

        #endregion
    }
}