using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusController : MonoBehaviour {

    public AudioSource gameplayMusic;
    public bool canShootAndMove = false;
    public GameObject ready;
    public GameObject set;
    public GameObject go;

    // Use this for initialization
    void Start () {
        gameplayMusic.Play();
        StartCoroutine(Wait());
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
