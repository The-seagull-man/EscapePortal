using Unity.VisualScripting;
using UnityEngine;

public class ObjectRespawner : MonoBehaviour
{
    public GameObject template;
    public GameObject respawnPosition;

	Vector3 respawnPos;
    GameObject obj;

	void Start()
    {
        respawnPos = respawnPosition.transform.position;
        Destroy(respawnPosition);
        obj = Instantiate(template);
    }

	private void FixedUpdate() {
		if (obj.IsDestroyed()) {
            obj = Instantiate(template);
        }
	}
}
