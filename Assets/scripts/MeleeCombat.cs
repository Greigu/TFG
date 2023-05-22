using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    [SerializeField] private Transform AtkController;
    [SerializeField] private float radiusAtk;
    
    public void Atk()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(AtkController.position, radiusAtk);
        foreach (Collider2D col in objects)
        {
            if (col.CompareTag("Slime"))
            {
                col.transform.GetComponent<slimeMovement>().Die();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AtkController.position, radiusAtk);
    }
}
