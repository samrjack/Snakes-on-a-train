using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

    

    private char direction;

    private float translation;

    private float xTrain;
    private float yTrain;

    // Use this for initialization
    void Start() {
        
        direction = 'N';

        translation = 0;
	}
	
	// Update is called once per frame
	void Update () {

        
    }

    public void moveTrain()
    {
        this.transform.Translate(Vector3.up * Time.deltaTime);
    }

    public int setDir(char dir)
    {
        if (direction == 'N')
        {
            if (dir == 'W')
            {
                Debug.Log("found west");
                direction = 'W';
                turnLeft();
            }
            else if (dir == 'E')
            {
                direction = 'E';
                turnRight();
            }
            else if (dir == 'S')
            {
                direction = 'S';
                turnRight();
                turnRight();
            }
            else if (dir == 'N')
            {

            }
            else
            {
                // Destruct perhaps
            }
            return 0;           
        }
        else if (direction == 'W')
        {
            if (dir == 'S')
            {
                direction = 'S';
                turnLeft();
            }
            else if (dir == 'N')
            {
                direction = 'N';
                turnRight();
            }
            else if (dir == 'E')
            {
                direction = 'E';
                turnRight();
                turnRight();
            }
            else if (dir == 'W')
            {

            }
            else
            {
                // Destruct perhaps
            }
            return 0;
        }
        else if (direction == 'E')
        {
            if (dir == 'N')
            {
                direction = 'N';
                turnLeft();
            }
            else if (dir == 'S')
            {
                direction = 'S';
                turnRight();
            }
            else if (dir == 'W')
            {
                direction = 'W';
                turnRight();
                turnRight();
            }
            else if (dir == 'E')
            {

            }
            else
            {
                // Destruct perhaps
            }
            return 0;
        }
        else if (direction == 'S')
        {
            if (dir == 'E')
            {
                direction = 'E';
                turnLeft();
            }
            else if (dir == 'W')
            {
                direction = 'W';
                turnRight();
            }
            else if (dir == 'N')
            {
                direction = 'N';
                turnRight();
                turnRight();
            }
            else if (dir == 'S')
            {

            }
            else
            {
                // Destruct perhaps
            }
            return 0;
        }
        return 0;

    }

    private void turnLeft()
    {
        this.transform.Rotate(0, 0, 90);
    }

    private void turnRight()
    {
        this.transform.Rotate(0, 0, -90);
    }

    public char getDir()
    {
        return direction;
    }

    public void setTranslation(float t)
    {
        translation = t;
    }

    public float getTranslation()
    {
        return translation;
    }
}
