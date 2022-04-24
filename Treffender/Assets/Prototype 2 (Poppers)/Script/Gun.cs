using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject gun;
    public GameObject batang;
    public GameObject bullet;
    public PoppersManager popperM;
    public float degree;
    public float degree2;
    public GameObject pointGun;
    public Vector3 force;
    public GameObject jointGun;
    public GameObject jointBatang;
    public Transform bulletAngleOffset;
    public float cooldown;
    public float actualCoolDown;
    public float sensitivity;
    public float sensitivity2;
    public void Shoot()
    {
        GameObject newBullet = Instantiate(bullet,pointGun.transform.position,Quaternion.identity);
        Vector3 newForce = bulletAngleOffset.rotation * force;
        newBullet.GetComponent<Rigidbody2D>().AddForce(newForce);

        Destroy(newBullet, 5);
        actualCoolDown = cooldown;
        
    }
    void Start()
    {
        
    }

    void Update()
    {

        if (popperM.isGameStart)
        {
            if (actualCoolDown >= 0)
                actualCoolDown -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (actualCoolDown <= 0)
                    Shoot();
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (degree > -90)
                    degree -= sensitivity;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (degree < 90)
                    degree += sensitivity;

            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (degree2 > -90)
                    degree2 -= sensitivity2;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (degree2 < 90)
                    degree2 += sensitivity2;

            }

            gun.transform.rotation = Quaternion.Euler(0, 0, degree);
            batang.transform.rotation = Quaternion.Euler(0, 0, degree2);

        }
    }
}
