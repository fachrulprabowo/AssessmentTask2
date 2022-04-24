using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioClip[] grunts;
    public AudioClip start, game, gameover;
    public AudioSource effect, music;
    public GameObject tree;
    public SpriteRenderer treeSprite;
    public Sprite[] treeConditions;
    public int health;
    public GameObject[] allSpawner;
    public float spawnRate;
    public float countDown;
    public GameObject lumberjackSpawn;
    public float score;
    public TextMeshProUGUI scoreUI;
    public GameObject title;
    public GameObject gameOver;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI highScore;

    public bool isGamePlayingNow;
    void Start()
    {
        music.clip = start;
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGamePlayingNow)
        {
            score += Time.deltaTime;
            scoreUI.text = score.ToString("F0");
            countDown -= Time.deltaTime;
            if (countDown <= 0)
            {
                int randomSpawnIdx = Random.Range(0, allSpawner.Length);
                Spawn(randomSpawnIdx);

                countDown = spawnRate - score * 0.1f;
                if (countDown <= 0.7f)
                {
                    countDown = 0.7f;
                }
            }
        }
    }
    public void Spawn(int randomSpawn)
    {
        GameObject spawnedLumberJack = Instantiate(lumberjackSpawn, allSpawner[randomSpawn].transform.position, Quaternion.identity);
        if(allSpawner[randomSpawn].transform.position.x > tree.transform.position.x)
        {
            spawnedLumberJack.transform.localScale = new Vector2(-0.2727147f, 0.2727147f);
        }
        LumberJack scriptLumberJack = spawnedLumberJack.GetComponent<LumberJack>();
        scriptLumberJack.treeDestination = tree;
        scriptLumberJack.gm = this;
    }

    public void RefreshTree()
    {
        if (health <= 0)
        {
            GameOver();
        }
        treeSprite.sprite = treeConditions[health];
    }

    public void GameOver()
    {
        health = 0;
        gameOver.gameObject.SetActive(true);
        Time.timeScale = 0;
        isGamePlayingNow = false;
        float highScoreTemp = PlayerPrefs.GetFloat("HighScore");
        float currentScore = score;
        finalScore.text = score.ToString("F0");
        if (score > highScoreTemp)
        {
            PlayerPrefs.SetFloat("HighScore", currentScore);
            highScoreTemp = PlayerPrefs.GetFloat("HighScore");
        }
        highScore.text = highScoreTemp.ToString("F0");
        effect.PlayOneShot(gameover);
        music.Stop();
    }

    public void _OnButtonRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }


    public void _OnButtonMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void _OnButtonPlay()
    {
        title.gameObject.SetActive(false);
        scoreUI.gameObject.SetActive(true);
        isGamePlayingNow = true;
        music.clip = game;
        music.Play();

    }
}
