using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stright : Track {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void setDir(int dir)
    {
        this.dir = dir;
    }

    public override void rotate()
    {
        this.transform.Rotate(0, 0, 90);
        dir = (dir + 1) % 4;
    }

    public override char inout(char dir)
    {
        if(this.dir == 0 || this.dir == 2)
        {
            if (dir == 'N' || dir == 'S')
                return dir;
        }
        else
        {
            if (dir == 'E' || dir == 'W')
                return dir;
        }

        return 'Z';
    }

    public override string identify()
    {
        return "Stright track!";
    }
}
