using UnityEngine;

public class ActivatorBattery : Activator
{
    void OnCollisionEnter(Collision collision) {
		if (!Active) {
			//If the object that collides is a battery, consume the battery to turn on the machine.
			if (collision.gameObject.CompareTag("Battery")) {
				Active = true;
				Destroy(collision.gameObject);
			}
		}
	}
}
