using System.Collections;
using UnityEngine;

public class AudioFade : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.volume = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFade(source, 1f));
    }

    private static IEnumerator StartFade(AudioSource audioSource, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < 1)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / 1);
            yield return null;
        }
        yield break;
    }

    public void FadeToZero()
    {
        StartCoroutine(StartFade(source, 0f));
    }
}
