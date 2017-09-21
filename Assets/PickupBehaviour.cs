using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour {

    private Vector3 rotateHeading;
    private float accelerationRate = 5f;
    private float accelerationInterval = 0.1f;
    private float lastAccelTime;
    private float bobbingPeriod = 1.5f;
    private float bobbingPhase;
    private float bobbingAmplitude = 0.1f;

    // Use this for initialization
    void Start () {
        rotateHeading = new Vector3(Random.Range(0f, 90f), Random.Range(0f, 90f), Random.Range(0f, 90f));
        lastAccelTime = Time.time;
        bobbingPhase = Random.Range(0f, 2f*Mathf.PI);
    }
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(rotateHeading * Time.deltaTime);

        // Change the rotation
        if (Time.time - lastAccelTime >= accelerationInterval) {
            lastAccelTime = Time.time;
            rotateHeading += accelerationRate * new Vector3(5f*Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
        // Bob
        transform.Translate(bobbingAmplitude * new Vector3(0f, 2f * Mathf.PI / bobbingPeriod * Time.deltaTime * Mathf.Cos(2f * Mathf.PI / bobbingPeriod * Time.time + bobbingPhase)), null);

    }
}
