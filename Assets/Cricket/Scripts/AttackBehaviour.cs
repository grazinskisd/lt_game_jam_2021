using System.Collections;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{
    public AudioClip triggerEffect;
    public PlayerTrigger trigger;

    private AudioManager _audioManager;

    protected virtual void Awake()
    {
        trigger.onPlayerEntered.AddListener(OnPlayerEntered);
        _audioManager = FindObjectOfType<AudioManager>();
    }

    protected virtual void OnPlayerEntered(PlayerController player)
    {
        var audioSource = player.GetComponent<AudioSource>();
        if(audioSource != null && triggerEffect != null)
        {
            audioSource.PlayOneShot(triggerEffect, _audioManager.effectVolume);
        }
        player.Stun();
    }

    protected IEnumerator DestroyDelayed(float delay, GameObject go)
    {
        yield return new WaitForSeconds(delay);
        Destroy(go);
    }
}
