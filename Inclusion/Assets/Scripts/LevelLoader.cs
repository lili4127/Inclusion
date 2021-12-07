using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private Animator crossFadeAnimator;

    private void Awake()
    {
        crossFadeAnimator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeToLevelCo(sceneName));
    }

    IEnumerator FadeToLevelCo(string levelName)
    {
        Time.timeScale = 1;
        crossFadeAnimator.SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName);
    }
}
