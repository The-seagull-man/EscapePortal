using System.Collections.Generic;
using UnityEngine;

public class Explosive : Machine
{
    public int timer = 450;
    public float flashRateStart = .25f;
	public float flashRateEnd = 5;
	public float radius;
	public float powerMax;
	public float powerMin;
	public float upwardBoost;

	Material mat;
	int countdown;

	public void Start() {
		mat = GetComponent<MeshRenderer>().material;
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        if (Active) {
			countdown++;
			if (countdown == timer) {
				Explode();
			} else {
				float timerFactor = (countdown - 1f)/(timer);

				float flashFactor = (1 - (Mathf.Cos(((flashRateEnd - flashRateStart)*timerFactor/2 + flashRateStart)*timerFactor*Mathf.PI*timer*Time.fixedDeltaTime) + 1)/2);
				mat.color = new Color((1 + flashFactor*7), flashFactor*(1 + flashFactor*7), 0);
		    }
		}
    }

    public void Explode() {
		List<GameObject> hits = new();
		foreach (Collider c in Physics.OverlapSphere(transform.position, radius)) {
			GameObject hit = c.gameObject;
			if (hit == gameObject || hits.Contains(hit)) {
				continue;
			}
			hits.Add(hit);
			Rigidbody rb = null;
			ExplosionReceiver receiver = null;
			hit.TryGetComponent(out rb);
			hit.TryGetComponent(out receiver);
			if (rb != null || receiver != null) {
				Vector3 direction = hit.transform.position - transform.position;
				float distance = direction.magnitude;
				if (distance == 0) {
					direction = Vector3.up;
				} else {
					direction /= distance;
				}
				distance /= radius;
				float power = Mathf.LerpUnclamped(powerMax, powerMin, distance*distance);
				if (power > 0) {
					Vector3 force = (direction + Vector3.up*upwardBoost)*power;
					if (rb != null) {
						rb.AddForce(force, ForceMode.Impulse);
					}
					if (receiver != null) {
						receiver.ReceiveExplosion(transform.position, force, power);
					}
				}
			}
		}
		Destroy(gameObject);
    }

	public override void OnActivate() {
        if (timer == 0) {
            Explode();
        } else {
			mat.color = new Color(1, 0, 0);
		}
    }

	public override void OnDeactivate() {
		mat.color = new Color(1, 1, 1);
		countdown = 0;
    }
}
