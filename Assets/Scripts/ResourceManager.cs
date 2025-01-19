using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    public GameObject destroyParticle;
    public AudioClip ExplosionSound;
   
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
}
