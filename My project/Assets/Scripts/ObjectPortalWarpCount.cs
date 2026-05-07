using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEditor.Experimental;
using Unity.VisualScripting;

public class ObjectPortalWarpCount : MonoBehaviour
{
    public int warpLimt;
    [SerializeField]
    private int max = 5;
    public float startSize;
    public float MaxSize { get { return startSize * max; } }
    public float MinSize { get { return startSize / max; } }

    private void Awake()
    {
        startSize = transform.localScale.x;
    }

    
}
