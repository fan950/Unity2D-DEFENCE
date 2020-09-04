using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTowerBounds : MonoBehaviour
{
    public Bounds bs;

    void OnEnable()
    {
        bs.center = transform.position;
    }

}
