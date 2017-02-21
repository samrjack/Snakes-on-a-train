using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : Track
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void rotate()
    {
        this.transform.Rotate(0, 0, 90);
        dir = (dir + 1) % 4;
    }

    public override void setOrentation(int d)
    {
        if (d == 1)
            dir = 3 - dir;
    }

    public override char inout(char dir) // direction object is going
    {
        if (this.dir == 0)
        {
            if (dir == 'N')
                return 'E';
            if (dir == 'W')
                return 'S';
        }
        else if (this.dir == 1)
        {
            if (dir == 'W')
                return 'N';
            if (dir == 'S')
                return 'E';
        }
        else if (this.dir == 2)
        {
            if (dir == 'S')
                return 'W';
            if (dir == 'E')
                return 'N';
        }
        else
        {
            if (dir == 'E')
                return 'S';
            if (dir == 'N')
                return 'W';
        }

        return 'Z';
    }

    public override string identify()
    {
        return "Curved track!";
    }
}
