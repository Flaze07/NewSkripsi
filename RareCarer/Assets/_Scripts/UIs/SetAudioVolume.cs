using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace RC
{
    public class SetAudioVolume : MonoBehaviour
    {
        public enum AudioType { SFX, Music };

        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioType type;
        [SerializeField] private Slider slider;

        private void Start()
        {
            slider.value = PlayerPrefs.GetFloat(type + "Volume", 1f);
        }

        public void SetVolume(float sliderValue)
        {
            audioMixer.SetFloat(type + "Volume", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat(type + "Volume", sliderValue);
        }
    }
}