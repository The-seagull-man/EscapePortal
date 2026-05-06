using UnityEngine;

public class ShatterBrick : MonoBehaviour {
	public Shatterable shatter;
	public float shatterForce = 7;
	public string brickTag;

	public void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag(brickTag) && collision.relativeVelocity.magnitude > shatterForce) {
			shatter.Shatter(shatter.CalculateReflectVector(collision));
		}
	}
}
