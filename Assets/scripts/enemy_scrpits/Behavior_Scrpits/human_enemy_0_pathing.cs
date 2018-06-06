using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class human_enemy_0_pathing : MonoBehaviour {

    Animator animator;
    SpriteRenderer ship;
    Vector3 spawnPoint;
    float speedX;
    float speedY;
    public bool direction;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        ship = GetComponent<SpriteRenderer>();
        spawnPoint = transform.parent.transform.position;
        spawnPoint.x += -12.3f;
        spawnPoint.y += 6.25f;
        spawnPoint.z += 10;
        transform.position = spawnPoint;
        speedX = Random.Range(5, 10);
        speedY = Random.Range(1, 2);
        animator.SetBool("isBanking", true);
        animator.SetBool("isHazard", true);
        direction = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (!animator.GetBool("isDead"))
        {
            float x = 0;
            if (transform.position.x < 8.3f && direction)
            {
                ship.flipX = false ;
                x = -speedX * Time.deltaTime;
            }
            else if (transform.position.x >= 8.3f && direction)
            {
                ship.flipX = true;
                x = speedX * Time.deltaTime;
                direction = false;
            }
            else if (transform.position.x > -8.3f && !direction)
            {
                x = speedX * Time.deltaTime;
            }
            else if (transform.position.x <= -8.3 && !direction)
            {
                ship.flipX = false;
                x = -speedX * Time.deltaTime;
                direction = true;
            }

            float y = speedY * Time.deltaTime;

            transform.Translate(x, 0, 0);
            transform.Translate(0, y, 0);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.Equals(GameObject.Find("Player_Ship")))
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            animator.Play("death_animation");
            Destroy(gameObject, 1.2f);
        }
        else if (!collision.gameObject.Equals(GameObject.Find("Player_Ship")))
        {
            direction = !direction;
            speedY += Random.Range(0, 2);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.Equals(GameObject.Find("Player_Ship")))
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            animator.Play("death_animation");
            Destroy(gameObject, 1.2f);
        }
        else if (!collision.gameObject.Equals(GameObject.Find("Player_Ship")))
        {
            direction = !direction;
            speedY += Random.Range(0, 2);
        }
    }
}
