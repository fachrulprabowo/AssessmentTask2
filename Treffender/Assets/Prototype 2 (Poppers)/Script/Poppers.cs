using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poppers : MonoBehaviour
{
    public SpriteRenderer sr;
    public PoppersManager pm;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {

            
            pm.allPoppers.Remove(this);
            pm.score++;
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
