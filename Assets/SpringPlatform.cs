using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlatform : MonoBehaviour {

    public float forceMagnitude;
    public float liftForce;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other) {
        Vector3 v = other.attachedRigidbody.velocity;
        Vector3 vProj = new Vector3(v[0], 0f, v[2]);
        Vector3 force = forceMagnitude * vProj / vProj.magnitude;
        force.Set(force[0], liftForce, force[2]);
        other.attachedRigidbody.AddForce(force);
    }
}
