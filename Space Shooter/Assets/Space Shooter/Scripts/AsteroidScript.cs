using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {

    public float speed = 5.0f;
    public AudioClip asteroidHitSound;
    public AudioClip asteroidDieSound;
    public int armor;

    private float maxHeight;
    

    private SpaceShipGameControllerScript gameController;

    // Use this for initialization
    void Start()
    {
        maxHeight = Camera.main.orthographicSize + 7.2f;
        gameController = FindObjectOfType<SpaceShipGameControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.0f, -speed * Time.deltaTime, 0.0f);

        if (transform.position.y < -maxHeight)
            Destroy(gameObject);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Laser"))
        {
            if (armor == 0)
            {
                AudioSource.PlayClipAtPoint(asteroidDieSound, new Vector3(0,0,0));

                if (!(this.gameObject.tag == "Invisibility" || this.gameObject.tag == "Weight" || this.gameObject.tag == "Rocket" || this.gameObject.tag == "Magnet" || this.gameObject.tag == "Bomb"))
                {
                    if (this.gameObject.tag == "Coin")
                    {
                        gameController.AddScore(-1);
                    }
                    else
                    {
                        gameController.AddScore(1);
                    }
                }

                Destroy(gameObject);
            }
            else
            {
                AudioSource.PlayClipAtPoint(asteroidHitSound, new Vector3(0, 0, 0));
                armor--;
                StartCoroutine(ShowAndHide(this.gameObject, 0.1f));
            }

        }
    }

    IEnumerator ShowAndHide(GameObject go, float delay)
    {
        go.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(delay);
        go.GetComponent<Renderer>().enabled = true;
    }

}
