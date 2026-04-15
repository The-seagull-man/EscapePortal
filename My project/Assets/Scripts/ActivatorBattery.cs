using UnityEngine;

public class ActivatorBattery : Activator
{
	public float size;
    void OnCollisionEnter(Collision collision) {
		if (!Active) {
			//If the object that collides is a battery, consume the battery to turn on the machine.
			if (collision.gameObject.CompareTag("Battery")) {
				if (collision.gameObject.transform.localScale.x == size)
				{
					Active = true;
					Debug.Log("active");
					Destroy(collision.gameObject);

				}
			}
		}
	}
}
