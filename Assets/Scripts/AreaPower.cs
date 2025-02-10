using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPower : MonoBehaviour
{
    public Camera Camera;
    public Transform walls;
   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Camera.orthographicSize = Camera.orthographicSize + 1.5f;
            walls.transform.localScale = new Vector3(walls.transform.localScale.x +0.1f , walls.transform.localScale.y +0.1f , walls.transform.localScale.z);
            
        }
    }
}
