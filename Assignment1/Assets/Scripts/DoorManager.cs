using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DoorManager : MonoBehaviour
{
    [SerializeField, Range(20, 100)]
    private int TotalDoors = 20;

    [SerializeField]
    private GameObject DoorPrefab;

    [SerializeField]
    private GameObject FloorObject;

    [SerializeField]
    private GameObject LeftBtn;
    [SerializeField]
    private GameObject RightBtn;

    private int CurrentDoor = 0;

    [SerializeField]
    private Text DoorNumberText;

    private List<GameObject> AllDoors = new List<GameObject>();

    string[] AllLines;

    // Start is called before the first frame update
    void Start()
    {
        ReadTextFile();

        for (int i = 0; i < TotalDoors; i++)
        {
            Vector3 NewVector = new Vector3(i * -6, 0, 0);
            GameObject NewDoor = Instantiate(DoorPrefab, NewVector, Quaternion.identity);
            AllDoors.Add(NewDoor);
        }

        for (int i = 1; i < AllLines.Length; i++)
        {
            Debug.Log(AllLines[i]);
        }    

        Vector3 NewPosition = FloorObject.transform.position;
        NewPosition.x -= (3 * TotalDoors) - 3;
        FloorObject.transform.position = NewPosition;

        Vector3 NewScale = FloorObject.transform.localScale;
        NewScale.x *= TotalDoors;
        FloorObject.transform.localScale = NewScale;
    }

    private void Update()
    {
        if (CurrentDoor == 0)
        {
            LeftBtn.SetActive(false);
        }
        else
        {
            LeftBtn.SetActive(true);
        }

        if (CurrentDoor == TotalDoors - 1)
        {
            RightBtn.SetActive(false);
        }
        else
        {
            RightBtn.SetActive(true);
        }
    }

    public void IncrementCurrentDoor()
    {
        CurrentDoor++;

        Vector3 NewPosition = Camera.main.transform.position;
        NewPosition.x -= 6;
        Camera.main.transform.position = NewPosition;

        DoorNumberText.text = (CurrentDoor+1).ToString();
    }

    public void DecrementCurrentDoor()
    {
        CurrentDoor--;

        Vector3 NewPosition = Camera.main.transform.position;
        NewPosition.x += 6;
        Camera.main.transform.position = NewPosition;

        DoorNumberText.text = (CurrentDoor + 1).ToString();
    }

    void ReadTextFile()
    {
        string PathToFile = "G:\\AIForGaming\\AIAssignment1\\Assignment1\\Assets\\StreamingAssets\\probabilities.txt";

        AllLines = System.IO.File.ReadAllLines(PathToFile);
    }
}
