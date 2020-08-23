using Managers;
using UnityEngine;

namespace UIBehaviour
{
    public class ContinueButton : MonoBehaviour
    {
        public void Continue()
        {
            //If there was any AdManager added in game, we would trigger in here and afterwards we will continue playing.

            GameplayManager.IsGameOver = false;
            GameplayManager.Instance.AdvertisementPlayed = true;
            TouchManager.Instance.IsAvailableForTouch = true;
            EventManager.Instance.LevelCanvasConditionsForClosing.Invoke(false);
        }

        public static void Restart()
        {
            LevelManager.Instance.LevelFailed();
        }
    }
}