using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardZone : MonoBehaviour
{
    public int Damage;
    public float DamageRate;

    private List<IDamagable> thingsToDamage = new List<IDamagable>();

     private void Start() 
     {
        StartCoroutine(DealDamage());
     }

    IEnumerator DealDamage()
    {
        while (true)
        {
            for (int i = 0; i < thingsToDamage.Count; i++)
            {
                thingsToDamage[i].TakePhysicalDamage(Damage);

            }
            yield return new WaitForSeconds(DamageRate);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.GetComponent<IDamagable>() != null)
        {
            thingsToDamage.Add(other.gameObject.GetComponent<IDamagable>());
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.GetComponent<IDamagable>() != null)
        {
            thingsToDamage.Remove(other.gameObject.GetComponent<IDamagable>());
        }
    }
   
}