using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlue : MonoBehaviour {

    public float speed = 25.0f;

    public AudioClip hitSound;
    public AudioClip destroySound;

    private float maxHeight;

    // Use this for initialization
    void Start () {
        maxHeight = Camera.main.orthographicSize + 4.0f;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);

        if (transform.position.y > maxHeight)
            Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (GreenShipController.healthGreen == 1)
            {
                GreenShipController.healthGreen--;
                AudioSource.PlayClipAtPoint(destroySound, new Vector3(0, 0, 0));
                Destroy(other.gameObject);
            }
            else
            {
                AudioSource.PlayClipAtPoint(hitSound, new Vector3(0, 0, 0));
                GreenShipController.healthGreen--;
            }
        }
        Destroy(gameObject);
    }
}
