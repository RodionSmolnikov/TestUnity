using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Threading;
using UnityEngine;

public class gameOfLifeEngine : MonoBehaviour
{

    float speed = 0.1f;
    float timer = 0;

    static int HEIGH = 100;
    static int WIDTH = 100;

    public GameObject[,] field = new GameObject[HEIGH, WIDTH];
    public bool[,] preAlive = new bool[HEIGH, WIDTH];
    public bool[,] actAlive = new bool[HEIGH, WIDTH];
    // Start is called before the first frame update
    // Init start fields


    void Start()
    {
        for(int i = 0; i < HEIGH; i++)
        {
            for(int j = 0; j < WIDTH; j++)
            {
                GameObject q = GameObject.CreatePrimitive(PrimitiveType.Quad);
                q.transform.position = new Vector3(i, j, 0);
                field[i, j] = q;

                if (i == 0 || i == HEIGH - 1 || j == 0 || j == WIDTH - 1)
                {
                    actAlive[i, j] = false;
                }
                else {
                    actAlive[i, j] = UnityEngine.Random.Range(0, 100) % 3 == 0;
                    print(actAlive[i, j]);
                }
            }
        }
        flush();
    }

    // Update is called once per frame
    // Calculate next iteration
    void Update()
    {
        
        if (timer >= speed)
        {
            for (int i = 1; i < HEIGH - 1; i++)
            {
                for (int j = 1; j < WIDTH - 1; j++)
                {
                    int neighbors = calcNeigh(i, j);

                    if (!preAlive[i, j] && neighbors == 3)
                    {
                        actAlive[i, j] = true;
                    }

                    if (preAlive[i, j] && (neighbors > 3 || neighbors < 2))
                    {
                        actAlive[i, j] = false;
                    }
                }
            }
            flush();
            timer = 0;
        }
        else {
            timer += Time.deltaTime;
        }
        
       
    }

    private int calcNeigh(int i, int j)
    {
        int result = 0;

        if (preAlive[i + 1, j + 1]) result++;
        if (preAlive[i + 1, j]) result++;
        if (preAlive[i + 1, j - 1]) result++;

        if (preAlive[i, j + 1]) result++;
        if (preAlive[i, j - 1]) result++;
        
        if (preAlive[i - 1, j + 1]) result++;
        if (preAlive[i - 1, j]) result++;
        if (preAlive[i - 1, j - 1]) result++;

        return result;
    }

    void flush() {
        for (int i = 0; i < HEIGH; i++)
            for (int j = 0; j < WIDTH; j++) {
                field[i, j].GetComponent<Renderer>().enabled = actAlive[i,j];
                preAlive[i, j] = actAlive[i, j];        
            }
    }



}
