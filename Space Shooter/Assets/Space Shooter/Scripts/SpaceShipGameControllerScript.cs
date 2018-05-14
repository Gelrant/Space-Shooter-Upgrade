using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using UnityEngine.UI;

public class SpaceShipGameControllerScript : MonoBehaviour {


    public GameObject AsteroidPrefab;
    public GameObject AsteroidPrefab2;
    public GameObject AsteroidPrefab3;
    public GameObject AsteroidPrefab4;
    public GameObject AsteroidPrefab5;
    public GameObject AsteroidPrefab6;

    public GameObject EnemyBird;
    public GameObject BossPrefab;

    public GameObject Invisibility;
    public GameObject Weight;
    public GameObject Magnet;

    public GameObject Coin;
    public GameObject Rocket;
    public GameObject Bomb;

    Scene activeScene;

    public GameObject Ready;
    public GameObject Set;
    public GameObject Go;

    public GameObject UICanvas;
    public Text ScoreText;
    public Text timeText;

    public GameObject LoseUI;
    public GameObject WinUI;
    public GameObject PauseUI;

    public AudioSource GamePlaySound;
    public AudioSource BossGameplaySound;

    public GameObject Background;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;

    private int asteroidNumber;
    private float maxWidth;
    private float maxHeight;

    private int canPause = 1;
    private int isPaused = 0;

    private int gameEnd = 0;

    Stopwatch watch;
    int watchController = 0;

    public float asteroidPeriod = 1.0f;

    private string passedTime;
    private int score = 0;
    private bool showResetButton = false;

    Stopwatch timer = new Stopwatch();

    // Use this for initialization
    void Start()
    {
        canPause = 1;
        WinUI.SetActive(false);
        LoseUI.SetActive(false);
        gameEnd = 0;
        timer.Start();
        ScoreText.text = "Score: " + score;
        UICanvas.SetActive(true);
        activeScene = SceneManager.GetActiveScene();
        watch = new Stopwatch();
        watch.Start();
        maxWidth = Camera.main.orthographicSize - 0.5f;
        maxHeight = Camera.main.orthographicSize + 3f;
        GamePlaySound.Play();
        StartCoroutine(Wait());

        InvokeRepeating("CreateAsteroid", 6.0f, 1.5f);

    }

    public void Sprite1()
    {
        Background.GetComponent<SpriteRenderer>().sprite = sprite1;
    }

    public void Sprite2()
    {
        Background.GetComponent<SpriteRenderer>().sprite = sprite2;
    }

    public void Sprite3()
    {
        Background.GetComponent<SpriteRenderer>().sprite = sprite3;
    }

    IEnumerator Wait()
    {
        Ready.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        Ready.SetActive(false);
        Set.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        Set.SetActive(false);
        Go.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        Go.SetActive(false);

    }

    public void Resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        timer.Start();
        isPaused = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (canPause == 1)
            {
                if (isPaused == 1)
                {
                    PauseUI.SetActive(false);
                    Time.timeScale = 1f;
                    timer.Start();
                    isPaused = 0;
                }
                else
                {
                    PauseUI.SetActive(true);
                    Time.timeScale = 0f;
                    timer.Stop();
                    isPaused = 1;

                }
            }
        }
    

        if (gameEnd == 1)
        {
            canPause = 0;
        }
        else
        {
            int elapsedSeconds = (int)timer.Elapsed.TotalSeconds;
            timeText.text = "Time: " + elapsedSeconds;
        }
        ScoreText.text = "Score: " + score;
        if (timer.Elapsed.TotalSeconds > 30 && watchController ==0)
        {
            CancelInvoke();
            watchController = 1;
            increaseDifficulty();
        }
        else if (timer.Elapsed.TotalSeconds > 60 && watchController == 1)
        {
            CancelInvoke();
            watchController = 2;
            increaseDifficulty2();
        }
        else if (timer.Elapsed.TotalSeconds > 80 && watchController == 2 && (activeScene.name.Equals("Boss")))
        {
            CancelInvoke();
            BossGameplaySound.Play();
            watchController = 3;
            StartCoroutine(Wait2());
        }
        else if (timer.Elapsed.TotalSeconds > 110 && watchController == 3 && (activeScene.name.Equals("Boss")))
        {
            CancelInvoke();
            watchController = 4;
            StartCoroutine(Wait3());
        }
        if (gameEnd !=1 && timer.Elapsed.TotalSeconds > 120 && (activeScene.name.Equals("Boss")) && GameObject.FindWithTag("Boss") == null)
        {
            WinUI.SetActive(true);
            gameEnd = 1;
        }
    }

    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(2);
        GamePlaySound.volume = 0.10f;
        yield return new WaitForSeconds(2);
        GamePlaySound.volume = 0.05f;
        yield return new WaitForSeconds(2);
        GamePlaySound.volume = 0.03f;
        yield return new WaitForSeconds(2);
        GamePlaySound.volume = 0.01f;
        yield return new WaitForSeconds(2);
        GamePlaySound.Stop();
        increaseDifficulty3();
    }

    IEnumerator Wait3()
    {

        yield return new WaitForSeconds(5);
        increaseDifficulty4();
    }

    void increaseDifficulty()
    {
        InvokeRepeating("CreateAsteroid", 1.0f, 1f);
    }

    void increaseDifficulty2()
    {
        InvokeRepeating("CreateAsteroid", 1.0f, 0.5f);
    }

    void increaseDifficulty3()
    {
        InvokeRepeating("CreateAsteroid", 1.0f, 0.25f);
    }

    void increaseDifficulty4()
    {
        Instantiate(BossPrefab, new Vector3(
                    (maxWidth/2)-2.0f,
                    maxHeight + 6.0f,
                    0.0f
                ), Quaternion.Euler(0, 0, 90));
    }

    public void CreateAsteroid()
    {
        if (!(watchController == 3))
        {
            int asteroidNumber = Random.Range(0, 8);
            int randomBuff = Random.Range(0, 5);
            if (asteroidNumber % 8 == 0)
            {
                Instantiate(AsteroidPrefab, new Vector3(
                    Random.Range(-maxWidth, maxWidth),
                    maxHeight + 3.0f,
                    0.0f
                ), Quaternion.identity);
            }
            else if (asteroidNumber % 8 == 1)
            {
                Instantiate(AsteroidPrefab2, new Vector3(
                    Random.Range(-maxWidth, maxWidth),
                    maxHeight + 3.0f,
                    0.0f
                ), Quaternion.identity);
            }
            else if (asteroidNumber % 8 == 2)
            {
                Instantiate(AsteroidPrefab3, new Vector3(
                    Random.Range(-maxWidth, maxWidth),
                    maxHeight + 3.0f,
                    0.0f
                ), Quaternion.identity);
            }
            else if (asteroidNumber % 8 == 3)
            {
                Instantiate(AsteroidPrefab4, new Vector3(
                    Random.Range(-maxWidth, maxWidth),
                    maxHeight + 3.0f,
                    0.0f
                ), Quaternion.identity);
            }
            else if (asteroidNumber % 8 == 4)
            {
                Instantiate(AsteroidPrefab5, new Vector3(
                    Random.Range(-maxWidth, maxWidth),
                    maxHeight + 3.0f,
                    0.0f
                ), Quaternion.identity);
            }
            else if (asteroidNumber % 8 == 5)
            {
                Instantiate(AsteroidPrefab6, new Vector3(
                    Random.Range(-maxWidth, maxWidth),
                    maxHeight + 3.0f,
                    0.0f
                ), Quaternion.identity);
            }
            else if (asteroidNumber % 8 == 6)
            {
                if (randomBuff == 0)
                {
                    Instantiate(Invisibility, new Vector3(
                        Random.Range(-maxWidth, maxWidth),
                        maxHeight + 3.0f,
                        0.0f
                    ), Quaternion.identity);
                }
                else if (randomBuff == 1)
                {
                    Instantiate(Weight, new Vector3(
                        Random.Range(-maxWidth, maxWidth),
                        maxHeight + 3.0f,
                        0.0f
                    ), Quaternion.identity);
                }
                else if (randomBuff == 2)
                {
                    Instantiate(Rocket, new Vector3(
                        Random.Range(-maxWidth, maxWidth),
                        maxHeight + 3.0f,
                        0.0f
                    ), Quaternion.identity);
                }
                else if (randomBuff == 3)
                {
                    Instantiate(Bomb, new Vector3(
                        Random.Range(-maxWidth, maxWidth),
                        maxHeight + 3.0f,
                        0.0f
                    ), Quaternion.identity);
                }
                else
                {
                    Instantiate(Magnet, new Vector3(
                        Random.Range(-maxWidth, maxWidth),
                        maxHeight + 3.0f,
                        0.0f
                    ), Quaternion.identity);
                }
            }
            else if (asteroidNumber % 8 == 7)
            {
                    Instantiate(Coin, new Vector3(
                        Random.Range(-maxWidth, maxWidth),
                        maxHeight + 3.0f,
                        0.0f
                    ), Quaternion.identity);
                
            }
            Random.seed = (int)System.DateTime.Now.Ticks;
        }
        else if (watchController == 3)
        {
            Instantiate(EnemyBird, new Vector3(
                    Random.Range(-maxWidth, maxWidth),
                    maxHeight + -2.0f,
                    0.0f
                ), Quaternion.Euler(0,0,90));
        }
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public void GameOver()
    {
        LoseUI.SetActive(true);
        gameEnd = 1;
    }

    public void restartScene()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void goMainMenu()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        timer.Start();
        isPaused = 0;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }


}
