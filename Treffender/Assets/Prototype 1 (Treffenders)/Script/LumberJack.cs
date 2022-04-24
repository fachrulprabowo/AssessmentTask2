using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberJack : MonoBehaviour
{
    public Rigidbody2D myBody;
    public GameManager gm;
    public GameObject treeDestination;
    public BoxCollider2D collider;
    public float speed;
    public Animator myAnimation;
    public int health;
    bool imdead;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        myAnimation = GetComponent<Animator>();
    }

    void Update()
    {
        if(treeDestination!=null && gm.isGamePlayingNow && !imdead)
        transform.position = Vector3.MoveTowards(transform.position, treeDestination.transform.position, speed);
    }

    public void Death()
    {
        int random = Random.Range(0, gm.grunts.Length);
        gm.effect.PlayOneShot(gm.grunts[random]);
        imdead = true;
        collider.enabled = false;
        myAnimation.SetBool("isDead", true);
        Destroy(gameObject,1.5f);
    }

    private void OnMouseUpAsButton()
    {
        Debug.Log("click");
        health--;
        if (health <= 0)
        {
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Tree")
        {
            gm.health--;
            gm.RefreshTree();
            Death();
        }
    }
}
