using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Text colorToChange;
    public AudioSource laserSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        colorToChange.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        colorToChange.color = Color.white;
    }

    public void convertToWhite()
    {
        colorToChange.color = Color.white;
    }


    public void PlayGame()
    {
        laserSound.Play();
        StartCoroutine(Wait());
    }

    public void PlayGameBoss()
    {
        laserSound.Play();
        StartCoroutine(WaitBoss());
    }

    public void PlayGameVersus()
    {
        laserSound.Play();
        StartCoroutine(WaitVersus());
    }

    IEnumerator Wait()
    {
   
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator WaitBoss()
    {

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    IEnumerator WaitVersus()
    {

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void ExitGame()
    {
        laserSound.Play();
        Application.Quit();
    }
}
