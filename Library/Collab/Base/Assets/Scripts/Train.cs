using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

    public GameObject background;


    public Train staticCabboose;
    Train lastLocationCopy;

    private char direction;
    private float translation;

    private float xTrain;
    private float yTrain;

    // Use this for initialization
    void Start() {
        translation = 0.0f;
        direction = 'E';

        // Create background images
        for ( int i = 0; i < 20; i++ )
        {
            for (int j = 0; j < 20; j++)
            {
                Vector3 pz = new Vector3(i, j, 1);

                Instantiate(background, pz, Quaternion.identity);
            }
        }
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

    public Train extend()
    {
        Train newTrain = (Train)Instantiate(lastLocationCopy);
        return newTrain;
    }

    public void setTranslation(float f)
    {
        translation = f;
    }

    public void incrementTranslation(float f)
    {
        translation += f;
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
