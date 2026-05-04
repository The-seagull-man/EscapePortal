using UnityEngine;

public class Shatterable : MonoBehaviour
{
    public GameObject shard;
    public int shardCount = 4;
    public float shardScale = .1f;
    public float shardScaleFactorVariance = .25f;
    public float shardForceVariationMin = .16f;
	public float shardForceVariationMax = .32f;
    public float shardDisplacementMin = .02f;
	public float shardDisplacementMax = .1f;
    public bool obeyShardLimit = true;

	public static int globalShardLimit = 120;
	public static int globalShardCount;

    public virtual void Shatter(Vector3 reflect) {
        for (int i = 0; i < shardCount; i++) {
            if (!obeyShardLimit || globalShardCount < globalShardLimit) {
				GenerateShard(reflect);
			} else {
                break;
            }
        }
        Destroy(gameObject);
    }

    public virtual GameObject GenerateShard(Vector3 reflect) {
        globalShardCount++;
        GameObject shardInstance = Instantiate(shard);
        shardInstance.transform.localScale = Vector3.one*shardScale*(1 + Random.Range(-shardScaleFactorVariance, shardScaleFactorVariance));
        shardInstance.transform.localRotation = Random.rotation;
        Vector3 direction = Random.onUnitSphere;
		shardInstance.GetComponent<Rigidbody>().linearVelocity =
                reflect + direction*Random.Range(shardForceVariationMin, shardForceVariationMax);
        shardInstance.transform.position = transform.position + direction*Random.Range(shardDisplacementMin, shardDisplacementMax);
		return shardInstance;
	}

    public Vector3 CalculateReflectVector(Collision collision) {
		return Vector3.Reflect(collision.relativeVelocity, collision.GetContact(0).normal);
    }
}
