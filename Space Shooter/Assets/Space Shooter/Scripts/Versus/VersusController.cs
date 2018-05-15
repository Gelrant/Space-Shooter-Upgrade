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

    // Use this for initialization
    void Start () {
        gameplayMusic.Play();
        blueWinUI.SetActive(false);
        greenWinUI.SetActive(false);
        StartCoroutine(Wait());
    }

    void Update()
    {
        if (GreenShipController.healthGreen == 0)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Laser");

            for (var i = 0; i < gameObjects.Length; i++)
            {
                Destroy(gameObjects[i]);
            }
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
        canShootAndMove = false;
        blueWinUI.SetActive(false);
        greenWinUI.SetActive(false);
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

    }

}
