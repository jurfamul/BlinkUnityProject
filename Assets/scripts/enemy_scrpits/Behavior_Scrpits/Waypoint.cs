using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    public float speed;
    public List<Vector3> waypoints;

	// Use this for initialization
	void Start () {
        path = StartCoroutine(FollowWaypoints());
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
                point.Set(point.x, point.y, 8);
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
            yield return null;
        }
    }

    private Coroutine path;
}
