using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float camerasens;
    float CameraYaw = 0;
    float CameraPitch = 0;

    Vector2 mouseDelta;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        CameraYaw += Input.mousePositionDelta.x * camerasens;
        CameraPitch -= Input.mousePositionDelta.y * camerasens;

        if (CameraYaw > 360) CameraYaw -= 360;
        if (CameraYaw < 0) CameraYaw += 360;
        if (CameraPitch > 90) CameraPitch = 90;
        if (CameraPitch < -90) CameraPitch = -90;

        transform.localRotation = Quaternion.Euler(CameraPitch, 0, 0);
        Playermovementscript.Instance.gameObject.transform.localRotation = Quaternion.Euler(0, CameraYaw, 0);

    }
}
