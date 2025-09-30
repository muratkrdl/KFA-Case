using _Scripts.Managers;
using _Scripts.Managers.ServiceLocator;
using TMPro;
using UnityEngine;

namespace _Scripts.Controllers.UI
{
    public class TestUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI enemyCountText;
        [SerializeField] private TextMeshProUGUI fpsText;

        private WaveManager _waveManager;

        private int _lastFrameIndex;
        private int _filledFrameCount;
        private float[] _frameDeltaArray;
        
        private void Awake()
        {
            _waveManager = ServiceLocator.Get<WaveManager>();
            _frameDeltaArray = new float[50];
        }

        private void OnEnable()
        {
            _waveManager.OnEnemyCountChanged += UpdateEnemyValue;
        }

        private void UpdateEnemyValue(int obj)
        {
            enemyCountText.SetText("Enemy:" + obj);
        }

        private void OnDisable()
        {
            _waveManager.OnEnemyCountChanged -= UpdateEnemyValue;
        }

        private void Update()
        {
            _frameDeltaArray[_lastFrameIndex] = Time.deltaTime;
            _lastFrameIndex = (_lastFrameIndex + 1) % _frameDeltaArray.Length;

            if (_filledFrameCount < _frameDeltaArray.Length)
                _filledFrameCount++;

            fpsText.SetText(Mathf.RoundToInt(CalculateFPS()) + " FPS");
        }

        private float CalculateFPS()
        {
            float total = 0;
            for (int i = 0; i < _filledFrameCount; i++)
            {
                total += _frameDeltaArray[i];
            }
            return _filledFrameCount / total;
        }

    }
}