using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveController : MonoBehaviour
{
    [SerializeField] GameObject text;
    GameDataController gameDataController;
    private bool canSave = false;
    void Start()
    {
        gameDataController = GameObject.Find("GameDataController").GetComponent<GameDataController>();
        text.SetActive(false);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.R) && canSave)
        {
            gameDataController.SaveData();
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            canSave = true;
            text.SetActive(true);
            Debug.Log("Can Save");
        }
        
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            canSave = false;
            text.SetActive(false);
            Debug.Log("Can't Save");
        }
    }
}
