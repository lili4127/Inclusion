using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private GameObject[] panels;
    public bool timerGoing { get; private set; }
    private float score = 0f;
    private float pointsPerSecond = 10;
    private int level;
    public static event System.Action<int> gameBegin;

    private void Awake()
    {
        level = 5;
        timerGoing = false;
    }

    private void OnEnable()
    {
        PlayerTrain.gameLost += LoseGame;
        LevelModifier.speedUp += SpeedUp;
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
        gameBegin?.Invoke(level);
        score = 0f;
        texts[1].text = score.ToString() + "m";
        texts[1].gameObject.SetActive(true);
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
            texts[1].text = Mathf.FloorToInt(score).ToString() + "m";
            yield return null;
        }
    }

    private void StopTimer()
    {
        timerGoing = false;
    }

    private void SpeedUp()
    {
        StartCoroutine(SpeedUpCo());
    }

    IEnumerator SpeedUpCo()
    {
        float time = 0f;

        while (time < 2f)
        {
            time += Time.deltaTime;
            //speeding up text animation
            texts[2].gameObject.SetActive(true);
            yield return null;
        }
        texts[2].gameObject.SetActive(false);
    }

    private void LoseGame()
    {
        StopTimer();

        if (PlayerPrefs.GetInt("highscore", 0) < Mathf.FloorToInt(score))
        {
            texts[3].text = "New High Score!";
            texts[4].text = texts[1].text;
            PlayerPrefs.SetInt("highscore", Mathf.FloorToInt(score));
        }

        else
        {
            texts[3].text = "Final Score: " + texts[1].text;
            texts[4].text = "High Score: " + PlayerPrefs.GetInt("highscore", 0).ToString() + "m";
        }

        panels[1].SetActive(true);
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
        texts[1].text = score.ToString();
        Time.timeScale = 1f;
        StartCoroutine(CountdownToStart());
    }

    private void OnDisable()
    {
        PlayerTrain.gameLost -= LoseGame;
        LevelModifier.speedUp -= SpeedUp;
    }
}
