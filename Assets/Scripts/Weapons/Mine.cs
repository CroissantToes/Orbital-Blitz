using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public Animator animator;
    public GameObject mineSprite;
    public SpriteRenderer radiusSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        mineSprite.SetActive(false);
        radiusSprite.enabled = false;
        animator.SetBool("Exploding", true);
        SoundManager.Instance.PlayExplosion();

        yield return null;

        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);

        Destroy(gameObject);
    }
}
