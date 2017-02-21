using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{

    public string ends;
    public int dir;
    public int oren;
    public int state;
    private float timer = 30;
	// Use this for initialization
	void Start () {
        state = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void setOrentation(int d)
    {
        oren = d;
            
    }

    public virtual void setDir(int dir)
    {
        this.dir = dir;
    }

    public virtual char inout(char dir)
    {
        Debug.Log("WRONG FUNCTION");
        return 'Z';
    }

    public virtual void rotate()
    {
        this.transform.Rotate(0, 0, 90);
        dir = (dir + 1) % 4;
    }

    public virtual string identify()
    {
        return "string base class";
    }

    public virtual void changeState()
    {
       state = ( state == 1 ? 0 : 1);
    }

    public virtual Track copy()
    {
        return Instantiate(this);
    }

    public virtual bool countTime()
    {
        timer -= Time.deltaTime;
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .1f + timer / 30);

        if(timer <= 0)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0);
            return false;
        }
        return true;
    }

    public virtual bool isTrack()
    {
        return true;
    }
}
