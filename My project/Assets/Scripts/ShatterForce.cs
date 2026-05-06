using UnityEngine;

public class ShatterForce : Shatterable, ExplosionReceiver {
	public float shatterForce = 6.2f;

	#nullable enable
	Rigidbody? rb;
	#nullable disable

	public void Start() {
		if (!TryGetComponent(out rb)) {
			rb = null;
		}
	}

	public void OnCollisionEnter(Collision collision) {
		if (collision.relativeVelocity.magnitude > shatterForce) {
			Shatter(CalculateReflectVector(collision));
		}
	}

	public void ReceiveExplosion(Vector3 position, Vector3 force, float power) {
		if (power/(rb == null ? 1 : rb.mass) > shatterForce) {
			Shatter(force.normalized);
		}
	}
}
