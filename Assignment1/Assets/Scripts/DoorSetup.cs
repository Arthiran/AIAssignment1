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
        DoorOuter.GetComponent<Renderer>().materials[1].color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    private void OnMouseDown()
    {
        Debug.Log("Is Hot: " + isHot + " | Is Noisy: " + isNoisy + " | Is Safe: " + isSafe);
    }
}
