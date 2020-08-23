using System;
using Managers;
using UnityEngine;

namespace CollisionControl
{
    public class BottomColliderControl : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (GameplayManager.IsGameOver) return;

            if (other.CompareTag("Collectable"))
            {
                Debug.Log("Collided with a collectable");
                iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
                GameplayManager.Instance.IncreaseCounterAndControl();
                Destroy(other.gameObject, 1);
            }

            if (other.CompareTag("Uncollectable"))
            {
                Debug.Log("Collided with a uncollectable");
                iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
                GameplayManager.IsGameOver = true;
                EventManager.Instance.LevelCanvasConditionsForOpening.Invoke(false);
                EventManager.Instance.LevelTouchConditions.Invoke(false);
                CameraManager.Instance.ShakeCamera();
                Destroy(other.gameObject, 1);
            }
        }
    }
}