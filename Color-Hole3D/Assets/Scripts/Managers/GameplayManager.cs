using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GameplayManager : MonoBehaviour
    {
        #region Singleton

        public static GameplayManager Instance;

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

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        #endregion
    }
}