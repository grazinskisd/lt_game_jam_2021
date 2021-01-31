using UnityEngine;
using DG.Tweening;

public class AngryCarBehaviour : AttackBehaviour
{
    public float driveSpeed;
    public Transform garage;
    private Vector3 _startPosition;

    protected override void Awake()
    {
        base.Awake();
        _startPosition = transform.position;
    }

    protected override void OnPlayerEntered(PlayerController player)
    {
        base.OnPlayerEntered(player);

        var midPoint = player.transform.position;
        midPoint.y = transform.position.y;

        var endPoint = garage.transform.position;
        endPoint.y = transform.position.y;

        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(midPoint, 1 / driveSpeed).OnComplete(() => {
            player.DoDamage();
        }));
        sequence.Append(transform.DOMove(endPoint, 2* (1 / driveSpeed)).SetDelay(1));
        sequence.SetEase(Ease.InQuad);
    }

    protected override void ResetState()
    {
        transform.position = _startPosition;
    }
}
