using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayingScript : MonoBehaviour
{
    public ParticleSystem SprayEffect;
    
    public void Anim_Spray()
    {
        SprayEffect.Play();
       
    }
   
}
