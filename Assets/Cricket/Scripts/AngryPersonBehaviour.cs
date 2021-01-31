using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AngryPersonBehaviour : AttackBehaviour
{
    public float travelDuration;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }


    protected override void OnPlayerEntered(PlayerController player)
    {
        base.OnPlayerEntered(player);
        transform.DOMove(player.transform.position, travelDuration)
            .OnComplete(() =>
            {
                player.DoDamage();
                StartCoroutine(DisableDelayed(1, gameObject));
            });
    }

    protected override void ResetState()
    {
        gameObject.SetActive(true);
        transform.position = _startPosition;
    }
}
