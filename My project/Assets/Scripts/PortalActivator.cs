using UnityEngine;

public class PortalActivator : MonoBehaviour
{
    // Hvis man skal lave en portal som starter deaktiveret og derefter bliver aktiveret, skal man slå Mesh Renderer fra og Collideren fra. Dette script skal på objektet der skal bruges til at åbne portalen.


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("portal"))
        {
            collider.GetComponentInParent<MeshRenderer>().enabled = true;
            collider.GetComponentInParent<SphereCollider>().enabled = true;
        }
    }
}   
