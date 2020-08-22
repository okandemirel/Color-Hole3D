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

        public UnityAction<bool> LevelCanvasConditions = delegate { };
        public UnityAction<bool> LevelTouchConditions = delegate { };

        #endregion
    }
}