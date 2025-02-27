using UnityEngine;

public class MD_ColliderManager : EnemyColliderManager
{   
    
    protected override void Start()
    {
        base.Start();
    }
   
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);        
    }   
}
