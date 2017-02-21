using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObject : MonoBehaviour {

    public GameObject trackVertical;
    public GameObject trackHorizontal;
    public GameObject trackNW;
    public GameObject trackNE;
    public GameObject trackSW;
    public GameObject trackSE;

    public GameObject trackVerticalFaded;
    public GameObject trackHorizontalFaded;
    public GameObject trackNWFaded;
    public GameObject trackNEFaded;
    public GameObject trackSWFaded;
    public GameObject trackSEFaded;

    public GameObject background;

    private GameObject prefab;
    private GameObject prefabFaded;

    private float rnd;

    public void Start()
    {
        rnd = Random.Range(0.0f, 1.0f);

        if (rnd < 0.01)
        {
            /*
            prefab = trackVertical;
            prefabFaded = trackVerticalFaded;
            */
            prefab = trackNW;
            prefabFaded = trackNWFaded;
        } else
        {
            prefab = trackNW;
            prefabFaded = trackNWFaded;
        }

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

    public void Update()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        pz.x = Mathf.Floor(pz.x);
        pz.y = Mathf.Floor(pz.y);
        pz.z = 0;

        // Creating new instance of a train track
        if (Input.GetButtonDown("Fire1"))
        {
            if ((pz.x >= 0) && (pz.x <= 20) && (pz.y >= 0) && (pz.y <= 20))
            {
                Instantiate(prefab, pz, Quaternion.identity);

                rnd = Random.Range(0.0f, 1.0f);

                if (rnd < 0)
                {
                    prefab = trackVertical;
                    prefabFaded = trackVerticalFaded;
                }
                else
                {
                    prefab = trackNW;
                    prefabFaded = trackNWFaded;
                }
            }
        }

        // Rotating the train track
        if (Input.GetButtonDown("Fire2"))
        {
            // Horzontal / Vertical
            if (prefabFaded == trackVerticalFaded)
            {
                prefabFaded.transform.position = new Vector3(-20, 0, 0);
                prefabFaded = trackHorizontalFaded;
                prefab = trackHorizontal;
            }
            else if (prefabFaded == trackHorizontalFaded)
            {
                prefabFaded.transform.position = new Vector3(-20, 0, 0);
                prefabFaded = trackVerticalFaded;
                prefab = trackVertical;
            }

            // Corners
            if ( prefabFaded == trackNWFaded)
            {
                prefabFaded.transform.position = new Vector3(-20, 0, 0);
                prefabFaded = trackNEFaded;
                prefab = trackNE;
            }
            else if ( prefabFaded == trackNEFaded)
            {
                prefabFaded.transform.position = new Vector3(-20, 0, 0);
                prefabFaded = trackSEFaded;
                prefab = trackSE;
            }
            else if (prefabFaded == trackSEFaded)
            {
                prefabFaded.transform.position = new Vector3(-20, 0, 0);
                prefabFaded = trackSWFaded;
                prefab = trackSW;
            }
            else if (prefabFaded == trackSWFaded)
            {
                prefabFaded.transform.position = new Vector3(-20, 0, 0);
                prefabFaded = trackNWFaded;
                prefab = trackNW;
            }
        }

        prefabFaded.transform.position = pz;
    }

}
