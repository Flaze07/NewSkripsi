using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC
{
    public class MusicPlayer : MonoBehaviour
    {
        public static MusicPlayer instance;

        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip[] songs;
        private int currentSongIndex;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            currentSongIndex = Random.Range(0, songs.Length);
            StartMusic(PickNextSong());
            StartCoroutine(CheckIfPlaying());
        }

        private IEnumerator CheckIfPlaying()
        {
            while (true)
            {
                if(!audioSource.isPlaying) StartMusic(PickNextSong());
                yield return new WaitForSeconds(1);
            }
        }

        public void StartMusic(AudioClip song)
        {
            Debug.Log($"Current song: {currentSongIndex}. {song.name}");
            audioSource.clip = song;
            audioSource.Play();
        }

        private AudioClip PickNextSong()
        {
            currentSongIndex = currentSongIndex + 1 >= songs.Length ? 0 : currentSongIndex + 1;
            return songs[currentSongIndex];
        }

        public void StopMusic()
        {
            audioSource.Stop();
        }
    }
}