using System.Collections.Generic;
using System.Linq;
using _Scripts.Data.UnityObjects.SO;
using _Scripts.Enums;
using _Scripts.Objects;
using _Scripts.Systems.ObjectPooling.Pools;
using _Scripts.Utilities;
using UnityEngine;
using UnityEngine.Audio;

namespace _Scripts.Managers
{
    public class SFXManager : MonoBehaviour
    {
        private Dictionary<string, SFXData> _sfxDataDictionary;
        private AudioMixer _audioMixer;

        private SFXObjectPool _sfxObjectPool;
        
        private void Awake()
        {
            SFXData[] sfxs = Resources.LoadAll<SFXData>("Data/SFXs");
            _sfxDataDictionary = sfxs.ToDictionary(sfx => sfx.name, sfx => sfx);
            
            _audioMixer = Resources.Load<AudioMixer>("Data/SFXAudioMixer");
        }

        public void PlaySFX(string sfxName, Vector3 position = default) // position = Vector3.Zero
        {
            if (!_sfxDataDictionary.TryGetValue(sfxName, out SFXData data)) return;
            
            SFXObject sfxObject = _sfxObjectPool.GetFromPool();
            sfxObject.SetPosition(position);
            sfxObject.Play(data, GetAudioMixerGroupByType(data.type));
        }
        
        private AudioMixerGroup GetAudioMixerGroupByType(SFXType type)
        {
            return _audioMixer.FindMatchingGroups(type switch
            {
                SFXType.Single => ConstUtilities.SINGLE,
                _ => ConstUtilities.LOOP
            })[0];
        }

    }
}