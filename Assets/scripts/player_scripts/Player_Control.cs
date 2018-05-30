using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour {

    Animator animator;
    SpriteRenderer playerShip;
    public float movementSpeed;
    public float blinkRadius;
    public float blinkCoolDown;
    public int maxBlinks;
    private int currentBlinks;
    private float endTime;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        playerShip = GetComponent<SpriteRenderer>();
        currentBlinks = maxBlinks;
        StartCoroutine(blinkRecharge());
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("bullet") || collision.gameObject.CompareTag("obsatcle")
            || collision.gameObject.CompareTag("boss"))
        {
            animator.SetBool("isDead", true);
            Destroy(gameObject, 1.2f);
        }
    }

    private void Warp()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentBlinks > 0)
            {
                Vector3 warpPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                warpPoint.z = 0;

                float warpDistance = Mathf.Abs(Vector3.Distance(transform.position, warpPoint));
                if (warpDistance <= blinkRadius)
                {
                    GetComponent<Collider2D>().enabled = false;
                    Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
                    Vector2 warpPoint2D = new Vector2(warpPoint.x, warpPoint.y);
                    Vector2 warpDirection = warpPoint2D - playerPosition;
                    RaycastHit2D[] warpHits = Physics2D.RaycastAll(playerPosition, warpDirection, warpDistance);
                    Debug.DrawRay(transform.position, warpDirection, Color.red, 2f);

                    if (warpHits.Length != 0)
                    {
                        foreach (RaycastHit2D hit in warpHits)
                        {
                            if (hit.collider.gameObject.CompareTag("enemy"))
                            {
                                Destroy(hit.collider.gameObject);
                            }
                        }
                    }
                    gameObject.GetComponent<AudioSource>().Play();
                    transform.position = warpPoint;
                    GetComponent<Collider2D>().enabled = true;
                    currentBlinks--;
                }
                else 
                {
                    print("Can not warp to location (" + warpPoint.x + ", " + warpPoint.y + ").");
                    StartCoroutine(flashColor(Color.blue));
                }
            }
            else
            {
                Debug.Log("Blink on cooldown");
                StartCoroutine(flashColor(Color.red));
            }
        }
    }

    protected virtual IEnumerator blinkRecharge()
    {
        while (true)
        {
            if (currentBlinks != maxBlinks)
            {
                // calculate the end time based on the duration
                endTime = Time.time + blinkCoolDown;

                // now loop until the current elapsed time is greater than the end time
                while (Time.time < endTime)
                {
                    yield return null;
                }

                currentBlinks++;
                yield return null;
            }
            else
            {
                yield return null;
            }
        }
    }

    protected virtual IEnumerator flashColor(Color color)
    {
        float endFlash = Time.time + 1;
        Color32 flashingColor = color;
        bool isColor = false;
        while (Time.time < endFlash)
        {
            if (!isColor)
            {
                playerShip.material.color = flashingColor;
                isColor = true;
                yield return new WaitForSeconds(0.06666666f);
            }
            else
            {
                playerShip.material.color = Color.white;
                isColor = false;
                yield return new WaitForSeconds(0.06666666f);
            }
        }
        playerShip.material.color = Color.white;
    }
}
