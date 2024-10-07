using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bellboy : MonoBehaviour
{
    public bool isPlayerIn;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerIn = false;
        }
    }
}
