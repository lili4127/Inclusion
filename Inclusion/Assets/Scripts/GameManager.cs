using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //screen and settings
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private GameObject[] screens;
    [SerializeField] private Image muteImage;
    [SerializeField] private bool isMute;

    //Gameplay
    public bool timerGoing { get; private set; }
    private float score = 0f;
    private float pointsPerSecond = 10;
    public static event System.Action<int> gameBegin;
    [SerializeField] private ObjectPool pool;

    private void Awake()
    {
        timerGoing = false;

        if (AudioListener.volume == 0)
        {
            isMute = true;
        }

        else
        {
            isMute = false;
        }

        texts[2].text = "High Score: " + PlayerPrefs.GetInt("highscore", 0).ToString() + "m";
    }

    private void OnEnable()
    {
        PlayerTrain.gameLost += LoseGame;
    }

    //Mute and Highscore Settings
    public void Mute()
    {
        isMute = !isMute;
        AudioListener.volume = isMute ? 0 : 1;
        muteImage.color = isMute ? Color.red : Color.white;
    }

    public void ResetHighscore()
    {
        PlayerPrefs.SetInt("highscore", 0);
        texts[2].text = "High Score: " + PlayerPrefs.GetInt("highscore", 0).ToString() + "m";
    }

    public void StartGame()
    {
        StartCoroutine(CountdownToStart());
        screens[0].SetActive(false);
        screens[1].SetActive(true);
    }

    IEnumerator CountdownToStart()
    {
        texts[0].gameObject.SetActive(false);
        texts[1].gameObject.SetActive(false);
        int countdownTime = 3;
        texts[0].text = countdownTime.ToString();
        texts[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);

        while (countdownTime > 0)
        {
            texts[0].text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        texts[0].text = "GO!";
        yield return new WaitForSeconds(1f);
        texts[0].gameObject.SetActive(false);
        BeginGame();
    }

    private void BeginGame()
    {
        gameBegin?.Invoke(PlayerPrefs.GetInt("difficulty",4));
        score = 0f;
        texts[1].text = score.ToString() + "m";
        texts[1].gameObject.SetActive(true);
        StartTimer();
    }

    private void StartTimer()
    {
        timerGoing = true;
        StartCoroutine(UpdateTimer());
        StartCoroutine(SpawnObstacles(PlayerPrefs.GetInt("difficulty", 4)));
    }

    IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            score += pointsPerSecond * Time.deltaTime;
            texts[1].text = Mathf.FloorToInt(score).ToString() + "m";
            yield return null;
        }
    }

    private void StopTimer()
    {
        timerGoing = false;
    }

    IEnumerator SpawnObstacles(float difficulty)
    {
        while (timerGoing)
        {
            GameObject g = pool.Get();
            g.gameObject.SetActive(true);
            StartCoroutine(MoveCo(g, new Vector3(pool.endPos.x, g.transform.position.y, pool.endPos.z), difficulty * 1.5f));
            yield return new WaitForSeconds(difficulty * 2);
        }
    }

    IEnumerator MoveCo(GameObject g, Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = g.transform.position;

        while (time < duration && timerGoing)
        {
            g.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        g.transform.position = targetPosition;
        pool.ReturnToPool(g);
    }

    private void LoseGame()
    {
        StopTimer();

        if (PlayerPrefs.GetInt("highscore", 0) < Mathf.FloorToInt(score))
        {
            texts[2].text = "New High Score! : " + texts[1].text;
            PlayerPrefs.SetInt("highscore", Mathf.FloorToInt(score));
        }

        else
        {
            texts[2].text = "Final Score: " + texts[1].text;
        }

        screens[0].SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        StopTimer();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        StartTimer();
    }

    public void RestartGame()
    {
        score = 0f;
        texts[1].text = score.ToString();
        Time.timeScale = 1f;
        StartCoroutine(CountdownToStart());
    }

    private void OnDisable()
    {
        PlayerTrain.gameLost -= LoseGame;
    }
}
