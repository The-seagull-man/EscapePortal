using UnityEngine;

public class MachinePlatform : Machine {
	public Transform destination;
	public float travelTime;
	public float endpointPause;
	public float acceleration;
	public float friction;
	public float minTimeLeft;

	Vector3 origin;
	Vector3 dest;
	Rigidbody rb;
	float currentTravelTime;
	bool reversing;

	void Start() {
		origin = transform.position;
		dest = destination.position;
		Destroy(destination.gameObject); //We don't need to keep the destination object, now that we have saved the position.
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        if (Active) {
			Vector3 direction = (reversing ? origin : dest) - transform.position;
			float distance = direction.magnitude;
			direction = direction.normalized;

			float speed = rb.linearVelocity.magnitude;
			if (speed != 0) {
				speed = speed*Mathf.Max(Vector3.Dot(rb.linearVelocity/speed, direction), 0);
			}
			float timeLeft = travelTime - currentTravelTime;
			rb.AddForce(direction*(distance/Mathf.Max(timeLeft, minTimeLeft) - speed)*acceleration - rb.linearVelocity*friction, ForceMode.Acceleration);

			currentTravelTime += Time.fixedDeltaTime;
			if (currentTravelTime >= travelTime + endpointPause) {
				currentTravelTime -= travelTime + endpointPause;
				reversing = !reversing;
			}
		}
    }

	public override void OnActivate() {
		rb.isKinematic = false;
	}

	public override void OnDeactivate() {}
}
