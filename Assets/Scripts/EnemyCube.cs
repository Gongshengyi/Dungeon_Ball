using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCube : MonoBehaviour
{
    public UnityEvent unityEvent;
    // Start is called before the first frame update
    private void OnDestroy()
    {
        Debug.Log("ondes");
        unityEvent.Invoke();    
    }
}
