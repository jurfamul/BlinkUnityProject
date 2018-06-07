using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Control : MonoBehaviour {

    Animator animator;
    SpriteRenderer playerShip;
    public GameObject sceneManager;
    public Text[] counters;
    public float movementSpeed;
    public float blinkRadius;
    public float blinkCoolDown;
    public int maxBlinks;
    private int points;
    private int currentBlinks;
    private int lives;
    private float flashTime;
    private bool isDead;

        // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        playerShip = GetComponent<SpriteRenderer>();
        currentBlinks = maxBlinks;
        StartCoroutine(BlinkRecharge());
        StartCoroutine(UpdateUI());
        lives = sceneManager.GetComponent<Player_Singleton>().lives;
        points = sceneManager.GetComponent<Player_Singleton>().getPoints();
        isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDead)
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
            if (!isDead)
            {
                KillPlayer();
            }
            /*if (lives == 0)
            {
                StartCoroutine("LoadStartScene");
                Destroy(gameObject, 1.2f);
            }
            else
            {
                lives--;
                StartCoroutine("ReloadScene");
            }*/
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("bullet") || collision.gameObject.CompareTag("obsatcle")
            || collision.gameObject.CompareTag("boss"))
        {
            if (!isDead)
            {
                KillPlayer();
            }
            /*if (lives == 0)
            {
                StartCoroutine("LoadStartScene");
                Destroy(gameObject, 1.2f);
            }
            else
            {
                lives--;
                StartCoroutine("ReloadScene");
            } */
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
                                GameObject enemy = hit.collider.gameObject;
                                enemy.SendMessage("EndCoroutines");
                                enemy.GetComponent<Collider2D>().enabled = false;
                                enemy.GetComponent<Animator>().Play("death_animation");
                                points = sceneManager.GetComponent<Player_Singleton>().addPoints(
                                    enemy.GetComponent<Enemy_Points_value>().points);
                                Destroy(enemy, 1.2f);
                            }
                        }
                    }
                    gameObject.GetComponents<AudioSource>()[0].Play();
                    transform.position = warpPoint;
                    GetComponent<Collider2D>().enabled = true;
                    currentBlinks--;
                }
                else 
                {
                    Debug.Log("Can not warp to location (" + warpPoint.x + ", " + warpPoint.y + ").");
                    StartCoroutine(FlashColor(Color.blue));
                }
            }
            else
            {
                Debug.Log("Blink on cooldown");
                StartCoroutine(FlashColor(Color.red));
            }
        }
    }

    private void KillPlayer()
    {
        isDead = true;
        StopCoroutine("BlinkRecharge");
        StopCoroutine("FlashColor");
        gameObject.GetComponent<Collider2D>().enabled = false;
        animator.Play("player_death");
        gameObject.GetComponents<AudioSource>()[1].Play();
        sceneManager.GetComponent<Player_Singleton>().WasHit();
        lives = sceneManager.GetComponent<Player_Singleton>().lives;
    }

    protected virtual IEnumerator BlinkRecharge()
    {
        while (true)
        {
            if (currentBlinks != maxBlinks)
            {
                // calculate the end time based on the duration
                flashTime = Time.time + blinkCoolDown;

                // now loop until the current elapsed time is greater than the end time
                while (Time.time < flashTime)
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

    protected virtual IEnumerator FlashColor(Color color)
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

    /*public IEnumerator ReloadScene()
    {
        float endTime = Time.time + loadTime;

        while (Time.time < endTime)
        {
            yield return null;
        }

        SceneManager.LoadSceneAsync("Level 1");
    }

    public IEnumerator LoadStartScene()
    {
        float endTime = Time.time + loadTime;

        while (Time.time < endTime)
        {
            yield return null;
        }

        SceneManager.LoadSceneAsync("Start_Screen");
        SceneManager.UnloadSceneAsync("Level 1");
    }*/

    public IEnumerator UpdateUI()
    {
        while (true)
        {
            if (counters[0] && counters[1] && counters[2] != null)
            {
                counters[0].text = "Lives: " + lives;
                counters[1].text = "Blinks: " + currentBlinks;
                counters[2].text = "Points:" + points;
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                yield return null;
            }
        }
    }
}
