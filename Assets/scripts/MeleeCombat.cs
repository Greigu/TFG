using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    [SerializeField] private GameObject AtkController;

    [SerializeField] private float radiusAtk;
    private float dirX = 0f;
    private bool isRight;
    private void Update()
    {
        AtkDirection();
    }
    public void Atk()
    {
        AtkController.transform.position = new Vector3(isRight ? AtkController.transform.parent.transform.position.x + 0.1f : AtkController.transform.parent.transform.position.x - 0.1f, AtkController.transform.parent.transform.position.y + 0.1f, 0);
        Collider2D[] objects = Physics2D.OverlapCircleAll(AtkController.transform.position, radiusAtk);
        foreach (Collider2D col in objects)
        {
            if (col.CompareTag("Slime"))
            {
                col.transform.GetComponent<slimeMovement>().Die();
                Destroy(col.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AtkController.transform.position, radiusAtk);
    }

    private void AtkDirection()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        if (dirX > 0)
        {
            isRight = true;
        }
        else if (dirX < 0)
        {
            isRight = false;
        }

    }
}
