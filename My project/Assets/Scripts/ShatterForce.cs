using UnityEngine;

public class ShatterForce : Shatterable {
	public float shatterForce = 6.2f;

	public void OnCollisionEnter(Collision collision) {
		if (collision.relativeVelocity.magnitude > shatterForce) {
			Vector3 reflect = Vector3.Reflect(collision.relativeVelocity, collision.GetContact(0).normal);
			Shatter(reflect);
		}
	}
}
