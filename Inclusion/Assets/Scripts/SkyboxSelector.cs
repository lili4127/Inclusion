using UnityEngine;

public class SkyboxSelector : MonoBehaviour
{
    [SerializeField] private Material[] skyboxes;
    private Skybox sky;

    private void Awake()
    {
        sky = Camera.main.GetComponent<Skybox>();
        sky.material = skyboxes[PlayerPrefs.GetInt("sky", 0)];
    }

    public void Changeskybox(int i)
    {
        sky.material = skyboxes[i];
        PlayerPrefs.SetInt("sky", i);
    }
}
