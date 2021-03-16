using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 10f;
    bool facingRight = true;
    Rigidbody2D rigid;
    public GameObject bulletPrefab;
    public GameObject playerAttack;
    public GameObject playerSlide;
    public Vector2 bulletSpeed;
    public Vector3 bulletOffSet;
    private Vector2 tempSpeed;
    public int lives;
    bool still = false;
    bool attacking = false;
    public float slideSpeed;
    float move;
    bool dead = false;
    bool beingHit = false;
    Animator animator;
    bool isAttacking = false;

    public Text failText;
    public Text playerLives;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        failText.text = "";
        playerLives.text = "Lives: " + lives;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        move = Input.GetAxis("Horizontal");

        if (still) move = 0.0f;

        rigid.velocity = new Vector2(move * maxSpeed, rigid.velocity.y);

        if(move > 0 && !facingRight)
        {
            Flip();
        }
        else if(move < 0 && facingRight)
        {
            Flip();
        }
	}

    void Update()
    {
        if (lives == 0) Death();

        if (dead) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if(Input.GetKeyDown(KeyCode.X))
        {
            Fire();
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(Attack());
        }

        animator.SetBool("isAttacking", isAttacking);
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Fire()
    {
        var bullet = Instantiate(
            bulletPrefab,
            rigid.transform.position + bulletOffSet,
            rigid.transform.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed;

        StartCoroutine(Still());

        Destroy(bullet, 1.0f);
    }

    IEnumerator Attack()
    {
        var attack = Instantiate(
            playerAttack,
            rigid.transform.position,
            rigid.transform.rotation);

        StartCoroutine(Still());
        isAttacking = true;
        attacking = true;
        yield return new WaitForSeconds(0.3f);
        attacking = false;
        isAttacking = false;
        Destroy(attack);
    }

    IEnumerator Slide()
    {
        var slide = Instantiate(
            playerSlide,
            rigid.transform.position,
            rigid.transform.rotation);

        attacking = true;
        tempSpeed.x = move * slideSpeed;
        tempSpeed.y = rigid.velocity.y;
        rigid.velocity = tempSpeed;
        slide.GetComponent<Rigidbody2D>().velocity = tempSpeed;
        yield return new WaitForSeconds(0.4f);
        attacking = false;
        Destroy(slide);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if((other.gameObject.tag == "Ball" || other.gameObject.tag == "EnemyBullet") && !attacking && !beingHit)
        {
            lives -= 1;
            playerLives.text = "Lives:" + lives.ToString();
            still = true;
            beingHit = true;
            StartCoroutine(IsHit());
        }
    }

    void Death()
    {
        dead = true;
        this.gameObject.SetActive(false);
        failText.text = "YOU DIED";
    }

    IEnumerator Still()
    {
        still = true;
        yield return new WaitForSeconds(0.3f);
        still = false;
    }

    IEnumerator IsHit()
    {
        yield return new WaitForSeconds(0.3f);
        still = false;
        beingHit = false;
    }
}
