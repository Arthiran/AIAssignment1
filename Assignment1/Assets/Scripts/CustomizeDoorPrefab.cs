using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeDoorPrefab : MonoBehaviour
{
    [SerializeField]
    private GameObject DoorOuter;

    // Start is called before the first frame update
    void Start()
    {
        DoorOuter.GetComponent<Renderer>().materials[1].color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }
}
