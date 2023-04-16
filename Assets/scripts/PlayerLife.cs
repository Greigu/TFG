using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerLife : MonoBehaviour
{
    // creem dues variables del rigid body i el sprite render
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    //Carregquem les dues variables
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    // detectem si hem fet colisió amb els objectes amb tag Slime
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Slime"))
        {
            Die();
        }
    }
    // Si morim, deixem de renderitzar la imatge del jugador i cancelem el seu moviment i tornem a carregar el nivell
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        sr.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
