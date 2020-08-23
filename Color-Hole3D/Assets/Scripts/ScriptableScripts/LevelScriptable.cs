using UnityEngine;

namespace ScriptableScripts
{
    [CreateAssetMenu(fileName = "Level", menuName = "Level ")]
    public class LevelScriptable : ScriptableObject
    {
        public GameObject LevelPrefab;
        public int LevelCollectablesCount, Zone1CollectableCount, Zone2CollectableCount;
        public Color LowPolyWaterColor;
    }
}