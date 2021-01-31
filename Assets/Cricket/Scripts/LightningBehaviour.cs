using UnityEngine;
using DG.Tweening;
using System.Collections;

public class LightningBehaviour : AttackBehaviour
{
    public float startHeight;
    public float animationDuration;
    public float destroyDelay;

    private Vector3 _startPosition;

    void Start()
    {
        gameObject.SetActive(false);
        _startPosition = transform.position;
    }

    protected override void OnPlayerEntered(PlayerController player)
    {
        base.OnPlayerEntered(player);

        gameObject.SetActive(true);
        var startPosition = player.transform.position + transform.up * startHeight;
        transform.position = startPosition;

        transform.DOMove(player.transform.position, animationDuration)
            .SetEase(Ease.InQuart)
            .OnComplete(() =>
            {
                player.DoDamage();
                StartCoroutine(DelayedMove());
            });
    }

    private IEnumerator DelayedMove()
    {
        yield return new WaitForSeconds(destroyDelay);
        transform.position = _startPosition;
    }

    protected override void ResetState()
    {
        transform.position = _startPosition;
    }
}
