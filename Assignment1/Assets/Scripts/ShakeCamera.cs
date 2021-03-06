// Arthiran Sivarajah - 100660300
// 2022/02/09
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [HideInInspector]
    public bool shouldShake = false;
    public AnimationCurve ShakeCurve;
    private float duration = 1f;

    Vector3 currentLocation = new Vector3(0, 0, 0);

    private void Start()
    {
        // Sets original Camera Location
        SetCameraLocation(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if door is noisy, starts coroutine for shaking
        if (shouldShake)
        {
            shouldShake = false;
            StartCoroutine(PlayCameraShake());
        }
        else
        {
            transform.position = GetCameraLocation();
        }
    }

    private IEnumerator PlayCameraShake()
    {
        // Uses an animation curve to shake camera position
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = ShakeCurve.Evaluate(elapsedTime / duration);
            transform.position = GetCameraLocation() + Random.insideUnitSphere * (strength / 2f);
            yield return null;
        }
        transform.position = GetCameraLocation();
    }

    public void SetCameraLocation(Vector3 _NewLocation)
    {
        currentLocation = _NewLocation;
    }

    private Vector3 GetCameraLocation() { return currentLocation; }
}
