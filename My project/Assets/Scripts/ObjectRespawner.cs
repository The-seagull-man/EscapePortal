using Unity.VisualScripting;
using UnityEngine;

public class ObjectRespawner : MonoBehaviour {
	public GameObject template;
	public int delay;

	GameObject obj;
	int delayCounter;

	void Start() {
		obj = Instantiate(template, transform.position, transform.rotation);
	}

	void FixedUpdate() {
		if (delayCounter > 0) {
			delayCounter--;
			if (delayCounter == 0) {
				obj = Instantiate(template, transform.position, transform.rotation);
			}
		} else if (obj.IsDestroyed()) {
			if (delay == 0) {
				obj = Instantiate(template, transform.position, transform.rotation);
			} else {
				delayCounter = delay;
			}
		}
	}
}