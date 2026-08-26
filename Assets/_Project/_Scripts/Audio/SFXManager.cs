using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Audio
{
    public class SFXManager : MonoBehaviour
    {
        public static SFXManager Instance;

        public enum SoundType
        {
            Gameplay,
            UI
        }

        [Title("Audio variations")]
        [SerializeField] private float minPitch = 0.95f;
        [SerializeField] private float maxPitch = 1.05f;

        private AudioSource _sfxSource;
        private float _uiCooldown = 0.1f;
        private float _uiTimer = 0f;


        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            _sfxSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_uiTimer > 0) _uiTimer -= Time.deltaTime;
        }

        public void PlaySFX(AudioClip clip, float volume, SoundType soundType, int pitchVariation = -1)
        {
            if (clip == null) return;

            if (soundType == SoundType.UI) {
                if (_uiTimer > 0) return;
                _uiTimer = _uiCooldown;
            }
            
            //Random pitch or rising pitch
            if (pitchVariation == -1) _sfxSource.pitch = Random.Range(minPitch, maxPitch);
            else _sfxSource.pitch = 1f + (pitchVariation * 0.03f);
            
            _sfxSource.PlayOneShot(clip, volume);
        }
    }
}