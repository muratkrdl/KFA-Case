using _Scripts.Events;
using _Scripts.Extensions;
using _Scripts.Managers.ServiceLocator;
using TMPro;
using UnityEngine;

namespace _Scripts.Controllers.UI
{
    public class WaveUIController : MonoBehaviour
    {
        private TextMeshProUGUI _waveText;
        
        private WaveEvents _waveEvents;

        private void Awake()
        {
            this.GetReference(ref _waveText);

            _waveEvents = ServiceLocator.Get<WaveEvents>();

            _waveEvents.OnWaveStarted += OnWaveStarted;
        }

        private void OnWaveStarted(int waveIndex)
        {
            _waveText.SetText("Wave " + (waveIndex-1));
        }

    }
}