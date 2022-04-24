using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyControl : MonoBehaviour
{
    public int value;
    public char operand;
    public CastleManager castleManager;
    public Rigidbody2D myBody;
    public float speed;
    public bool iAmFront;
    public TextMeshPro valueUI;
    public int point;
    // Start is called before the first frame update
    void Start()
    {
        valueUI.text = value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        myBody.velocity = new Vector2(-speed, myBody.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Balista")
        {
            BalistaPlayer bp = collision.gameObject.GetComponent<BalistaPlayer>();
            Destroy(collision.gameObject);
            if(bp.valueDamage == value)
            {
                castleManager.sfxS.PlayOneShot(castleManager.diedSFX);
                castleManager.score += point;
                castleManager.RefreshScore();
                castleManager.enemyDeployed.Remove(this);
                castleManager.RefreshOperand();
                Destroy(gameObject);
                
            }
        }

        if (collision.gameObject.tag == "Castle")
        {
            castleManager.GameOver();
        }
    }
}
