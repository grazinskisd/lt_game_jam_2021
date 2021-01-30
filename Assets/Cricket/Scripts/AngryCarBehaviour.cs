using UnityEngine;
using DG.Tweening;

public class AngryCarBehaviour : AttackBehaviour
{
    public float driveSpeed;
    public Transform garage;

    protected override void OnPlayerEntered(PlayerController player)
    {
        base.OnPlayerEntered(player);

        var midPoint = player.transform.position;
        midPoint.y = transform.position.y;

        var endPoint = garage.transform.position;
        endPoint.y = transform.position.y;

        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(midPoint, 1 / driveSpeed));
        sequence.Append(transform.DOMove(endPoint, 1 / driveSpeed).SetDelay(1))
            .OnComplete(() => 
            {
                Destroy(gameObject);
            });
        sequence.SetEase(Ease.InQuad);
    }
}
