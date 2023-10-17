using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallToggle : MonoBehaviour
{
    public GameObject[] normalWalls;    // Drag your normal rectangular walls here
    public GameObject[] pointedWalls;   // Drag your '^' and 'v' walls here

    public void SwitchToNormalWalls()
    {
        foreach (var wall in normalWalls)
        {
            wall.SetActive(true);
        }
        foreach (var wall in pointedWalls)
        {
            wall.SetActive(false);
        }
    }

    public void SwitchToPointedWalls()
    {
        foreach (var wall in normalWalls)
        {
            wall.SetActive(false);
        }
        foreach (var wall in pointedWalls)
        {
            wall.SetActive(true);
        }
    }
}
