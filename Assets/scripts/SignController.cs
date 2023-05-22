using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour
{
    [SerializeField] GameObject text;
    void Start()
    {
        text.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            text.SetActive(true);
            Debug.Log("Display Text");
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            text.SetActive(false);
            Debug.Log("Hide Text");
        }
    }
}
