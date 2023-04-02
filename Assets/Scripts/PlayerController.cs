using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private Controls controls;
    private Vector2 move; //vector representing movement input
    public float moveSpeed;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public float cooldownInSeconds;
    public Transform spawnPoint;
    private bool cooldownOn = false;

    [Header("Mine")]
    public GameObject minePrefab;
    public float mineSpawnDistanceFromPlayer;
    [HideInInspector] public bool canMine = false;

    [Header("Boosters")]
    public GameObject boosterRight;
    public GameObject boosterLeft;
    public float boostFlickerScale; //booster object scale oscillates between 1 and this number when flickering
    private bool boostExpand = false; //if the booster is expanding or shrinking

    private Vector2 origin = new Vector2(0f, 0f);

    private void Awake()
    {
        //assigns actions to controls
        controls = new Controls();
        controls.Default.Movement.performed += ctx => Fly(ctx);
        controls.Default.Movement.canceled += ctx => StopFlying();

        controls.Default.Shoot.performed += ctx => Shoot();

        controls.Default.Mine.performed += ctx => LayMine();
    }

    private void Update()
    {
        //manages rocket booster animations
        if (move.x > 0)
        {
            boosterLeft.SetActive(true);
            if (boostExpand)
            {
                boosterLeft.transform.localScale = new Vector3(boostFlickerScale, 1f, 1f);
                boostExpand = false;
            }
            else
            {
                boosterLeft.transform.localScale = Vector3.one;
                boostExpand = true;
            }

        }
        else if (move.x < 0)
        {
            boosterRight.SetActive(true);
            if (boostExpand)
            {
                boosterRight.transform.localScale = new Vector3(boostFlickerScale, 1f, 1f);
                boostExpand = false;
            }
            else
            {
                boosterRight.transform.localScale = Vector3.one;
                boostExpand = true;
            }
        }
    }

    private void FixedUpdate()
    {
        //actual movement around planet
        float movement = move.x * moveSpeed * Time.deltaTime;
        transform.RotateAround(origin, Vector3.back, movement);
    }

    private void OnEnable()
    {
        controls.Default.Enable();
    }

    private void OnDisable()
    {
        controls.Default.Disable();
    }

    //sets move vector value
    private void Fly(InputAction.CallbackContext ctx)
    {
        move = ctx.ReadValue<Vector2>();
        SoundManager.Instance.PlayBooster();
    }

    //stops player movement
    private void StopFlying()
    {
        move = Vector2.zero;
        boosterLeft.SetActive(false);
        boosterRight.SetActive(false);
        boostExpand = false;
        SoundManager.Instance.StopBooster();
    }

    private void Shoot()
    {
        if(cooldownOn == false)
        {
            SoundManager.Instance.PlayShot();
            Instantiate(projectilePrefab, spawnPoint.position, transform.rotation);
            cooldownOn = true;
            StartCoroutine(ShotCooldown());
        }
    }

    private void LayMine()
    {
        if(canMine == true)
        {
            Vector3 spawnPos = new Vector3 (transform.up.x * mineSpawnDistanceFromPlayer, transform.up.y * mineSpawnDistanceFromPlayer, transform.position.z);
            Instantiate(minePrefab,  transform.position + spawnPos, transform.rotation);
            canMine = false;
            GameManager.Instance.OnMineUsed();
        }
    }

    private IEnumerator ShotCooldown()
    {
        yield return new WaitForSeconds(cooldownInSeconds);

        cooldownOn = false;
    }
}
