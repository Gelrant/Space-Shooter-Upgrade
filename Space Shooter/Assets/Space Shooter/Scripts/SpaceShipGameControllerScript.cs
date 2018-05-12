using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class SpaceShipGameControllerScript : MonoBehaviour {


    public GameObject AsteroidPrefab;
    public GameObject AsteroidPrefab2;
    public GameObject AsteroidPrefab3;
    public GameObject AsteroidPrefab4;
    public GameObject AsteroidPrefab5;
    public GameObject AsteroidPrefab6;

    public GameObject EnemyBird;

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

    public AudioSource GamePlaySound;

    private int asteroidNumber;
    private float maxWidth;
    private float maxHeight;

    Stopwatch watch;
    int watchController = 0;

    public float asteroidPeriod = 1.0f;

    private int score = 0;
    private bool showResetButton = false;

    // Use this for initialization
    void Start()
    {
        activeScene = SceneManager.GetActiveScene();
        watch = new Stopwatch();
        watch.Start();
        maxWidth = Camera.main.orthographicSize * Camera.main.aspect - 6.1f;
        maxHeight = Camera.main.orthographicSize + 5;
        GamePlaySound.Play();
        StartCoroutine(Wait());

        InvokeRepeating("CreateAsteroid", 6.0f, asteroidPeriod);

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

    // Update is called once per frame
    void Update()
    {
        if (watch.Elapsed.TotalSeconds > 10 && watchController ==0)
        {
            CancelInvoke();
            watchController = 1;
            increaseDifficulty();
        }
        else if (watch.Elapsed.TotalSeconds > 20 && watchController == 1)
        {
            CancelInvoke();
            watchController = 2;
            increaseDifficulty2();
        }
        else if (watch.Elapsed.TotalSeconds > 30 && watchController == 2 && (activeScene.name.Equals("Boss")))
        {
            CancelInvoke();
            watchController = 3;
            StartCoroutine(Wait2());
        }
    }

    IEnumerator Wait2()
    {

        yield return new WaitForSeconds(3);
        increaseDifficulty3();
    }

    void increaseDifficulty()
    {
        InvokeRepeating("CreateAsteroid", 1.0f, 0.5f);
    }

    void increaseDifficulty2()
    {
        InvokeRepeating("CreateAsteroid", 1.0f, 0.25f);
    }

    void increaseDifficulty3()
    {
        InvokeRepeating("CreateAsteroid", 1.0f, 1.5f);
    }

    public void CreateAsteroid()
    {
        if (!(watchController == 3))
        {
            int asteroidNumber = Random.Range(0, 8);
            int randomBuff = Random.Range(0, 3);
            int randomBuff2 = Random.Range(0, 3);
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
                if (randomBuff2 == 0)
                {
                    Instantiate(Coin, new Vector3(
                        Random.Range(-maxWidth, maxWidth),
                        maxHeight + 3.0f,
                        0.0f
                    ), Quaternion.identity);
                }
                else if (randomBuff2 == 1)
                {
                    Instantiate(Rocket, new Vector3(
                        Random.Range(-maxWidth, maxWidth),
                        maxHeight + 3.0f,
                        0.0f
                    ), Quaternion.identity);
                }
                else
                {
                    Instantiate(Bomb, new Vector3(
                        Random.Range(-maxWidth, maxWidth),
                        maxHeight + 3.0f,
                        0.0f
                    ), Quaternion.identity);
                }
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
        showResetButton = true;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Score : " + score);

        if (showResetButton)
        {
            if (GUI.Button(new Rect(Camera.main.pixelWidth / 2 - 40, Camera.main.pixelHeight / 2 - 15, 80, 30), "Try again"))
            {
                int scene = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
            }
        }
    }


}
