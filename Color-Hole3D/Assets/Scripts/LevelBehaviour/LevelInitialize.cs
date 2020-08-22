using System;
using Managers;
using ScriptableScripts;
using UnityEngine;

namespace LevelBehaviour
{
    public class LevelInitialize : MonoBehaviour
    {
        private void OnEnable() => LevelManager.Instance.LevelInstantiate += InstantiateLevel;

        private void OnDisable() => LevelManager.Instance.LevelInstantiate -= InstantiateLevel;


        private void InstantiateLevel(LevelScriptable newLevel)
        {
            Instantiate(newLevel.LevelPrefab, Vector3.zero, Quaternion.identity);
            enabled = false;
        }
    }
}