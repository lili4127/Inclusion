using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI scoreText;
    public bool timerGoing { get; private set; }
    private float score = 0f;
    private float pointsPerSecond = 10;
    private int level;
    public static event System.Action<int> gameBegin;

    private void Awake()
    {
        level = 5;
        timerGoing = false;
        scoreText.text = "0";
    }

    private void OnEnable()
    {
        PlayerTrain.gameLost += LoseGame;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        int countdownTime = 3;
        countdownText.text = countdownTime.ToString();
        countdownText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);

        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
        BeginGame();
    }

    private void BeginGame()
    {
        gameBegin?.Invoke(level);
        score = 0f;
        scoreText.text = score.ToString();
        StartTimer();
    }

    private void StartTimer()
    {
        timerGoing = true;
        StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            score += pointsPerSecond * Time.deltaTime;
            scoreText.text = Mathf.FloorToInt(score).ToString();
            yield return null;
        }
    }

    private void StopTimer()
    {
        timerGoing = false;
    }

    private void LoseGame()
    {
        StopTimer();

        //if (PlayerPrefs.GetInt("highscore", 0) < Mathf.FloorToInt(score))
        //{
        //    texts[2].text = "New High Score!";
        //    texts[3].text = texts[1].text;
        //    PlayerPrefs.SetInt("highscore", Mathf.FloorToInt(score));
        //}

        //else
        //{
        //    texts[2].text = "Final Score: " + texts[1].text;
        //    texts[3].text = "High Score: " + PlayerPrefs.GetInt("highscore", 0).ToString();
        //}

        //panels[1].SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        StopTimer();
        //panels[0].SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        StartTimer();
        //panels[0].SetActive(false);
        //gameplayObjects[0].SetActive(true);
    }

    public void RestartGame()
    {
        //panels[0].SetActive(false);
        //panels[1].SetActive(false);
        score = 0f;
        scoreText.text = score.ToString();
        Time.timeScale = 1f;
        StartCoroutine(CountdownToStart());
    }

    private void OnDisable()
    {
        PlayerTrain.gameLost -= LoseGame;
    }
}
