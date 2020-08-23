//using GameAnalyticsSDK;
//using Facebook.Unity;

using UnityEngine;


namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            //GameAnalytics.Initialize();
            // FB.Init(FBInitCallback);


            Application.targetFrameRate = 60;
            //Application.backgroundLoadingPriority = ThreadPriority.Low;
        }

        #endregion

        // #region Facebook
        //
        // private void FBInitCallback()
        // {
        //     if (FB.IsInitialized)
        //     {
        //         FB.ActivateApp();
        //     }
        // }
        //
        // public void OnApplicationPause(bool paused)
        // {
        //     if (!paused)
        //     {
        //         if (FB.IsInitialized)
        //         {
        //             FB.ActivateApp();
        //         }
        //     }
        // }
        //
        // #endregion
    }
}