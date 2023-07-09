using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    public bool alive = false;

    public void setAlive(bool alive) 
    {
        this.alive = alive;

        if (alive)
        {
            GetComponent<Renderer>().enabled = true;
        } else
        {
            GetComponent<Renderer>().enabled = false;
        }
    }
}
