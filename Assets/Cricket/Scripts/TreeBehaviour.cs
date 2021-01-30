using UnityEngine;
using DG.Tweening;

public class TreeBehaviour : AttackBehaviour
{
    public GameObject apple;
    public float fallDuration;
    public float stayDuration;

    private bool _hasTheAppleFallen;

    protected override void OnPlayerEntered(PlayerController player)
    {
        if (!_hasTheAppleFallen)
        {
            base.OnPlayerEntered(player);
            _hasTheAppleFallen = true;

            var startPosition = player.transform.position;
            startPosition.y = apple.transform.position.y;
            apple.transform.position = startPosition;

            var endPosition = startPosition;
            endPosition.y = 0;

            apple.transform.DOMove(endPosition, fallDuration)
                .OnComplete(() =>
                {
                    player.DoDamage();
                    StartCoroutine(DestroyDelayed(stayDuration, apple.gameObject));
                });
        }
    }
}
