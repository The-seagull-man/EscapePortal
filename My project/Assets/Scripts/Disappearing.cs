using UnityEngine;

public class Disappearing : MonoBehaviour
{
    public float shrinkStartTime;
	public float disappearTime;
	public bool isShard;

    Vector3 scale;
	float time;

	public void Start() {
		scale = transform.localScale;
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
		if (time >= disappearTime) {
			if (isShard) {
				Shatterable.globalShardCount--;
			}
			Destroy(gameObject);
		} else if (time > shrinkStartTime) {
			transform.localScale = scale*(1 - (time - shrinkStartTime)/(disappearTime - shrinkStartTime));
		}
    }
}
