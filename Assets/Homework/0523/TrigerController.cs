using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerController : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        Debug.Log("面倒 惯积");
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        boxCollider.isTrigger = false;
        Debug.Log("面倒 力芭");
    }
}
