using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

    public Rigidbody2D enemyBullet;
    public GameObject player;
    public float bulletSpeed;
    Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        InvokeRepeating("Shoot", 4.0f, 1.5f);
	}
	
    void Shoot()
    {
        Rigidbody2D bullet = Instantiate(
            enemyBullet,
            rigid.transform.position,
            rigid.transform.rotation);

        bullet.velocity = (player.transform.position - bullet.transform.position).normalized * bulletSpeed;
        
    }
}
