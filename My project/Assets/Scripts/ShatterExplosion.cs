using UnityEngine;

public class ShatterExplosion : MonoBehaviour, ExplosionReceiver {
	public Shatterable shatter;
	public float shatterForce = 6.2f;

	public void ReceiveExplosion(Vector3 position, Vector3 force, float power) {
		Debug.Log(power);
		if (power > shatterForce) {
			shatter.Shatter(force.normalized);
		}
	}
}
