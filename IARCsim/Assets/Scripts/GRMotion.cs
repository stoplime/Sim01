using UnityEngine;
using System.Collections;

public class GRMotion : MonoBehaviour {

	public float Speed;
	public float AngularSpeed;

	private float initialRot;
	public float InitialRot{
		get{return initialRot;}
		set{initialRot = value;}
	}

	private float rot;//final rotation without delay (radians)
	private float currentRot;//visual rotation (radians)
	private float time;
	private bool play;

	private float counter5;
	private float counter20;

	// Use this for initialization
	void Start () {
		play = false;
		time = 0;
		rot = initialRot;
		currentRot = initialRot;

		counter5 = 0;
		counter20 = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump")) {
			play = !play;
		}
		if (play) {
			float delta = Time.deltaTime;
			time += delta;
			counter5 += delta;
			counter20 += delta;

			if (counter20 >= 20) {
				counter20 = 0;
				rot += Mathf.PI + RotateNoise (3);
			}
			if (counter5 >= 5) {
				counter5 = 0;
				rot += RotateNoise (20) + RotateNoise (3);
			}
			//adjusting rotation
			float deltaRot = rot - currentRot;
			Debug.Log(rot);
			Vector3 angularVel = new Vector3 (0, AngularSpeed, 0) * Time.deltaTime;
			if (deltaRot != 0) {
				angularVel *= deltaRot / Mathf.Abs (deltaRot);
			}
			if (angularVel.magnitude > deltaRot) {
				angularVel *= 0;
			}
			this.transform.Rotate (-angularVel*180/Mathf.PI);
			currentRot += angularVel.magnitude;

			//adjusting Velocity
			//if (Mathf.Abs(deltaRot) < 5*Mathf.PI/180) {
				Vector3 vel = new Vector3 (Mathf.Cos (currentRot), 0, Mathf.Sin (currentRot)) * Speed * Time.deltaTime;
				
				this.transform.localPosition += vel;
			//}
		}
	}

	public float RotateNoise (float angle) {
		return ((Random.value * 2 * angle) - angle) * Mathf.PI / 180;
	}
}
