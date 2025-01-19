using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class selfDestructionDoor : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name=="Key")
        {
            InitParticle();
            Destroy(gameObject, 0.01f);
            Destroy(collision.transform.gameObject, 0.01f);
        }
    }

    private void InitParticle()
    {
        GameObject newParticle = Instantiate(ResourceManager.Instance.destroyParticle, this.transform);
        newParticle.transform.localPosition = Vector3.zero;
        newParticle.transform.SetParent(null, true);
    }
    
}