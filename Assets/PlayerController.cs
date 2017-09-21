using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    public int count = 0;
    public Text countText;
    public Text msgTest;
    private float fadeOutTime = 1.5f;
    private float reflectionMagnitude = 3.5f;
    public int victoryCount = 10;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        SetCountText();
        msgTest.text = "";
    }
	
	// Phys update
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(speed*movement);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("pickup")) {
            other.gameObject.SetActive(false);
            ++count;
            SetCountText();
            if (count >= victoryCount) {
                DeclareWin();
            }
        }
        else if (other.gameObject.CompareTag("mapBoundary")) {
            DeclareLose();
        }
        else if (other.gameObject.CompareTag("bouncy")) {
            TransformVelocityBounce(other);
        }
    }

    void SetCountText() {
        countText.text = "Count: " + count.ToString();
    }

    void DeclareWin() {
        msgTest.text = "You win!\nR to restart";
    }

    void DeclareLose() {
        msgTest.canvasRenderer.SetAlpha(0f);
        msgTest.CrossFadeAlpha(1f, fadeOutTime, false);
        msgTest.text = "YOU DIED\nR to restart";
    }

    void TransformVelocityBounce(Collider other) {
        Debug.Log(rb.velocity);
        Vector3 r = other.transform.position - rb.transform.position;
        Vector3 unitPerp = r / Vector3.Magnitude(r);
        Vector3 unitParallel = Vector3.Cross(new Vector3(0f, 1f, 0f), unitPerp);
        rb.velocity = (
            -reflectionMagnitude * Vector3.Dot(rb.velocity, unitPerp)* unitPerp + Vector3.Dot(rb.velocity, unitParallel) * unitParallel);
        Debug.Log(rb.velocity);
    }

}
