using UnityEngine;
using DG.Tweening;
using System.Collections;

public class TreeBehaviour : AttackBehaviour
{
    public GameObject apple;
    public float fallDuration;
    public float stayDuration;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = apple.transform.position;
    }

    protected override void OnPlayerEntered(PlayerController player)
    {
        base.OnPlayerEntered(player);

        var startPosition = player.transform.position;
        startPosition.y = apple.transform.position.y;
        apple.transform.position = startPosition;

        var endPosition = startPosition;
        endPosition.y = 0;

        apple.transform.DOMove(endPosition, fallDuration)
            .OnComplete(() =>
            {
                player.DoDamage();
                StartCoroutine(DelayedMove());
            });
    }

    private IEnumerator DelayedMove()
    {
        yield return new WaitForSeconds(stayDuration);
        apple.transform.position = _startPosition;
    }

    protected override void ResetState()
    {
    }
}
