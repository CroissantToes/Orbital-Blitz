using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRateInSeconds; //desired time between spawns
    public float travelSpeed;
    public TravelDirection orbitDirection;

    private Vector2 origin = new Vector2(0f, 0f);
    private float coolDown; //actual time since last spawn


    private void Update()
    {
        coolDown += Time.deltaTime;

        //spawns enemies if not on cooldown and if there are still enemies that need to be spawned
        if(coolDown >= spawnRateInSeconds && GameManager.Instance.enemiesLeftToSpawn > 0)
        {
            GameManager.Instance.enemiesLeftToSpawn--;
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            coolDown = 0f;
        }
    }

    private void FixedUpdate()
    {
        //controls spawner orbiting
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
