using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour {

    Animator animator;
    SpriteRenderer playerShip;
    public float movementSpeed;
    public float warpRadius;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        playerShip = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!animator.GetBool("isDead"))
        {
            float horizontalDirection = Input.GetAxis("Horizontal");
            if (Input.GetButtonDown("Horizontal"))
            {
                if (horizontalDirection > 0.00001)
                {
                    playerShip.flipX = true;
                }
                else if (horizontalDirection < -0.00001)
                {
                    playerShip.flipX = false;
                }

                animator.SetBool("isBanking", true);
            }
            else if (Input.GetButtonUp("Horizontal"))
            {
                playerShip.flipX = false;
                animator.SetBool("isBanking", false);
            }

            horizontalDirection = 0;

            var x = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
            var y = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

            transform.Translate(x, 0, 0);
            transform.Translate(0, y, 0);

            Warp();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("isDead", true);
        Destroy(gameObject, 1.2f);
    }

    private void Warp()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 warpPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            warpPoint.z = 0;

            float warpDistance = Mathf.Abs(Vector3.Distance(transform.position, warpPoint));
            print(warpDistance);
            if (warpDistance <= warpRadius)
            {
                Debug.DrawLine(transform.position, warpPoint, Color.red, 60f);
                RaycastHit warpHit;
                if (Physics.Linecast(transform.position, warpPoint, out warpHit))
                {
                    print("warpHit");
                    // raycast collition is not functioning properly. Need to work on understanding collitions.
                    Destroy(warpHit.collider.gameObject);
                }
                transform.position = warpPoint;
                GetComponent<Collider2D>().enabled = true;
            }
            else if (Vector3.Distance(transform.position, warpPoint) <= warpRadius)
            {
                print("Can not warp to location (" + warpPoint.x + ", " + warpPoint.y + ").");
            }
            
        }
    }
}
