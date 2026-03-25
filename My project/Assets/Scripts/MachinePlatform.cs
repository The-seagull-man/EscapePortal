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
		Destroy(destination.gameObject); //We don't need to keep the destination object, now that we have saved the position.
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        if (Active) {
			//If the platform is not paused.
			if (pauseTime == 0) {
				//Calculate amount to move per frame.
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
				//Tick down pause timer.
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
