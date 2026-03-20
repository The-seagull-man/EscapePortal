using UnityEngine;

public class MachinePlatform : Machine
{
	public float travelTime;
	public Transform destination;

	Vector3 origin;
	Vector3 dest;
	float travelFactor;
	bool reversing;

	void Start() {
		origin = transform.position;
		dest = destination.position;
		Destroy(destination.gameObject);
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        if (Active) {
			float travelDelta = Time.fixedDeltaTime/travelTime;
			if (reversing) {
				travelFactor -= travelDelta;
				if (travelFactor <= 0) {
					reversing = false;
					travelFactor = 0;
				}
			} else {
				travelFactor += travelDelta;
				if (travelFactor >= 1) {
					reversing = true;
					travelFactor = 1;
				}
			}
			transform.position = Vector3.Lerp(origin, dest, travelFactor);
		}
    }

	public override void OnActivate() {}

	public override void OnDeactivate() {}
}
