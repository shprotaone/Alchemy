using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionGarbage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bottle"))
        {
            Bottle bottle = collision.GetComponent<Bottle>();

            bottle.ResetBottle();
            
            Debug.Log("Garbage");
        }
    }
}
