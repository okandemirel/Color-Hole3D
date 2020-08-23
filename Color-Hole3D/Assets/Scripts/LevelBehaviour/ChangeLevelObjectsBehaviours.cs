using LPWAsset;
using Managers;
using ScriptableScripts;
using UnityEngine;

namespace LevelBehaviour
{
    public class ChangeLevelObjectsBehaviours : MonoBehaviour
    {
        #region Serialized Variables

        [SerializeField] private LowPolyWaterScript lowPolyWater;

        #endregion

        private void OnEnable() => LevelManager.Instance.LevelInstantiate += ChangeLowPolyWaterColor;

        private void ChangeLowPolyWaterColor(LevelScriptable newLevel)
        {
            lowPolyWater.material.color = newLevel.LowPolyWaterColor;
        }
    }
}