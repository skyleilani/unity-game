using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OnCollection : MonoBehaviour
{
    public GameObject collectible;

    // initial spawn and respawn position
    public Vector3 pos_intl;
    public Vector3 pos_rspwn; 


    private void Start()
    {
        collectible = gameObject;
        pos_intl = collectible.transform.position; 
    }

    // rather than destroying a ton of objects and recreating them 
    // or even going thru activating and deactivating 
    // there will be a set of 3-5 collectibles 
    // and on collision they will just relocate to a different part of the map and we will be 
    // recollecting these same 5 collectibles 
    private void OnTriggerEnter(Collider other)
    {
        // would like to have a set of 5 collectibles that I reuse and just re-activate at different points 

        // Destroy is very costly why would you use this again?
        // Destroy(gameObject); 


       // collectible.SetActive(false);
        Debug.Log("initial position: " + pos_intl);

        Debug.Log("pos_rspwn before reset: " + pos_rspwn);

        pos_rspwn.Set(Random.Range(pos_intl.x, pos_intl.x * pos_intl.x ),pos_intl.y, Random.Range(pos_intl.z, pos_intl.z * pos_intl.z));
        Debug.Log("pos_rspwn after reset: " + pos_rspwn);

        transform.position = pos_rspwn;
    }
}
