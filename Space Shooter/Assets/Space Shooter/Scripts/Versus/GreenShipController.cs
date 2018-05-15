using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenShipController : MonoBehaviour {

    private float maxWidth;
    private float speed = 7.0f;

    public GameObject LaserGreenPrefab;
    public AudioSource LaserSound;

    public GameObject health4;
    public GameObject health5;
    public GameObject health6;

    public static int healthGreen = 3;

    // Use this for initialization
    void Start () {
        maxWidth = Camera.main.orthographicSize - 1.5f;
    }
	
	// Update is called once per frame
	void Update () {
        if (healthGreen == 3)
        {
            health4.SetActive(true);
            health5.SetActive(true);
            health6.SetActive(true);
        }
        else if (healthGreen == 2)
        {
            health4.SetActive(false);
        }
        else if (healthGreen == 1)
        {
            health5.SetActive(false);
        }
        else if (healthGreen == 0)
        {
            health6.SetActive(false);
        }


        if (VersusController.canShootAndMove)
        {
            if (Input.GetKey("up"))
            {
                if (transform.position.x > -maxWidth)
                {
                    transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
                }
            }

            if (Input.GetKey("down"))
            {
                if (transform.position.x < maxWidth)
                {
                    transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
                }
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Instantiate(LaserGreenPrefab, new Vector3(transform.position.x, transform.position.y - 0.7f, 0.0f), Quaternion.identity);
                LaserSound.Play();
            }
        }


    }
}
