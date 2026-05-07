using UnityEngine;

public class PortalActivator : MonoBehaviour
{
    // Hvis man skal lave en portal som starter deaktiveret og derefter bliver aktiveret, skal man slå Mesh Renderer fra og Collideren fra samt deaktivere particle-gameobjectet.På portal scriptet skal portalIsActivated være false Dette script skal på objektet der skal bruges til at åbne portalen.


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("portal"))
        {
            collider.GetComponentInParent<MeshRenderer>().enabled = true; // man kan se portalen
            collider.GetComponentInParent<SphereCollider>().enabled = true; // man kan collide med portalen
            collider.GetComponentInParent<Portal>().portalIsActivated = true; // man kan gå igennem portalen
            collider.GetComponentInParent<Portal>().gameObject.GetComponentInChildren<ParticleSystem>(true).gameObject.SetActive(true); // der er partikler omkring portalen
            collider.enabled = false; // man kan ikke længere trigger det her script
        }
    }
}   
