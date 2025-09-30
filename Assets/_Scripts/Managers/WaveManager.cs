using System;
using System.Collections.Generic;
using System.Threading;
using _Scripts.Controllers.Enemy;
using _Scripts.Data.UnityObjects.SO;
using _Scripts.Data.ValueObjects;
using _Scripts.Enums;
using _Scripts.Events;
using _Scripts.Keys;
using _Scripts.Systems.ObjectPooling.Pools;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Scripts.Managers
{
	public class WaveManager : MonoBehaviour
	{
		public event Action<int> OnEnemyCountChanged;
		
		private WaveConfig _config;

		private int _waveCount;
		
		private readonly HashSet<EnemyController> _aliveEnemies = new();
		private int _currentWaveIndex = 1;
		private int _realWaveIndex;
		private bool _isRunning;
		
		private CancellationTokenSource _cts;
		
		private WaveEvents _waveEvents;
		private PathKeeper _pathKeeper;
		private EnemyObjectPool _enemyPool;

		private EnemyHealthKey _bossHealthKey;
		private EnemyHealthKey _healthKey;
		
		private void Awake()
		{
			_config = Resources.Load<WaveConfig>("Data/WaveConfig");

			_waveCount = _config.waves.Count;

			_waveEvents = ServiceLocator.ServiceLocator.Get<WaveEvents>();
			_pathKeeper = ServiceLocator.ServiceLocator.Get<PathKeeper>();
			_enemyPool = ServiceLocator.ServiceLocator.Get<EnemyObjectPool>();

			_bossHealthKey = new EnemyHealthKey()
			{
				Health = _config.BaseBossHealth, 
				IncrementByWave = _config.HealthIncrementBossByWave,
				CurrentWaveIndex = _currentWaveIndex
			};
			_healthKey = new EnemyHealthKey()
			{
				Health = _config.BaseEnemyHealth, 
				IncrementByWave = _config.HealthIncrementByWave,
				CurrentWaveIndex = _currentWaveIndex
			};
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				StartWaves();
			}

			if (Input.GetKeyDown(KeyCode.N))
			{
				StartWave(_currentWaveIndex, _cts.Token);
			}
		}

		public void StartWaves()
		{
			if (_isRunning) return;
			_isRunning = true;

			_cts?.Cancel();
			_cts?.Dispose();
			_cts = new CancellationTokenSource();

			RunWavesAsync(_cts.Token).Forget();
		}

		public void StopWaves()
		{
			_isRunning = false;
		}
		
    	private async UniTaskVoid RunWavesAsync(CancellationToken token)
	    {
		    _realWaveIndex = _currentWaveIndex;
		    
    	    while (_isRunning && !token.IsCancellationRequested)
    	    {
		        _bossHealthKey.CurrentWaveIndex = _realWaveIndex;
		        _healthKey.CurrentWaveIndex = _realWaveIndex;

    	        StartWave(_currentWaveIndex, token);

    	        await UniTask.Delay(TimeSpan.FromSeconds(_config.timeBetweenWaves),cancellationToken: token);
    	    }
    	}

	    private void StartWave(int waveIndex, CancellationToken token)
	    {
		    _realWaveIndex++;
		    _currentWaveIndex = _realWaveIndex % _waveCount;
		    if (_currentWaveIndex == 0) _currentWaveIndex = 10;
		    
		    _waveEvents.RaiseWaveStarted(_realWaveIndex);

		    var wave = GetWaveData(waveIndex);
		    bool isBossWave = wave.isBossWave || (waveIndex % _config.bossEveryWaves == 0);

		    if (isBossWave)
		    {
			    _waveEvents.RaiseBossWave();
			    
			    QueueWaveSpawn(wave.bossData.bossEntries, wave.bossData.spawnInterval, _bossHealthKey, token);
		    }

		    QueueWaveSpawn(wave.entries, wave.spawnInterval, _healthKey, token);
	    }

	    private void QueueWaveSpawn(List<EntryData> entries, float interval, EnemyHealthKey healthKey,
		    CancellationToken token)
	    {
		    _ = SpawnWaveRoutine(new EntriesDataKey
		    {
			    Entries = entries,
			    SpawnInterval = interval
		    }, healthKey, token);
	    }

	    private async UniTask SpawnWaveRoutine(EntriesDataKey wave, EnemyHealthKey healthKey, CancellationToken token)
	    {
		    float interval = wave.SpawnInterval;
		    var entries = wave.Entries;

		    for (int i = 0; i < entries.Count; i++)
		    {
			    if (token.IsCancellationRequested) break;

			    var go = Spawn(entries[i]);
			    _aliveEnemies.Add(go);
			    OnEnemyCountChanged?.Invoke(_aliveEnemies.Count);

			    if (i < entries.Count - 1)
				    await UniTask.Delay(TimeSpan.FromSeconds(interval), cancellationToken: token);
		    }

		    _waveEvents.RaiseWaveCompleted();
	    }

		private EnemyController Spawn(EntryData entryData)
		{
			EnemyController go = _enemyPool.GetFromPool();

			var myHealthKey = _healthKey;
			if (entryData.EnemyType == EnemyType.Boss)
			{
				myHealthKey = _bossHealthKey;
			}

			go.SetHealthDataValue(myHealthKey);
			go.Initialize(_pathKeeper.GetPath(), entryData);
			return go;
		}
		
		private WaveData GetWaveData(int index)
		{
			int zeroBased = Mathf.Clamp(index - 1, 0, _config.waves.Count - 1);
			return _config.waves[zeroBased];
		}

		private void OnEnable()
		{
			_waveEvents.OnEnemyDied += HandleEnemyDied;
		}
		
		private void HandleEnemyDied(EnemyController obj)
    	{
    		_aliveEnemies.Remove(obj);
		    OnEnemyCountChanged?.Invoke(_aliveEnemies.Count);
    	}

		private void OnDisable()
		{
			_waveEvents.OnEnemyDied -= HandleEnemyDied;
			_cts?.Cancel();
			_cts?.Dispose();
		}
		
	}
}
