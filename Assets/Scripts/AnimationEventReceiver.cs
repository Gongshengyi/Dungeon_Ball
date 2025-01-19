using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    public bool isScanning = false;
    [SerializeField] LightAttack lightAttack;
    void Update()
    {
        if (isScanning)
        {
            lightAttack.HandleSwing();
            isScanning = false;
        }
    }
    // Start is called before the first frame update
    public void AttackStarted()
    {
        isScanning = true;
    }

    public void AttackEnded()
    {
        isScanning = true;
    }
}
