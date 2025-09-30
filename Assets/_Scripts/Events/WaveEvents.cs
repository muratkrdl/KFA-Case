using _Scripts.Controllers.Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Events
{
	public class WaveEvents : MonoBehaviour
	{
		public event UnityAction<int> OnWaveStarted;
		public event UnityAction OnWaveCompleted;
		public event UnityAction OnBossWave;
		public event UnityAction<EnemyController> OnEnemyDied;

		public void RaiseWaveStarted(int waveIndex) => OnWaveStarted?.Invoke(waveIndex);
		public void RaiseWaveCompleted() => OnWaveCompleted?.Invoke();
		public void RaiseBossWave() => OnBossWave?.Invoke();
		public void RaiseEnemyDied(EnemyController go) => OnEnemyDied?.Invoke(go);
	}
}


