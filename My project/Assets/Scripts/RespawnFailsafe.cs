using UnityEngine;

public class RespawnFailsafe : MonoBehaviour
{
    public float lowestY;

    Vector3 respawnPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        respawnPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < lowestY) {
            transform.position = respawnPosition;
        }
    }
}
