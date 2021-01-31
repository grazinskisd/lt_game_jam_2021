using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerTrigger : MonoBehaviour
{
    public MyPlayerControllerEvent onPlayerEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().enabled)
                onPlayerEntered.Invoke(other.GetComponent<PlayerController>());
        }
    }
}