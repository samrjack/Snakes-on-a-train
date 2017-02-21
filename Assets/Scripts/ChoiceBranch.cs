using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceBranch : Track {

    public GameObject[] parts;
	// Use this for initialization
	void Start () {
		parts[1].GetComponent<Renderer>().enabled = false;
        state = 0;
    }
	
	// Update is called once per frame
	void Update () {
        parts[state].transform.position = this.transform.position;
	}

    public override void changeState()
    {
        if (state == 0)
        {
            state = 1;
            parts[1].transform.position = parts[0].transform.position;
            parts[0].GetComponent<Renderer>().enabled = false;
            parts[1].GetComponent<Renderer>().enabled = true;
        }
        else
        {
            state = 0;
            parts[0].transform.position = parts[1].transform.position;
            parts[1].GetComponent<Renderer>().enabled = false;
            parts[0].GetComponent<Renderer>().enabled = true;
        }
    }

    public override void setOrentation(int d)
    {
        if (d == 1)
            dir = 3 - dir;
    }

    public override void setDir(int dir)
    {
        this.dir = dir;
    }

    public override char inout(char dir)
    {
        if(state == 0)
        {
        
            if (this.dir == 0 || this.dir == 2)
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
        else
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
    }


    public override void rotate()
    {
        this.transform.Rotate(0, 0, 90);
        parts[0].transform.Rotate(0, 0, 90);
        parts[1].transform.Rotate(0, 0, 90);
        dir = (dir + 1) % 4;
    }

    public override string identify()
    {
        return "forking track";
    }

    //public override Track copy()
    //{
    //    return Instantiate(
    //}
}
