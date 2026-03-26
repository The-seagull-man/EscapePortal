using UnityEngine;

public class cameraHolder : MonoBehaviour
{
    public Transform cameraPosition;
    private void Update()
    {
        transform.rotation = cameraPosition.rotation;
    }
}
