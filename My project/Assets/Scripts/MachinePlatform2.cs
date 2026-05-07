using UnityEngine;

public class MachinePlatform2 : Machine {
	public Transform destination;
	public float travelTime;
	public float endpointPause;
	public float distanceResist;
	public float offAxisDistanceResist;
	public float speedResist;
	public float offAxisSpeedResist;

	Vector3 origin;
	Vector3 dest;
	Vector3 direction;
	float distance;
	Rigidbody rb;
	float currentTravelTime;
	bool reversing;

	void Start() {
		origin = transform.position;
		dest = destination.position;
		direction = dest - origin;
		distance = direction.magnitude;
		direction /= distance;
		Destroy(destination.gameObject); //We don't need to keep the destination object, now that we have saved the position.
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate() {
		if (Active) {
			float accel;
			float expectedDistance;
			float expectedSpeed;
			if (currentTravelTime < travelTime) {
				float timeFactor = currentTravelTime/travelTime;

				accel = ((2*timeFactor - 3)*timeFactor + 1)*60*distance*timeFactor/(travelTime*travelTime);
				expectedDistance = ((2*timeFactor - 5)*3*timeFactor + 10)*distance*timeFactor*timeFactor*timeFactor;
				expectedSpeed = ((timeFactor - 2)*timeFactor + 1)*30*distance*timeFactor*timeFactor/travelTime;
			} else {
				accel = 0;
				expectedDistance = distance;
				expectedSpeed = 0;
				
			}
			if (reversing) {
				expectedDistance = distance - expectedDistance;
				expectedSpeed = -expectedSpeed;
			}

			Vector3 offAxisDistance = transform.position - origin;
			float actualDistance = Vector3.Dot(offAxisDistance, direction);
			offAxisDistance -= direction*actualDistance;

			Vector3 offAxisSpeed = rb.linearVelocity;
			float actualSpeed = Vector3.Dot(offAxisSpeed, direction);
			offAxisSpeed -= direction*actualSpeed;

			rb.AddForce((reversing ? -1 : 1)*accel*direction 
					-offAxisSpeedResist*offAxisSpeed 
					-speedResist*(actualSpeed - expectedSpeed)*direction 
					-offAxisDistanceResist*offAxisDistance 
					-distanceResist*(actualDistance - expectedDistance)*direction, ForceMode.Acceleration);

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

	public override void OnDeactivate() {
		rb.isKinematic = true;
	}
}
