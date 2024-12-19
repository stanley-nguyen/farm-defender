using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private AudioSource sfxSource;

    public AudioClip titlebgm;
    public AudioClip bgm;
    public AudioClip playerHit;
    public AudioClip gunShot;
    public AudioClip minePlace;
    public AudioClip mineExplode;
    public AudioClip enemyDeath;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Title Screen")
        {
            PlayMusic(titlebgm);
        }
        else if (scene.name == "Game")
        {
            PlayMusic(bgm);
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.pitch = 1f;
        sfxSource.PlayOneShot(clip, volume);
    }

    public void PlaySFXRandomPitch(AudioClip clip, float min, float max, float volume)
    {
        sfxSource.pitch = Random.Range(min, max);

        sfxSource.PlayOneShot(clip, volume);
    }
}
