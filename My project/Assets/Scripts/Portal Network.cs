using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PortalNetwork : MonoBehaviour
{
    Portal[] portals;
    Transform portalNetwork;
    void Start()
    {
        portalNetwork = gameObject.GetComponent<Transform>();
        for (int i = 0; i < portalNetwork.childCount; i++)
        {
            Transform curent = gameObject.GetComponentInChildren<Transform>().GetChild(i);
            portals[i] = curent.gameObject.GetComponent<Portal>();
            portals[i].exitPortal;
        }



    }

    
}
