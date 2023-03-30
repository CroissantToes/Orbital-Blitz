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

    public void EmptyBar()
    {
        foreach(var pip in pips)
        {
            pip.SetActive(false);
        }

        PipLevel = 0;
    }
}
