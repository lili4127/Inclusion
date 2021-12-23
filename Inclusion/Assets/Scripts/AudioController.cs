using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] songs;
    private AudioSource aud;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        aud.volume = 0f;
        aud.clip = songs[PlayerPrefs.GetInt("sky", 0)];
    }

    private void Start()
    {
        FadeMusic(1f);
    }

    private IEnumerator Fade(float targetVolume)
    {
        float currentTime = 0;
        float currentVolume = aud.volume;

        while (currentTime < 1)
        {
            currentTime += Time.deltaTime;
            aud.volume = Mathf.Lerp(currentVolume, targetVolume, currentTime / 1);
            yield return null;
        }
        aud.volume = targetVolume;

        if (aud.volume == 1)
        {
            aud.Play();
        }
        yield break;
    }

    private void FadeMusic(float volume)
    {
        StartCoroutine(Fade(volume));
    }

    public void ChangeSong(int i)
    {
        aud.Stop();
        aud.volume = 0f;
        aud.clip = songs[i];
        FadeMusic(1f);
    }
}
