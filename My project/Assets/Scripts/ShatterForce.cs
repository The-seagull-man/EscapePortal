using UnityEngine;

public class ShatterForce : Shatterable {
	public float shatterForce = 6.2f;

	public void OnCollisionEnter(Collision collision) {
		if (collision.relativeVelocity.magnitude > shatterForce) {
			Shatter(CalculateReflectVector(collision));
		}
	}
}
