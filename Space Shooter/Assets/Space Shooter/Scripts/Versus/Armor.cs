using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour {

    private float maxWidth;

    public AudioClip armorHitSound;
    public AudioClip armorDieSound;

    private int armor = 9;

    private int toWhere = 0;

    void Start()
    {
        maxWidth = Camera.main.orthographicSize - 1.5f;
    }

    void Update()
    {
        if (this.gameObject.tag == "MovingArmor")
        {
            if (transform.position.x > -maxWidth && toWhere == 0)
            {
                transform.Translate(Vector3.left * 3.0f * Time.deltaTime, Space.World);
            }
            else
            {
                toWhere = 1;
            }

            if (transform.position.x < maxWidth && toWhere == 1)
            {
                transform.Translate(Vector3.right * 3.0f * Time.deltaTime, Space.World);
            }
            else
            {
                toWhere = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Laser"))
        {
            if (armor == 0)
            {
                AudioSource.PlayClipAtPoint(armorDieSound, new Vector3(0, 0, 0));
                Destroy(gameObject);
            }
            else
            {
                AudioSource.PlayClipAtPoint(armorHitSound, new Vector3(0, 0, 0));
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
