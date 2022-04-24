using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class CastleManager : MonoBehaviour
{

    public List<int> valueWaitingList;
    public List<char> operandWaitingList;
    public List<EnemyControl> enemyDeployed;
    public int[] randomValue;
    public float actualCoolDown;
    public float spawnRate;
    public Transform pointSpawn;
    public GameObject balistaSpawn;
    public Transform pointEnemy;
    public GameObject enemySpawn;

    public List<int> valueForAttack;
    public TextMeshProUGUI[] valueUI;
    public TextMeshProUGUI operand;
    public char currentOperand;

    public int score;
    public TextMeshProUGUI scoreUI;
    public bool isGameStart;

    public GameObject title;

    public GameObject game;

    public GameObject GameOverPanel;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI highScore;

    public AudioSource musicS;
    public AudioSource sfxS;

    public AudioClip mainMenuMusic, gameplayMusic;
    public AudioClip arrowSFX,diedSFX;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<3; i++)
        {
            AddOperand();
        }
        musicS.clip = mainMenuMusic;
        musicS.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStart)
        {
            actualCoolDown -= Time.deltaTime;
            if (actualCoolDown < 0)
            {
                SpawnEnemy();
                actualCoolDown = spawnRate;
            }
        }
    }

    public void RefreshScore()
    {
        scoreUI.text = score.ToString();
    }
    public void AddOperand()
    {
        int random1 = Random.Range(1, 9);
        int random2 = Random.Range(1, 9);
        int randomOperand = Random.Range(0, 3);

        int valueResult = 0;
        char operandResult ='+';
        if(randomOperand == 0)
        {
            valueResult = random1 + random2;
            operandResult = '+';
        }
        else if (randomOperand == 1)
        {
            valueResult = random1 - random2;
            operandResult = '-';
        }
        else
        {
            valueResult = random1 * random2;
            operandResult = '*';
        }

        valueWaitingList.Add(valueResult);
        operandWaitingList.Add(operandResult);
    }

    public void RefreshOperand()
    {
        if(enemyDeployed.Count > 0)
        {
            currentOperand = enemyDeployed[0].operand;
        }
        else
        {
            currentOperand = operandWaitingList[0];
        }
        operand.text = currentOperand.ToString();
    }

    public void SpawnEnemy()
    {
        if (spawnRate > 3)
        {
            spawnRate = 5 - (score / 100);
        }
        GameObject spawnedGO = Instantiate(enemySpawn,pointEnemy.position,Quaternion.identity);
        EnemyControl spawnedScript = spawnedGO.GetComponent<EnemyControl>();
        enemyDeployed.Add(spawnedScript);
        spawnedScript.castleManager = this;
        spawnedScript.value = valueWaitingList[0];
        spawnedScript.operand = operandWaitingList[0];
        valueWaitingList.RemoveAt(0);
        operandWaitingList.RemoveAt(0);
        RefreshOperand();
        AddOperand(); 
    }
    public void _OnButtonAddValue(int value)
    {
        if(valueForAttack.Count < 2)
        {
            valueUI[valueForAttack.Count].text = value.ToString();
            valueForAttack.Add(value);
        }
    }

    public void _OnButtonClear()
    {
        for(int i=0; i < valueUI.Length; i++)
        {
            valueUI[i].text = "";
        }
        valueForAttack.Clear();
    }

    public void _OnButtonFire()
    {
        if (valueForAttack.Count > 1)
        {
            sfxS.PlayOneShot(arrowSFX);
            int valueReleased;
            if (currentOperand == '+')
            {
                valueReleased = valueForAttack[0] + valueForAttack[1];
            }
            else if (currentOperand == '-')
            {
                valueReleased = valueForAttack[0] - valueForAttack[1];
            }
            else
            {
                valueReleased = valueForAttack[0] * valueForAttack[1];
            }

            GameObject balistaInst = Instantiate(balistaSpawn, pointSpawn.position, Quaternion.identity);
            BalistaPlayer balistaScript = balistaInst.GetComponent<BalistaPlayer>();
            balistaScript.valueDamage = valueReleased;
            _OnButtonClear();
        }
    }

    public void GameStart()
    {
        title.gameObject.SetActive(false);
        game.gameObject.SetActive(true);
        musicS.clip = gameplayMusic;
        musicS.Play();
            
        isGameStart = true;
    }

    public void GameOver()
    {
        musicS.Stop();
        GameOverPanel.SetActive(true);
        isGameStart = false;
        int highScoreTemp = PlayerPrefs.GetInt("HighScoreCastle");
        int currentScore = score;
        finalScore.text = score.ToString();
        if (score > highScoreTemp)
        {
            PlayerPrefs.SetInt("HighScoreCastle", currentScore);
            highScoreTemp = PlayerPrefs.GetInt("HighScoreCastle");
        }
        highScore.text = highScoreTemp.ToString();
    }

    public void _OnButtonRetry()
    {
        SceneManager.LoadScene(3);
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
