using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Alien : MonoBehaviour
{
    public Animator animator;
    public float travelSpeed;

    private bool canMove = true;

    protected Vector2 origin = new Vector2(0f, 0f);

    protected TravelDirection direction;

    private void FixedUpdate()
    {
        if (canMove)
        {
            transform.position += transform.up * travelSpeed * Time.deltaTime;
            Maneuver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            GameManager.Instance.OnEnemyDeath(CauseOfDeath.projectile);
            Destroy(collision.gameObject);
            StartCoroutine(Die());
        }

        if (collision.gameObject.tag == "Planet")
        {
            GameManager.Instance.OnEnemyDeath(CauseOfDeath.planet);
            GameManager.Instance.CurrentPlanetHealth--;
            StartCoroutine(Die());
        }

        if(collision.gameObject.tag == "Mine")
        {
            StartCoroutine(Die(0.5f));
        }
    }

    private IEnumerator Die(float delay = 0)
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(delay);

        canMove = false;
        animator.SetBool("Exploding", true);
        SoundManager.Instance.PlayExplosion();
        GameManager.Instance.EnemiesRemaining--;

        yield return null;

        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);

        Destroy(gameObject);
    }

    protected abstract void Maneuver();
}
