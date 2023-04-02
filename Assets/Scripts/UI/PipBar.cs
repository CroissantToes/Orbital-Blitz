using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipBar : MonoBehaviour
{
    public GameObject[] pips;
    private int pipLevel = 0;
    private int PipLevel
    {
        get { return pipLevel; }
        set 
        {
            //prevents bar level from being lower than 0 or higher than 5
            if(value <= pips.Length && value >= 0)
            {
                pipLevel = value;
            }
        }
    }

    private void Awake()
    {
        foreach (var pip in pips)
        {
            if(pip.activeInHierarchy == true)
            {
                PipLevel++;
            }
        }
    }

    public void RaisePips()
    {
        if(pipLevel < pips.Length)
        {
            pips[PipLevel].SetActive(true);
            PipLevel++;
        }
    }

    public void LowerPips()
    {
        PipLevel--;
        pips[PipLevel].SetActive(false);
    }

    //sets bar to 0 pips
    public void EmptyBar()
    {
        foreach(var pip in pips)
        {
            pip.SetActive(false);
        }

        PipLevel = 0;
    }
}
