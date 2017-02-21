using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {
    private Train nextInChain;
    private int lastInChain = 1; //last in chain if ==1 2 indicates newly created

    private char direction;
    private float translation = 0.0f;

    private float xTrain;
    private float yTrain;

    // Use this for initialization
    void Start() {
        direction = 'E';

        nextInChain = null;
	}
	
	// Update is called once per frame
	void Update () {

        xTrain = transform.position.x;
        yTrain = transform.position.y;

        // Translation
        if (direction == 'E')
        {
            transform.Translate(translation, 0, 0);
        }
        else if (direction == 'N')
        {
            transform.Translate(0, translation, 0);
        }
        else if (direction == 'W')
        {
            transform.Translate(-translation, 0, 0);
        }
        else if (direction == 'S')
        {
            transform.Translate(0, -translation, 0);
        }
    }

    public void setIsLastInChain(int i)
    {
        lastInChain = i;
    }

    public void setTranslation(float f)
    {
        translation = f;
    }

    public void incrementTranslation(float f)
    {
        translation += f;
        if(nextInChain != null) nextInChain.incrementTranslation(f);
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.name == "station" || coll.name == "station(Clone)")
        {
            if (lastInChain == 1)
            {
                Vector3 newCoord;
                
                newCoord.x = xTrain;
                newCoord.y = yTrain;
                newCoord.z = -1;
                if (direction == 'E')
                {
                    newCoord.x -= 1;
                    nextInChain = Instantiate(this, newCoord, Quaternion.identity);
                    nextInChain.setTranslation(translation);
                    nextInChain.setIsLastInChain(2);
                }
                else if (direction == 'S')
                {
                    newCoord.y -= 1;
                    nextInChain = Instantiate(this, newCoord, Quaternion.identity);
                    nextInChain.setTranslation(translation);
                    nextInChain.setIsLastInChain(2);
                }
                else if (direction == 'N')
                {
                    newCoord.y += 1;
                    nextInChain = Instantiate(this, newCoord, Quaternion.identity);
                    nextInChain.setTranslation(translation);
                    nextInChain.setIsLastInChain(2);
                }
                else if (direction == 'W')
                {
                    newCoord.x += 1;
                    nextInChain = Instantiate(this, newCoord, Quaternion.identity);
                    nextInChain.setTranslation(translation);
                    nextInChain.setIsLastInChain(2);
                }
                lastInChain--;
            }
            else if (lastInChain >= 2)
            {
                lastInChain--;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
        {
        if (coll.name == "trackNW(Clone)")
        {
            if (direction == 'E')
            {
                transform.position = new Vector3(Mathf.Floor(xTrain + 1), yTrain, -1);
                direction = 'N';
            }
            else if (direction == 'S')
            {
                transform.position = new Vector3(xTrain, Mathf.Floor(yTrain), -1);
                direction = 'W';
            }
        }
        else if (coll.name == "trackNE(Clone)")
        {
            if (direction == 'W')
            {
                transform.position = new Vector3(Mathf.Floor(xTrain), yTrain, -1);
                direction = 'N';
            }
            else if (direction == 'S')
            {
                transform.position = new Vector3(xTrain, Mathf.Floor(yTrain), -1);
                direction = 'E';
            }
        }
        else if (coll.name == "trackSW(Clone)")
        {
            if (direction == 'E')
            {
                transform.position = new Vector3(Mathf.Floor(xTrain + 1), yTrain, -1);
                direction = 'S';
            }
            else if (direction == 'N')
            {
                transform.position = new Vector3(xTrain, Mathf.Floor(yTrain + 1), -1);
                direction = 'W';
            }
        }
        else if (coll.name == "trackSE(Clone)")
        {
            if (direction == 'W')
            {
                transform.position = new Vector3(Mathf.Floor(xTrain), yTrain, -1);
                direction = 'S';
            }
            else if (direction == 'N')
            {
                transform.position = new Vector3(xTrain, Mathf.Floor(yTrain + 1), -1);
                direction = 'E';
            }
        }
    }

}
