using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalistaPlayer : MonoBehaviour
{
    public int valueDamage;
    public float speed;
    public Rigidbody2D myBody;
    void Start()
    {
        myBody.velocity = new Vector2(speed, myBody.velocity.y);
    }


}
