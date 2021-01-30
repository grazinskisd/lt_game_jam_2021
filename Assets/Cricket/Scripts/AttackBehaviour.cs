using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{
    public PlayerTrigger trigger;

    protected virtual void Awake()
    {
        trigger.onPlayerEntered.AddListener(OnPlayerEntered);
    }

    protected abstract void OnPlayerEntered(PlayerController player);


}
