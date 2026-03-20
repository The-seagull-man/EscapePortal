using UnityEngine;

public class ActivatorBattery : Activator
{
	bool activated = false;

    void OnCollisionEnter(Collision collision) {
		if (!activated) {
			if (collision.gameObject.CompareTag("Battery")) {
				activated = true;
				Active = true;
				Destroy(collision.gameObject);
			}
		}
	}
}
