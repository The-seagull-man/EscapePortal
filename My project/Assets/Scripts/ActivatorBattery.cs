using UnityEngine;

public class ActivatorBattery : Activator
{
	bool activated = false;

    void OnCollisionEnter(Collision collision) {
		if (!activated) {
			if (collision.gameObject.CompareTag("Battery")) {
				activated = true;
				SetActivate(true);
				Destroy(collision.gameObject);
			}
		}
	}
}
