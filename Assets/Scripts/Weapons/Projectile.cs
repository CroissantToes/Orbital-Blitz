using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float travelSpeed;

    private void Start()
    {
        StartCoroutine(Expire());
    }

    private void FixedUpdate()
    {
        transform.position += transform.up * travelSpeed * Time.deltaTime;
    }

    private IEnumerator Expire()
    {
        yield return new WaitForSeconds(5f);

        Destroy(gameObject);
    }
}
