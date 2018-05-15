using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour {

    public AudioClip armorHitSound;
    public AudioClip armorDieSound;

    private int armor = 9;

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
