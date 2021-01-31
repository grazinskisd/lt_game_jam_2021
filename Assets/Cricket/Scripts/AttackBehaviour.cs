using System;
using System.Collections;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{
    private const float REPEAT_DELAY = 40;

    public AudioClip triggerEffect;
    public PlayerTrigger trigger;

    private AudioManager _audioManager;
    private float _timeSinceLastTrigger;
    private bool _wasTriggered;

    protected virtual void Awake()
    {
        trigger.onPlayerEntered.AddListener(CheckOnPlayerEntered);
        _audioManager = FindObjectOfType<AudioManager>();
        _timeSinceLastTrigger = 0;
    }

    private void CheckOnPlayerEntered(PlayerController player)
    {
        if (!_wasTriggered)
        {
            _timeSinceLastTrigger = 0;
            _wasTriggered = true;
            OnPlayerEntered(player);
        }
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

    protected IEnumerator DisableDelayed(float delay, GameObject go)
    {
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
    }

    private void Update()
    {
        if (_wasTriggered)
        {
            _timeSinceLastTrigger += Time.deltaTime;
            if(_timeSinceLastTrigger >= REPEAT_DELAY)
            {
                _wasTriggered = false;
                ResetState();
            }
        }
    }

    protected abstract void ResetState();
}
