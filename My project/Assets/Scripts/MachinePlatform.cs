using UnityEngine;

public class MachinePlatform : Machine {
	public Transform destination;
	public float travelTime;
	public float endpointPause;

	Vector3 origin;
	Vector3 dest;
	float travelFactor;
	bool reversing;
	float pauseTime;

	void Start() {
		origin = transform.position;
		dest = destination.position;
		Destroy(destination.gameObject);
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        if (Active) {
			if (pauseTime == 0) {
				float travelDelta = Time.fixedDeltaTime/travelTime;
				if (reversing) {
					travelFactor -= travelDelta;
					if (travelFactor <= 0) {
						reversing = false;
						travelFactor = 0;
						pauseTime = endpointPause;
					}
				} else {
					travelFactor += travelDelta;
					if (travelFactor >= 1) {
						reversing = true;
						travelFactor = 1;
						pauseTime = endpointPause;
					}
				}
				transform.position = Vector3.Lerp(origin, dest, travelFactor);
			} else {
				pauseTime -= Time.fixedDeltaTime;
				if (pauseTime < 0) {
					pauseTime = 0;
				}
			}
		}
    }

	public override void OnActivate() {
		pauseTime = endpointPause;
	}

	public override void OnDeactivate() {}
}
