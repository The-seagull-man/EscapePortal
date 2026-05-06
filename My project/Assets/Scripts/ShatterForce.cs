using UnityEngine;

public class ShatterForce : Shatterable, ExplosionReceiver {
	public float shatterForce = 6.2f;

	public void OnCollisionEnter(Collision collision) {
		if (collision.relativeVelocity.magnitude > shatterForce) {
			Shatter(CalculateReflectVector(collision));
		}
	}

	public void ReceiveExplosion(Vector3 position, Vector3 force, float power) {
		if (power > shatterForce) {
			Shatter(force.normalized);
		}
	}
}
