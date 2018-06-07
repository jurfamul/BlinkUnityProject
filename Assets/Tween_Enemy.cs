using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween_Enemy : MonoBehaviour {

    public Vector3 endPoint;
    private Vector3 start;
    public float f;
    public float speed;
    Tweener.Tween enemyTween;

    // Use this for initialization
    void Start () {

        start = transform.position;
        enemyTween = Tweener.MakeTween(Tweener.TweenType.Quad);
    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine("Tween");
	}

    public IEnumerator Tween()
    {
        float step = Time.deltaTime * speed;
        while (true)
        {
            bool direction = false;
            if (f <= 0.8f && !direction)
            {
                f += 0.1f;
                Vector3 movement = enemyTween(start, endPoint, f);
                movement.x *= step;
                movement.y *= step;
                movement.z = -1;
                transform.Translate(movement);
            }
            else if (f >= 0.8f && !direction)
            {
                f -= 0.1f;
                Vector3 movement = enemyTween(start, endPoint, f);
                movement.x *= step;
                movement.y *= step;
                movement.z = -1;
                transform.Translate(movement);
                direction = true;
                endPoint *= -1;
                endPoint.y = -1;
            }
            else if (f >= 0.2f && direction)
            {
                f -= 0.1f;
                Vector3 movement = enemyTween(start, endPoint, f);
                movement.x *= step;
                movement.y *= step;
                movement.z = -1;
                transform.Translate(movement);
            }
            else if (f < 0.2f && direction)
            {
                f += 0.1f;
                Vector3 movement = enemyTween(start, endPoint, f);
                movement.x *= step;
                movement.y *= step;
                movement.z = -1;
                transform.Translate(movement);
                direction = false;
                endPoint *= -1;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
