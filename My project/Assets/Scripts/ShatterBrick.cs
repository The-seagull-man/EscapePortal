using UnityEngine;

public class ShatterBrick : Shatterable {
	public float shatterForce = 7;
	public string brickTag;

	public void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag(brickTag) && collision.relativeVelocity.magnitude > shatterForce) {
			Vector3 reflect = Vector3.Reflect(collision.relativeVelocity, collision.GetContact(0).normal);
			Shatter(reflect);
		}
	}
}
