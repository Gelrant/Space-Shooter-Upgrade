using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VersusController : MonoBehaviour {

    public AudioSource gameplayMusic;
    public static bool canShootAndMove = false;
    public GameObject ready;
    public GameObject set;
    public GameObject go;

    public GameObject greenWinUI;
    public GameObject greenLastHealth;
    public GameObject blueLastHealth;
    public GameObject blueWinUI;

    public GameObject Background;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;

    public GameObject pauseUI;
    private int canPause = 0;
    private int isPaused = 0;

    // Use this for initialization
    void Start () {
        gameplayMusic.Play();
        blueWinUI.SetActive(false);
        greenWinUI.SetActive(false);
        StartCoroutine(Wait());
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = 0;
        canShootAndMove = true;
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (canPause == 1)
            {
                if (isPaused == 1)
                {
                    pauseUI.SetActive(false);
                    Time.timeScale = 1f;
                    isPaused = 0;
                    canShootAndMove = true;
                }
                else
                {
                    pauseUI.SetActive(true);
                    Time.timeScale = 0f;
                    isPaused = 1;
                    canShootAndMove = false;

                }
            }
        }


        if (GreenShipController.healthGreen == 0)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Laser");

            for (var i = 0; i < gameObjects.Length; i++)
            {
                Destroy(gameObjects[i]);
            }
            canPause = 0;
            greenLastHealth.SetActive(false);
            blueWinUI.SetActive(true);
            greenWinUI.SetActive(false);
        }
        else if (BlueShipController.healthBlue == 0)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Laser");

            for (var i = 0; i < gameObjects.Length; i++)
            {
                Destroy(gameObjects[i]);
            }
            canPause = 0;
            blueLastHealth.SetActive(false);
            greenWinUI.SetActive(true);
            blueWinUI.SetActive(false);
        }
    }

    public void tryAgain()
    {
        canShootAndMove = false;
        blueWinUI.SetActive(false);
        greenWinUI.SetActive(false);
        GreenShipController.healthGreen = 3;
        BlueShipController.healthBlue = 3;
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
       
    }

    public void goMainMenu()
    {
        Time.timeScale = 1f;
        canShootAndMove = false;
        blueWinUI.SetActive(false);
        greenWinUI.SetActive(false);
        pauseUI.SetActive(false);
        GreenShipController.healthGreen = 3;
        BlueShipController.healthBlue = 3;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    IEnumerator Wait()
    {
        ready.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        ready.SetActive(false);
        set.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        set.SetActive(false);
        go.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        go.SetActive(false);
        canShootAndMove = true;
        canPause = 1;

    }

}
