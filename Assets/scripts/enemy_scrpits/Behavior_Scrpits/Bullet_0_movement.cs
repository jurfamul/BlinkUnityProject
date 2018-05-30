using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_0_movement : MonoBehaviour {

    public float speed;
    
    // Use this for initialization
    void Start () {
        transform.position = gameObject.transform.parent.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.Translate(step, 0, 0, Space.Self);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obsatcle") || collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
