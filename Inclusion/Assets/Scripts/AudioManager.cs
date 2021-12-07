using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameObject muteToggle;
    [SerializeField] private bool isMute;

    private void Awake()
    {
        if (AudioListener.volume == 0)
        {
            muteToggle.SetActive(true);
            isMute = true;
        }

        else
        {
            muteToggle.SetActive(false);
            isMute = false;
        }
    }

    public void Mute()
    {
        isMute = !isMute;
        AudioListener.volume = isMute ? 0 : 1;
        muteToggle.SetActive(!muteToggle.activeInHierarchy);
    }
}
