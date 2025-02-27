using UnityEngine;

public class MD_ColliderManager : EnemyColliderManager
{       
   
   
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);        
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);  
    }
}
