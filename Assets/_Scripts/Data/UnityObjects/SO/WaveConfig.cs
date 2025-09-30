using System.Collections.Generic;
using _Scripts.Data.ValueObjects;
using UnityEngine;

namespace _Scripts.Data.UnityObjects.SO
{
	[CreateAssetMenu(fileName = "WaveConfig", menuName = "Game/Wave Config", order = 0)]
	public class WaveConfig : ScriptableObject
	{
		public float BaseEnemyHealth;
		public float BaseBossHealth;

		public float HealthIncrementByWave;
		public float HealthIncrementBossByWave;
		
		public int bossEveryWaves = 5;
		public float timeBetweenWaves = 20f;

		public List<WaveData> waves = new();
	}

}






