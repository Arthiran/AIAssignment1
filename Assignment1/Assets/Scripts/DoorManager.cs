// Arthiran Sivarajah - 100660300
// 2022/02/09
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

    [SerializeField]
    private GameObject HotImageEffect;

    [SerializeField]
    private ShakeCamera CameraShakeScript;

    [SerializeField]
    private AudioSource NoiseSoundEffect;

    [SerializeField]
    private GameObject EndScreen;

    [SerializeField]
    private Text EndConditionText;

    // Start is called before the first frame update
    void Start()
    {
        // Reads through given text file
        ReadTextFile();
        //Gets amount of doors from menu scene
        TotalDoors = PlayerPrefs.GetInt("DoorAmount");

        // Spawns in doors according to total amount
        for (int i = 0; i < TotalDoors; i++)
        {
            Vector3 NewVector = new Vector3(i * -6, 0, 0);
            GameObject NewDoor = Instantiate(DoorPrefab, NewVector, Quaternion.identity);
            AllDoors.Add(NewDoor);
        }

        // Reads through text files and sets door values (Hot, Noisy, Safe) based on probabilities from text file
        for (int i = 1; i < AllLines.Length; i++)
        {
            float Probability = float.Parse((AllLines[i].Substring(6)));
            for (int j = 0; j < Mathf.RoundToInt(Probability * TotalDoors); j++)
            {
                SetDoorValies(AllLines[i][0] == 'Y' ? true : false, AllLines[i][2] == 'Y' ? true : false, AllLines[i][4] == 'Y' ? true : false);
            }
        }

        UpdateDoor(CurrentDoor);

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

    void ReadTextFile()
    {
        // Checks if text file exists, otherwise use default
        if (File.Exists(PlayerPrefs.GetString("FileLocation")))
        {
            AllLines = File.ReadAllLines(PlayerPrefs.GetString("FileLocation"));
        }
        else
        {
            AllLines = File.ReadAllLines(Application.streamingAssetsPath + "\\probabilities.txt");
        }
    }

    void SetDoorValies(bool isHot, bool isNoisy, bool isSafe)
    {
        // Sets values of door, makes sure it isn't already set
        int RandomInt = Random.Range(0, TotalDoors);

        if (AllDoors[RandomInt].GetComponent<DoorSetup>().isSet == false)
        {
            AllDoors[RandomInt].GetComponent<DoorSetup>().isSet = true;
            AllDoors[RandomInt].GetComponent<DoorSetup>().isHot = isHot;
            AllDoors[RandomInt].GetComponent<DoorSetup>().isNoisy = isNoisy;
            AllDoors[RandomInt].GetComponent<DoorSetup>().isSafe = isSafe;
        }
        else 
        {
            SetDoorValies(isHot,isNoisy, isSafe);
        }
    }

    public void IncrementCurrentDoor()
    {
        // Goes to next door
        CurrentDoor++;

        Vector3 NewPosition = Camera.main.transform.position;
        NewPosition.x -= 6;
        Camera.main.transform.position = NewPosition;
        CameraShakeScript.SetCameraLocation(NewPosition);

        DoorNumberText.text = (CurrentDoor + 1).ToString();

        UpdateDoor(CurrentDoor);
    }

    public void DecrementCurrentDoor()
    {
        // Goes to previous door
        CurrentDoor--;

        Vector3 NewPosition = Camera.main.transform.position;
        NewPosition.x += 6;
        Camera.main.transform.position = NewPosition;
        CameraShakeScript.SetCameraLocation(NewPosition);

        DoorNumberText.text = (CurrentDoor + 1).ToString();

        UpdateDoor(CurrentDoor);
    }

    private void UpdateDoor(int _CurrentDoor)
    {
        // Updates feedback according to door
        HotImageEffect.SetActive(AllDoors[_CurrentDoor].GetComponent<DoorSetup>().isHot);
        CameraShakeScript.shouldShake = AllDoors[_CurrentDoor].GetComponent<DoorSetup>().isNoisy;

        if (AllDoors[_CurrentDoor].GetComponent<DoorSetup>().isNoisy)
        {
            NoiseSoundEffect.Play();
        }
        else
        {
            NoiseSoundEffect.Stop();
        }
    }

    public void CheckDoor()
    {
        // Checks if door is safe or dangerous
        if (AllDoors[CurrentDoor].GetComponent<DoorSetup>().isSafe)
        {
            EndConditionText.text = "YOU ARE SAFE!";
        }
        else
        {
            EndConditionText.text = "YOU LOST!";
        }
    }
}
