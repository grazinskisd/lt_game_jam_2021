using System.Collections;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{
    public PlayerTrigger trigger;

    protected virtual void Awake()
    {
        trigger.onPlayerEntered.AddListener(OnPlayerEntered);
    }

    protected abstract void OnPlayerEntered(PlayerController player);

    protected IEnumerator DestroyDelayed(float delay, GameObject go)
    {
        yield return new WaitForSeconds(delay);
        Destroy(go);
    }
}
