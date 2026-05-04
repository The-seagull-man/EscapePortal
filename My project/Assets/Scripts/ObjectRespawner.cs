using Unity.VisualScripting;
using UnityEngine;

public class ObjectRespawner : MonoBehaviour
{
    public GameObject template;
    public GameObject respawnPosition;
    public int delay;

	Vector3 respawnPos;
    GameObject obj;
    int delayCounter;

	void Start()
    {
        respawnPos = respawnPosition.transform.position;
        Destroy(respawnPosition);
        obj = Instantiate(template);
    }

	private void FixedUpdate() {
        if (delayCounter > 0) {
            if (delayCounter == delay) {
				delayCounter = 0;
				obj = Instantiate(template);
            } else {
                delayCounter++;
            }
        } else if (obj.IsDestroyed()) {
            if (delay == 0) {
				obj = Instantiate(template);
			} else {
                delayCounter++;
            }
		}
	}
}
