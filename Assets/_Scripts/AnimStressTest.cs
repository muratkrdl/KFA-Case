using System;
using _Scripts.Controllers.Enemy;
using _Scripts.Data.UnityObjects.SO;
using _Scripts.Enums;
using _Scripts.Extensions;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class AnimStressTest : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI animCountText;
        
        [SerializeField] private int maxCount;
    
        [SerializeField] private EnemyAnimController prefab;

        [SerializeField] private EnemyAnimFrames animFrames;

        [SerializeField] private float xRange;
        [SerializeField] private float zMin;
        [SerializeField] private float zMax;

        [SerializeField] private float timer;
    
        private int _currentCount;

        private bool _isRunning;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.P) || _isRunning) return;

            _isRunning = true;
            _ = SpawnAllEnemies();
        }

        private async UniTaskVoid SpawnAllEnemies()
        {
            while (_currentCount < maxCount)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-xRange, xRange), 0, Random.Range(-zMin, zMin));
                EnemyAnimController spawnObj = Instantiate(prefab, spawnPos, Quaternion.identity);
                spawnObj.transform.LookCamera();
                spawnObj.SetAnimFrames(animFrames);
                spawnObj.TriggerAnim(EnemyAnimType.Walk);
                _currentCount++;
                animCountText.SetText("Animation:" + _currentCount);
                await UniTask.WaitForSeconds(timer);
            }
        }

    }
}
