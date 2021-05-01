using Two.Common.Presentation.Controller.Interface;
using UnityEngine;

namespace Two.Common.Presentation.Controller.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class BaseAudioSource : MonoBehaviour, IVolumeUpdate
    {
        private AudioSource _audioSource;

        protected AudioSource audioSource
        {
            get
            {
                if (_audioSource == null)
                {
                    _audioSource = GetComponent<AudioSource>();
                }

                return _audioSource;
            }
        }

        public void SetVolume(float value) => audioSource.volume = value;

        public float GetVolume() => audioSource.volume;
    }
}