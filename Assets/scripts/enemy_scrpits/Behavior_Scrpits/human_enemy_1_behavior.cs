using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class human_enemy_1_behavior : MonoBehaviour {
    
    public float speed;
    public float fireRate;         // the time between each shot fired.
    public List<Vector3> waypoints;
    public GameObject bullet;
    public float shots;
    private float endTime;
    private bool isDead;
    Animator animator;
    SpriteRenderer sprite;
   

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        path = StartCoroutine(FollowWaypoints());
        isDead = false;
        StartTimer();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            KillEnemy();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            KillEnemy();
        }
    }

    public virtual void StartTimer()
    {
        StartCoroutine(StartCountdown());
    }

    public float timeLeft()
    {
        float left = endTime - Time.time;
        return (left <= 0.0f ? 0.0f : left);
    }

    protected virtual IEnumerator StartCountdown()
    {
        while (true)
        {
            // calculate the end time based on the duration
            endTime = Time.time + fireRate;

            // now loop until the current elapsed time is greater than the end time
            while (Time.time < endTime)
            {
                yield return null;
            }

            for (int i = 0; i < shots; i++)
            {
                Shoot();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private void Shoot()
    {
        GameObject tempBullet0 = Instantiate<GameObject>(bullet, transform, true);
        tempBullet0.transform.position = new Vector3(tempBullet0.transform.position.x - 0.1f, tempBullet0.transform.position.y,
            tempBullet0.transform.position.z);
        GameObject tempBullet1 = Instantiate<GameObject>(bullet, transform, false);
        gameObject.GetComponent<AudioSource>().Play();
         
    }

    public void KillEnemy()
    {
        StopAllCoroutines();
        gameObject.GetComponent<Collider2D>().enabled = false;
        animator.Play("death_animation");
        isDead = true;
        Destroy(gameObject, 1.2f);
    }

    public virtual void StopPatroling()
    {
        StopCoroutine(path);
    }

    public IEnumerator FollowWaypoints()
    {
        while (true)
        {
            foreach (Vector3 point in waypoints)
            {
                Debug.Log("pathing to: " + point);
                Debug.DrawLine(transform.position, point, Color.white);
                Vector3 init = transform.position;
                float step = 0;
                Vector3 desination = new Vector3(point.x, point.y, -1.0f);
                while (transform.position != desination)
                {
                    step += speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(init, desination, step);
                    transform.position = new Vector3(transform.position.x, transform.position.y, -1.0f);
                    Debug.Log(transform.position);
                    yield return null;
                }
            }
            yield return null;
        }
    }

    private Coroutine path;
}
