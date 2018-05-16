using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueShipController : MonoBehaviour {

    private float maxWidth;
    private float speed = 7.0f;

    public GameObject LaserBluePrefab;
    public AudioSource LaserSound;

    public GameObject health1;
    public GameObject health2;
    public GameObject health3;

    public static int healthBlue = 3;

    // Use this for initialization
    void Start () {
        maxWidth = Camera.main.orthographicSize - 1.5f;
    }
	
	// Update is called once per frame
	void Update () {
        if (healthBlue == 3)
        {
            health1.SetActive(true);
            health2.SetActive(true);
            health3.SetActive(true);
        }
        else if (healthBlue == 2)
        {
            health3.SetActive(false);
        }
        else if (healthBlue == 1)
        {
            health2.SetActive(false);
        }
        else if (healthBlue == 0)
        {
            health1.SetActive(false);
        }


        if (VersusController.canShootAndMove)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (transform.position.x > -maxWidth)
                {
                    transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (transform.position.x < maxWidth)
                {
                    transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                    Instantiate(LaserBluePrefab, new Vector3(transform.position.x, transform.position.y + 0.7f, 0.0f), Quaternion.identity);
                    LaserSound.Play();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(ShowAndHide(this.gameObject, 0.1f));
    }

    IEnumerator ShowAndHide(GameObject go, float delay)
    {
        go.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(delay);
        go.GetComponent<Renderer>().enabled = true;
    }
}
