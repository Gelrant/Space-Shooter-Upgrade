using UnityEngine;
using System.Collections;

public class SpaceShipScriptMini : MonoBehaviour {


    public float speed = 15.0f;
    public GameObject LaserPrefab;
    public AudioSource LaserSound;

    private float maxWidth;
    private float maxHeight;

    private SpaceShipGameControllerScript gameController;

    // Use this for initialization
    void Start()
    {
        maxWidth = Camera.main.orthographicSize * Camera.main.aspect + 25.1f;
        maxHeight = Camera.main.orthographicSize + 4.0f;

        gameController = FindObjectOfType<SpaceShipGameControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime,
                            Input.GetAxis("Vertical") * speed * Time.deltaTime,
                            0.0f);

        Vector3 newPosition = new Vector3(
                                Mathf.Clamp(transform.position.x, -maxWidth, maxWidth),
                                Mathf.Clamp(transform.position.y, -maxHeight, maxHeight),
                                0.0f
            );

        transform.position = newPosition;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(LaserPrefab, new Vector3(transform.position.x, transform.position.y + 1.0f, 0.0f), Quaternion.identity);
            LaserSound.Play();

        }

    }

}
