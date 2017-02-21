using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Main Camera").GetComponent<SnapObject>().placedParts < 2)
        {
            this.transform.position = new Vector3(GameObject.Find("Main Camera").GetComponent<SnapObject>().pGW / 2, 0, -5);
        }
        else
        {


            float xTrain = GameObject.Find("trainSphere").transform.position.x;
            float yTrain = GameObject.Find("trainSphere").transform.position.y;

            transform.position = new Vector3(xTrain, yTrain, -5);


        }

    }
}
