using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public MyGameObjectEvent onPlayerEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerEntered.Invoke(other.gameObject);
        }
    }
}