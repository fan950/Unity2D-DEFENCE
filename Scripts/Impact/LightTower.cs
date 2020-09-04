using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTower : MonoBehaviour
{
    public Animator ani;

    private void OnDisable()
    {
        ani.SetTrigger("LightStart");        
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
