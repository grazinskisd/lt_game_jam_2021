using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AngryPersonBehaviour : AttackBehaviour
{
    public float travelDuration;

    protected override void OnPlayerEntered(PlayerController player)
    {
        transform.DOMove(player.transform.position, travelDuration)
            .OnComplete(() =>
            {
                player.DoDamage();
                StartCoroutine(DestroyDelayed(1, gameObject));
            });
    }
}
