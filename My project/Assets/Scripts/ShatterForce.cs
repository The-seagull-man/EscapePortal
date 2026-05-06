using UnityEngine;

public class ShatterForce : MonoBehaviour, ExplosionReceiver {
	public Shatterable shatter;
	public float shatterForce = 6.2f;

	public void OnCollisionEnter(Collision collision) {
		if (collision.relativeVelocity.magnitude > shatterForce) {
			shatter.Shatter(shatter.CalculateReflectVector(collision));
		}
	}

	public void ReceiveExplosion(Vector3 position, Vector3 force, float power) {
		if (power > shatterForce) {
			shatter.Shatter(force.normalized);
		}
	}
}
