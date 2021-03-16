using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

    public Vector2 initialSpeed;
    private Vector2 tempSpeed;
    public float maxSpeed;
    public float minSpeed;
    Rigidbody2D rigid;

    public Text countText;
    public Text winText;
    private int score;
    public int goalScore;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        rigid.velocity = initialSpeed;
        score = 0;
        SetCountText();
        winText.text = "";
    }
	
	// Update is called once per frame
	void Update () {
		if(Mathf.Abs(rigid.velocity.x) > maxSpeed)
        {
            if(rigid.velocity.x < 0)
            {
                tempSpeed.x = -maxSpeed;
            }
            else tempSpeed.x = maxSpeed;
            tempSpeed.y = rigid.velocity.y;
            rigid.velocity = tempSpeed;
        }
        if(Mathf.Abs(rigid.velocity.y) > maxSpeed)
        {
            if(rigid.velocity.y < 0)
            {
                tempSpeed.y = -maxSpeed;
            }
            else tempSpeed.y = maxSpeed;
            tempSpeed.x = rigid.velocity.x;
            rigid.velocity = tempSpeed;
        }
        if(Mathf.Abs(rigid.velocity.x) < minSpeed)
        {
            if(rigid.velocity.x < 0)
            {
                tempSpeed.x = -minSpeed;
            }
            else tempSpeed.x = minSpeed;
            tempSpeed.y = rigid.velocity.y;
            rigid.velocity = tempSpeed;
        }

        rigid.rotation += Time.deltaTime;

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerAttack")
        {
            tempSpeed.x = rigid.velocity.x * .7f;
            tempSpeed.y = rigid.velocity.y;
            if (tempSpeed.y < 0)
            {
                tempSpeed.y *= -3f;
            }
            else if (tempSpeed.y > 0)
            {
                tempSpeed.y *= 3f;
            }
            rigid.velocity = tempSpeed;
        }

        if(other.gameObject.tag == "BasicTarget")
        {
            other.gameObject.SetActive(false);
            score += 1;
            SetCountText();
        }
/*
        if(other.gameObject.tag == "EnemyBullet")
        {
            other.gameObject.SetActive(false);
        }
*/
        if(other.gameObject.tag == "PlayerBullet")
        {
            tempSpeed.x = rigid.velocity.x;
            tempSpeed.y = rigid.velocity.y;
            if (tempSpeed.y < 0)
            {
                tempSpeed.y *= -1.5f;
            }
            else if (tempSpeed.y > 0)
            {
                tempSpeed.y *= 1.5f;
            }
            rigid.velocity = tempSpeed;
        }
    }

    void SetCountText()
    {
        countText.text = "Score: " + (score/2).ToString();
        if (score/2 == goalScore) {
            winText.text = "Congratulations!";
        }
    }
}
