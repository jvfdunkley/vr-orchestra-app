using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    public float lifetime = 0.2f;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
