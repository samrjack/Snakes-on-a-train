using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SnapObject : MonoBehaviour {

    public Track[] tracks;

    public GameObject background;

    public int placedParts = 0;
    private const int gridHeight = 16;
    private const int gridWidth = 26;
    private Track[,] objGrid = new Track[gridHeight, gridWidth];
    private GameObject[,] bkGrid = new GameObject[gridHeight, gridWidth];
    public int pGW = gridWidth;

    private Track nextTrack;
    private Vector3 initPosition;
    private Vector3 finalPosition;

    public Train staticTrain;
    private Train tTrain;
    private Vector3 nextPos;

    private int score;

    float startSpeed = .01f;

    public GameObject goAudio;
    
    public GameObject win;

    private bool dead = false;

    public GameObject lighting;
    public void Start()
    {
        lighting.transform.position = new Vector3(-gridWidth / 2 + 3, 0, -5);
        score = 0;
        nextTrack = tracks[0];
        nextTrack.rotate();
        nextTrack.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);

        // Create background images
        for (int i = -gridHeight/2; i < gridHeight/2; i++)
        {
            for (int j = -gridWidth/2; j < gridWidth/ 2; j++)
            {
                Vector3 temp = new Vector3();
                temp.x = j;
                temp.y = i;
                temp.z = background.transform.position.z;
                bkGrid[i + gridHeight/2, j + gridWidth/2] = Instantiate(background,temp,Quaternion.identity);
            }
        }

        GameObject.Find("Score").GetComponent<TextMesh>().transform.position = new Vector3( - gridWidth / 2, gridHeight / 2, 2);

    }

    public void Update()
    {
        if(dead)
        {
            
            Color tempC = tTrain.GetComponent<SpriteRenderer>().color;
            if (tempC.a > 0)
            {
                float newa = tTrain.GetComponent<SpriteRenderer>().color.a - (Time.deltaTime/2);
                tTrain.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, newa);
            }
            else
                SceneManager.LoadScene("GameOver");
        }
        else 
           {
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            MousePos.x = Mathf.Round(MousePos.x);
            MousePos.y = Mathf.Round(MousePos.y);
            MousePos.z = 0;
            if (placedParts == 0)
                MousePos.x = -gridWidth / 2;

            if (MousePos.x < -gridWidth / 2)
                MousePos.x = -gridWidth / 2;
            if (MousePos.x > gridWidth / 2 - 1)
                MousePos.x = gridWidth / 2 - 1;

            if (MousePos.y < -gridHeight / 2)
                MousePos.y = -gridHeight / 2;
            if (MousePos.y > gridHeight / 2 - 1)
                MousePos.y = gridHeight / 2 - 1;

            nextTrack.transform.position = MousePos;

            // Creating new instance of a train track
            if (Input.GetButtonDown("Fire1") && objGrid[(int)MousePos.y + gridHeight / 2, (int)MousePos.x + gridWidth / 2] == null)
            {
                if (placedParts == 0)
                {
                    initPosition = MousePos;
                    initPosition.z = staticTrain.transform.position.z;
                }
                else if (placedParts == 2)
                {
                    makeTrain(initPosition);
                }
                placedParts++;

                nextTrack.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                objGrid[(int)MousePos.y + gridHeight / 2, (int)MousePos.x + gridWidth / 2] = nextTrack.copy();
                bkGrid[(int)MousePos.y + gridHeight / 2, (int)MousePos.x + gridWidth / 2].GetComponent<Renderer>().enabled = false;
                nextPiece();
            }

            // Rotating the train track
            if (Input.GetButtonDown("Fire2") && placedParts != 0)
            {
                Track temp = objGrid[(int)MousePos.y + gridHeight / 2, (int)MousePos.x + gridWidth / 2];

                if (temp == null)
                    nextTrack.rotate();
                else
                    temp.changeState();
            }

            if (placedParts > 2)
            {
                tTrain.moveTrain();
                ColisionDetection();
            }

            //TextMesh textObject = GameObject.Find("Score").GetComponent<TextMesh>();
            //textObject.text = "Score:\n" + score;
            if (placedParts > 2)
            {
                for (int i = 0; i < gridHeight; i++)
                {
                    for (int j = 0; j < gridWidth; j++)
                    {
                        if (objGrid[i, j] != null)
                        {
                            //if (i == (int)initPosition.y + gridHeight / 2 && j == (int)initPosition.x + gridWidth / 2)
                            //    continue;

                            if (i == (int)finalPosition.y + gridHeight / 2 && j == (int)finalPosition.x + gridWidth / 2)
                                continue;

                            if (!objGrid[i, j].countTime())
                            {
                                objGrid[i, j] = null;
                                bkGrid[i, j].GetComponent<Renderer>().enabled = true;
                            }
                        }
                    }
                }
            }

            TextMesh textObject = GameObject.Find("Score").GetComponent<TextMesh>();
            textObject.text = "Score:\n" + score * 125;
        }
    }

    public void makeTrain(Vector3 pos)
    {
        nextPos.x = pos.x;
        nextPos.y = pos.y;
        nextPos.z = pos.z;
        pos.x -= 3;
        tTrain = staticTrain;
        tTrain.transform.position = pos;
        tTrain.setTranslation(startSpeed);
        tTrain.setDir('E');

        finalPosition.z = nextTrack.transform.position.z;
        finalPosition.x = gridWidth/2 - 1;
        finalPosition.y = Random.Range(0, gridHeight - 1) - gridHeight/2;

        objGrid[(int)finalPosition.y + gridHeight / 2, (int)finalPosition.x + gridWidth / 2] = Instantiate(objGrid[(int)initPosition.y + gridHeight / 2, (int)initPosition.x + gridWidth / 2], finalPosition, Quaternion.identity);

        objGrid[(int)finalPosition.y + gridHeight / 2, (int)finalPosition.x + gridWidth / 2].transform.Rotate(0, 0, 90);

        bkGrid[(int)finalPosition.y + gridHeight / 2, (int)finalPosition.x + gridWidth / 2].GetComponent<Renderer>().enabled = false;
    }

    private void nextPiece()
    {
        while (nextTrack.dir != 0)
            nextTrack.rotate();
        nextTrack.GetComponent<Renderer>().enabled = false;

        int i = Random.Range(0, tracks.Length);
        nextTrack = tracks[i];
        nextTrack.GetComponent<Renderer>().enabled = true;
        nextTrack.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        i = Random.Range(0, 3);
        while (nextTrack.dir != i)
            nextTrack.rotate();
        nextTrack.setDir(i);

        
    }

    private void setNextPos()
    {

        score++;

        if (nextPos.x == finalPosition.x && nextPos.y == finalPosition.y)
        {
            removeGoal();
            makeTrain(initPosition);
            score += 10;
            startSpeed = tTrain.getTranslation();
            win.GetComponent<AudioSource>().Play();
            return;
        }


        switch (tTrain.getDir())
        {
            case 'N':
                nextPos.x = Mathf.Round(tTrain.transform.position.x);
                nextPos.y = Mathf.Round(tTrain.transform.position.y) + 1;
                break;
            case 'S':
                nextPos.x = Mathf.Round(tTrain.transform.position.x);
                nextPos.y = Mathf.Round(tTrain.transform.position.y) - 1;
                break;
            case 'E':
                nextPos.x = Mathf.Round(tTrain.transform.position.x) + 1;
                nextPos.y = Mathf.Round(tTrain.transform.position.y);
                break;
            case 'W':
                nextPos.x = Mathf.Round(tTrain.transform.position.x) - 1;
                nextPos.y = Mathf.Round(tTrain.transform.position.y);
                break;
            default:
                break;
        }

        tTrain.setTranslation(tTrain.getTranslation() + .0003f);

        if (nextPos.x > gridWidth/2 - 1 || nextPos.x < -gridWidth / 2  ||
            nextPos.y > gridHeight / 2 - 1 || nextPos.y < -gridHeight / 2)
        {
            Debug.Log("Dead here");
            EndGame();
        }
    }

    private void ColisionDetection()
    { 

        Track nextSpot = objGrid[(int)nextPos.y + gridHeight/2, (int)nextPos.x + gridWidth / 2];

        if (nextSpot == null)
            EndGame();

        if (tTrain.getDir() == 'E')
        {

            if (tTrain.transform.position.x >= nextPos.x)
            {
                tTrain.transform.position = nextPos;
                tTrain.setDir(nextSpot.inout('E'));
                setNextPos();
            }
            else if (tTrain.transform.position.x >= nextPos.x - .5)
            {
                if (nextSpot == null || nextSpot.inout('E') == 'Z')
                {
                    EndGame();
                }
            }
        }

        else if (tTrain.getDir() == 'N')
        {

            if (tTrain.transform.position.y >= nextPos.y)
            {
                tTrain.transform.position = nextPos;
                tTrain.setDir(nextSpot.inout('N'));
                setNextPos();
            }
            else if (tTrain.transform.position.y >= nextPos.y - .5)
            {
                if (nextSpot == null || nextSpot.inout('N') == 'Z')
                {
                    EndGame();
                }
            }
        }

        else if (tTrain.getDir() == 'S')
        {

            if (tTrain.transform.position.y <= nextPos.y)
            {
                tTrain.transform.position = nextPos;
                tTrain.setDir(nextSpot.inout('S'));
                setNextPos();
            }
            else if (tTrain.transform.position.y <= nextPos.y + .5)
            {
                if (nextSpot == null || nextSpot.inout('S') == 'Z')
                {
                    EndGame();
                }
            }
        }

        else if (tTrain.getDir() == 'W')
        {

            if (tTrain.transform.position.x <= nextPos.x)
            {
                tTrain.transform.position = nextPos;
                tTrain.setDir(nextSpot.inout('W'));
                setNextPos();
            }
            else if (tTrain.transform.position.x <= nextPos.x + .5)
            {
                if (nextSpot == null || nextSpot.inout('W') == 'Z')
                {
                    EndGame();
                }
            }
        }
    }

    private void EndGame()
    {
        dead = true;



        goAudio.GetComponent<AudioSource>().Play();

  
    }

    private void removeGoal()
    {
        objGrid[(int)finalPosition.y + gridHeight / 2, (int)finalPosition.x + gridWidth / 2].GetComponent<Renderer>().enabled = false;
        objGrid[(int)finalPosition.y + gridHeight / 2, (int)finalPosition.x + gridWidth / 2] = null;
        bkGrid[(int)finalPosition.y + gridHeight / 2, (int)finalPosition.x + gridWidth / 2].GetComponent<Renderer>().enabled = true;

        placedParts = 0;
        while (nextTrack.dir != 0)
            nextTrack.rotate();
        nextTrack.GetComponent<Renderer>().enabled = false;

        nextTrack = tracks[0];
        nextTrack.GetComponent<Renderer>().enabled = true;
        while (nextTrack.dir != 1)
            nextTrack.rotate();
        nextTrack.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
    }
}
