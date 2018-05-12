using UnityEngine;
using System.Collections;
using System;

public class SpaceShipScript : MonoBehaviour {


    public float speed = 10.0f;
    public GameObject LaserPrefab;
    public AudioSource LaserSound;
    public AudioSource spaceShipDeathSound;
    public AudioSource beInvisibleSound;
    public AudioSource weightIncreaseSound;
    public AudioSource coinTakeSound;
    public AudioSource rocketSound;
    public AudioSource bombSound;

    private float maxWidth;
    private float maxHeight;

    private GameObject[] coins;
    private GameObject[] asteroids;

    private bool canShoot = true;
    private bool canCollect = false;

    private SpaceShipGameControllerScript gameController;

    // Use this for initialization
    void Start()
    {
        maxWidth = Camera.main.orthographicSize * Camera.main.aspect -6.1f;
        maxHeight = Camera.main.orthographicSize + 4.0f;

        gameController = FindObjectOfType<SpaceShipGameControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canCollect)
        {
            coins = GameObject.FindGameObjectsWithTag("Coin");
            foreach (GameObject coin in coins)
            {
                gameController.AddScore(2);
                coinTakeSound.Play();
                Destroy(coin);

            }
        }
        transform.Translate(Input.GetAxis("Vertical") * -speed * Time.deltaTime,
                            Input.GetAxis("Horizontal") * speed * Time.deltaTime,
                            0.0f);


        Vector3 newPosition = new Vector3(
                                Mathf.Clamp(transform.position.x, -maxWidth, maxWidth),
                                Mathf.Clamp(transform.position.y, -maxHeight, maxHeight),
                                0.0f
            );

        transform.position = newPosition;

        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            Instantiate(LaserPrefab, new Vector3(transform.position.x, transform.position.y + 0.7f, 0.0f), Quaternion.identity);
            LaserSound.Play();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Invisibility")
        {
            beInvisibleSound.Play();
            canShoot = false;
            Destroy(other.gameObject);
            StartCoroutine(ShowAndHide());

        }
        else if (other.gameObject.tag == "Weight")
        {
            weightIncreaseSound.Play();
            speed = 5.0f;
            Destroy(other.gameObject);
            StartCoroutine(Weight());
            
        }
        else if (other.gameObject.tag == "Coin")
        {
            coinTakeSound.Play();
            Destroy(other.gameObject);
            gameController.AddScore(2);

        }
        else if (other.gameObject.tag == "Rocket")
        {
            rocketSound.Play();
            speed = 20.0f;
            Destroy(other.gameObject);
            StartCoroutine(Weight());

        }
        else if (other.gameObject.tag == "Magnet")
        {
            coinTakeSound.Play();
            Destroy(other.gameObject);
            StartCoroutine(Magnet());
            canCollect = true;

        }
        else if (other.gameObject.tag == "Bomb")
        {
            bombSound.Play();
            Destroy(other.gameObject);
            asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
            foreach (GameObject asteroid in asteroids)
            {
                Destroy(asteroid);

            }

        }
        else
        {
            spaceShipDeathSound.Play();
            gameController.GameOver();
            Destroy(gameObject);
        }
    }

    IEnumerator ShowAndHide()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(3);
        this.gameObject.GetComponent<Renderer>().enabled = true;
        canShoot = true;
        beInvisibleSound.Play();
    }

    IEnumerator Weight()
    {
        yield return new WaitForSeconds(6);
        beInvisibleSound.Play();
        speed = 10.0f;
    }

    IEnumerator Magnet()
    {
        yield return new WaitForSeconds(15);
        beInvisibleSound.Play();
        canCollect = false;
    }

}
