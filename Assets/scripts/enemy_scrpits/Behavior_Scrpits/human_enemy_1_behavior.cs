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
    Animator animator;
    SpriteRenderer sprite;
   

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        path = StartCoroutine(FollowWaypoints());
        StartTimer();
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
        GameObject tempBullet0 = Instantiate<GameObject>(bullet);
        tempBullet0.transform.parent = gameObject.transform;
        tempBullet0.transform.position = transform.position;
        GameObject tempBullet1 = Instantiate<GameObject>(bullet);
        tempBullet1.transform.parent = gameObject.transform;
        tempBullet1.transform.position = transform.position;
        gameObject.GetComponent<AudioSource>().Play();
         
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
                while (transform.position != point)
                {
                    transform.position = Vector3.MoveTowards(transform.localPosition, point, speed);
                    Vector3 position = transform.localPosition;
                    position.z = 9;
                    transform.localPosition = position;
                    yield return null;
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

    private Coroutine path;
}
