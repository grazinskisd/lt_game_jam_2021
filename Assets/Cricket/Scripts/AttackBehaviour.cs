using System.Collections;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{
    public PlayerTrigger trigger;

    protected virtual void Awake()
    {
        trigger.onPlayerEntered.AddListener(OnPlayerEntered);
    }

    protected virtual void OnPlayerEntered(PlayerController player)
    {
        player.Stun();
    }

    protected IEnumerator DestroyDelayed(float delay, GameObject go)
    {
        yield return new WaitForSeconds(delay);
        Destroy(go);
    }
}
