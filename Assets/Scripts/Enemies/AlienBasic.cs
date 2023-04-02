using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBasic : Alien
{
    [Header("Basic Maneuver Parameters")]
    public float maneuverSpeed;
    public float directionSwitchTime;

    private float directionSwitchTimer;

    private void Start()
    {
        directionSwitchTimer = directionSwitchTime;
    }

    /* Alien orbits to the side a short distance (based on speed over time,
     * not actual distance), then does the same in the other direction.*/
    protected override void Maneuver()
    {
        float movement = maneuverSpeed * Time.deltaTime;

        if(direction == TravelDirection.clockwise)
        {
            transform.RotateAround(origin, Vector3.back, movement);
        }
        else
        {
            transform.RotateAround(origin, Vector3.back, -movement);
        }

        directionSwitchTimer -= Time.deltaTime;

        if(directionSwitchTimer <= 0)
        {
            if(direction == TravelDirection.clockwise)
            {
                direction = TravelDirection.counterclockwise;
            }
            else
            {
                direction = TravelDirection.clockwise;
            }

            directionSwitchTimer = directionSwitchTime;
        }
    }
}
