using UnityEngine;
using TMPro;

public class SkyboxSelector : MonoBehaviour
{
    [SerializeField] private Material[] skyboxes;
    [SerializeField] private TextMeshProUGUI currentSkybox;
    private Skybox sky;
    private int currentIndex;

    private void Awake()
    {
        sky = Camera.main.GetComponent<Skybox>();
        currentIndex = PlayerPrefs.GetInt("sky", 0);
        sky.material = skyboxes[currentIndex];
        currentSkybox.text = sky.material.name;
    }

    public void ChangeSkybox(int i)
    {
        if (currentIndex == 0 && i < 0)
        {
            currentIndex = 4;
        }

        else if (currentIndex == 3 && i > 0)
        {
            currentIndex = -1;
        }

        currentIndex += i;
        sky.material = skyboxes[currentIndex];
        currentSkybox.text = sky.material.name;
        PlayerPrefs.SetInt("sky", currentIndex);
    }
}
