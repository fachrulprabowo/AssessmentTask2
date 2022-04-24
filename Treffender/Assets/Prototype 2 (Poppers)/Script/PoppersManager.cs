using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PoppersManager : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Poppers> allPoppers;
    public GameObject poppersSpawn;
    public bool isGameStart;
    public GameObject left, right, down, up;
    public float timeLeft;
    public int spawningPoppers;
    public int score;

    public float timeAdd;
    public GameObject title;

    public GameObject game;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI timeLeftUI;

    public GameObject GameOverPanel;

    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI highScore;



    public void SpawnPop()
    {

        if (score % 40 == 0)
        {
            if (spawningPoppers > 15)
            {
                spawningPoppers++;
            }
            if(timeAdd > 7)
            {
                timeAdd -= 0.25f;
            }
        }
        for (int i = 0; i < spawningPoppers; i++)
        {
            float randomX = Random.Range(left.transform.position.x, right.transform.position.x);
            float randomY = Random.Range(down.transform.position.y, up.transform.position.y);
            Vector2 coor = new Vector2(randomX, randomY);
            GameObject a = Instantiate(poppersSpawn, coor, Quaternion.identity);
            Poppers script = a.GetComponent<Poppers>();
            script.pm = this;
            allPoppers.Add(script);
        }
        timeLeft += timeAdd;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStart)
        {
            scoreUI.text = "Score : " + score;
            timeLeft -= Time.deltaTime;
            timeLeftUI.text = "Time : " + timeLeft.ToString("F0");

            if (allPoppers.Count <= 0)
            {
                SpawnPop();
            }
            if (timeLeft<= 0)
            {
                GameOver();
            }
        }
    }

    public void GameStart()
    {
        title.gameObject.SetActive(false);
        game.gameObject.SetActive(true);

        isGameStart = true;
    }

    public void GameOver()
    {
        
        GameOverPanel.SetActive(true);
        isGameStart = false;
        float highScoreTemp = PlayerPrefs.GetFloat("HighScorePop");
        float currentScore = score;
        finalScore.text = score.ToString();
        if (score > highScoreTemp)
        {
            PlayerPrefs.SetFloat("HighScorePop", currentScore);
            highScoreTemp = PlayerPrefs.GetFloat("HighScorePop");
        }
        highScore.text = highScoreTemp.ToString();
    }

    public void _OnButtonRetry()
    {
        SceneManager.LoadScene(2);
    }
    public void _OnButtonMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void _OnButtonStart()
    {
        GameStart();
    }

}
