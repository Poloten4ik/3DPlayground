using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class Torch : MonoBehaviour
{
    
    [SerializeField] ParticleSystem fireEffect;
    // Start is called before the first frame update
    void Start()
    {
        Damageable damageable = GetComponent<Damageable>();
        damageable.OnRecieveDamage += ActivateTorct;
    }

   private void ActivateTorct(int damage)
    {
        print("active");
      

        if (fireEffect.isPlaying)
        {
            fireEffect.Stop();
        }
        else
        {
            fireEffect.Play();
        }
           
       // GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
