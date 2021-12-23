using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] songs;
    private AudioSource aud;
    private int currentIndex;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        aud.volume = 0f;
        currentIndex = PlayerPrefs.GetInt("sky", 0);
        aud.clip = songs[currentIndex];
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

        if (currentIndex == 0 && i < 0)
        {
            currentIndex = 4;
        }

        else if (currentIndex == 3 && i > 0)
        {
            currentIndex = -1;
        }

        currentIndex += i;
        aud.clip = songs[currentIndex];
        FadeMusic(1f);
    }
}
