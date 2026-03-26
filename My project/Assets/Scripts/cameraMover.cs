using UnityEngine;

public class cameraMover : MonoBehaviour
{
    public float sensX;
    public float sensY;


    float xRotation;
    float yRotation;

    public Transform cameraYTrans;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * sensX;
        
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        
    }
    private void LateUpdate()
    {
        // rotate camera
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        cameraYTrans.localRotation = Quaternion.Euler(0, yRotation, 0);

    }
}
