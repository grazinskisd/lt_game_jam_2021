using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : AttackBehaviour
{
    protected override void OnPlayerEntered(PlayerController player)
    {
        base.OnPlayerEntered(player);
        player.DoDamage();
    }
}
