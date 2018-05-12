using UnityEngine;
using System.Collections;

public class LaserScriptMini : MonoBehaviour {

    public float speed = 25.0f;


    private float maxHeight;

    // Use this for initialization
    void Start () {
        maxHeight = Camera.main.orthographicSize + 50.0f;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0.0f, speed * Time.deltaTime, 0.0f);

        if (transform.position.y > maxHeight)
            Destroy(gameObject);

	}


}
