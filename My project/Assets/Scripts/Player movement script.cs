using UnityEngine;

public class Playermovementscript : MonoBehaviour
{
    Vector3 right;
    Vector3 forward;
    Vector3 vel;

    public float Speed;

    Rigidbody rb;
    public static Playermovementscript Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        right = transform.right;
        forward = transform.forward;
        vel = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            vel += forward * Speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vel += right * Speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vel -= forward * Speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vel -= right * Speed;
        }
        rb.linearVelocity = vel;
    }
}
