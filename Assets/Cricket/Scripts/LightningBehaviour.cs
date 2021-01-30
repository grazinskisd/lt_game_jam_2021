using UnityEngine;
using DG.Tweening;
using System.Collections;

public class LightningBehaviour : AttackBehaviour
{
    public float startHeight;
    public float animationDuration;
    public float destroyDelay;

    void Start()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator DestroyDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    protected override void OnPlayerEntered(PlayerController player)
    {
        gameObject.SetActive(true);
        var startPosition = player.transform.position + transform.up * startHeight;
        transform.position = startPosition;

        transform.DOMove(player.transform.position, animationDuration)
            .SetEase(Ease.InQuart)
            .OnComplete(() =>
            {
                player.DoDamage();
                StartCoroutine(DestroyDelayed(destroyDelay));
            });
    }
}
