using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public GameObject background;

    public Train staticTrain; //initial static Train
    public List<Train> trainList; //collection of all trains

    private int frameCount;

	// Use this for initialization
	void Start () {
        frameCount = 0;
        trainList = new List<Train>();

        // Create background images
        for (int i = 0; i < 20; i++)
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
        frameCount++;
        if(frameCount % 50 == 0)
        {
            if (frameCount == 50)
            {
                trainList.Add(newTrain());
            }
        }

        foreach(Train train in trainList)
        {
            train.incrementTranslation(.0001f);
        }
	}

    Train newTrain()
    {
        Vector3 startPoint;
        startPoint.x = 0;
        startPoint.y = 10;
        startPoint.z = -1;
        Train newTrain = (Train) Instantiate(staticTrain, startPoint, Quaternion.identity);
        newTrain.setTranslation(.005f);
        return newTrain;
    }
}
