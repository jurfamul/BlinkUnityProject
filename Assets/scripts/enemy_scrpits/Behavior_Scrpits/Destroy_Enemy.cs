using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Enemy : MonoBehaviour {

    Animator animator;

    public void KillEnemy()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        animator.Play("death_animation");
        Destroy(gameObject, 1.2f);
    }
}
