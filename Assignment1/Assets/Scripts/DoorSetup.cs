// Arthiran Sivarajah - 100660300
// 2022/02/09
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSetup : MonoBehaviour
{
    [SerializeField]
    private GameObject DoorOuter;

    [HideInInspector]
    public bool isSet = false;
    [HideInInspector]
    public bool isHot = false;
    [HideInInspector]
    public bool isNoisy = false;
    [HideInInspector]
    public bool isSafe = false;

    // Start is called before the first frame update
    void Start()
    {
        // Randomizes colours of prefabs
        DoorOuter.GetComponent<Renderer>().materials[1].color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }
}
