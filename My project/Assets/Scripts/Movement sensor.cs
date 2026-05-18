using UnityEngine;

public class Movementsensor : MonoBehaviour
{
    [SerializeField]
    LayerMask mask;
    [SerializeField]
    float length = 5;
    [SerializeField]
    AudioSource Voiceline;
    [SerializeField]
    bool played;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        played = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(played)
        {
            return;
        }
        RaycastHit hit;

        Debug.DrawLine(transform.position, transform.forward*length+transform.position, Color.red);
        if(Physics.Raycast(transform.position, transform.forward, out hit, length, mask))
        {
            if (hit.collider.CompareTag("Player") )
            {
                Voiceline.gameObject.SetActive(true);
                played = true;
                
            }
        }
    }
}
