using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAdvanced : Alien
{
    [Header("Advanced Maneuver Parameters")]
    public float maneuverSpeed;
    public float shortDirectionSwitchTime;
    public float longDirectionSwitchTime;
    public int longManeuversBeforeSwitch;

    private float directionSwitchTimer;
    private ManeuverType maneuverType;

    private int longManeuverCount = 0;

    private enum ManeuverType
    {
        shortManeuver,
        longManeuver,
    }

    private void Start()
    {
        maneuverType = ManeuverType.longManeuver;
        directionSwitchTimer = longDirectionSwitchTime;
    }

    protected override void Maneuver()
    {
        float movement = maneuverSpeed * Time.deltaTime;

        if (direction == TravelDirection.clockwise)
        {
            transform.RotateAround(origin, Vector3.back, movement);
        }
        else
        {
            transform.RotateAround(origin, Vector3.back, -movement);
        }

        directionSwitchTimer -= Time.deltaTime;

        if (directionSwitchTimer <= 0)
        {
            if (direction == TravelDirection.clockwise)
            {
                direction = TravelDirection.counterclockwise;
            }
            else
            {
                direction = TravelDirection.clockwise;
            }

            if(maneuverType == (ManeuverType.longManeuver))
            {
                if(longManeuverCount < longManeuversBeforeSwitch)
                {
                    longManeuverCount++;
                    directionSwitchTimer = shortDirectionSwitchTime;
                    maneuverType = ManeuverType.shortManeuver;
                }
                else
                {
                    longManeuverCount = 0;
                    directionSwitchTimer = longDirectionSwitchTime;
                    maneuverType = ManeuverType.longManeuver;
                }
            }
            else
            {
                directionSwitchTimer = longDirectionSwitchTime;
                maneuverType = ManeuverType.longManeuver;
            }

        }
    }
}
