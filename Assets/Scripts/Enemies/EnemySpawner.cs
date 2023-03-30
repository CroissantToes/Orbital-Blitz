using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRateInSeconds;
    public float travelSpeed;
    public TravelDirection orbitDirection;

    private Vector2 origin = new Vector2(0f, 0f);
    private float coolDown;


    private void Update()
    {
        coolDown += Time.deltaTime;

        if(coolDown >= spawnRateInSeconds && GameManager.Instance.enemiesLeftToSpawn > 0)
        {
            GameManager.Instance.enemiesLeftToSpawn--;
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            coolDown = 0f;
        }
    }

    private void FixedUpdate()
    {
        float movement = travelSpeed * Time.deltaTime;

        if(orbitDirection == TravelDirection.clockwise)
        {
            transform.RotateAround(origin, Vector3.back, movement);
        }
        else
        {
            transform.RotateAround(origin, Vector3.back, -movement);
        }
    }
}
