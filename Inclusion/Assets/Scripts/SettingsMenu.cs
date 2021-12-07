using UnityEngine;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject muteToggle;
    [SerializeField] private TextMeshProUGUI scoreText;
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

        scoreText.text = "High Score: " + PlayerPrefs.GetInt("highscore", 0).ToString() + "m";
    }

    public void Mute()
    {
        isMute = !isMute;
        AudioListener.volume = isMute ? 0 : 1;
        muteToggle.SetActive(!muteToggle.activeInHierarchy);
    }

    public void ResetHighscore()
    {
        scoreText.text = "High Score: " + "0m";
        PlayerPrefs.SetInt("highscore", 0);
    }
}
