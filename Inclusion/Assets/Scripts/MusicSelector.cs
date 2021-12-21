using UnityEngine;

public class MusicSelector : MonoBehaviour
{
    [SerializeField] private AudioClip[] songs;
    private AudioSource aud;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        aud.clip = songs[PlayerPrefs.GetInt("sky", 0)];
        aud.Play();
    }
}
