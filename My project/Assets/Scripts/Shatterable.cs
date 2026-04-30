using UnityEngine;

public class Shatterable : MonoBehaviour
{
    public GameObject shard;
    public int shardCount = 4;
    public float shardScale = .1f;
    public float shatterForce = 6.2f;
    public float shardForceVariationMin = .16f;
	public float shardForceVariationMax = .32f;
    public float shardDisplacementMin = .02f;
	public float shardDisplacementMax = .1f;

	public void OnCollisionEnter(Collision collision) {
		if (collision.relativeVelocity.magnitude > shatterForce) {
            Vector3 reflect = Vector3.Reflect(collision.relativeVelocity, collision.GetContact(0).normal);
            Shatter(reflect);
        }
	}

    public void Shatter(Vector3 reflect) {
        for (int i = 0; i < shardCount; i++) {
            GenerateShard(reflect);
        }
        Destroy(gameObject);
    }

    public GameObject GenerateShard(Vector3 reflect) {
        GameObject shardInstance = Instantiate(shard);
        shardInstance.transform.localScale = Vector3.one*shardScale;
        shardInstance.transform.localRotation = Random.rotation;
        Vector3 direction = Random.onUnitSphere;
		shardInstance.GetComponent<Rigidbody>().linearVelocity =
                reflect + direction*Random.Range(shardForceVariationMin, shardForceVariationMax);
        shardInstance.transform.position = transform.position + direction*Random.Range(shardDisplacementMin, shardDisplacementMax);
		return shardInstance;
	}
}
