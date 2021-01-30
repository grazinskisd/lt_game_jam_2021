using UnityEngine;
using DG.Tweening;
using System.Collections;

public class LightningBehaviour : MonoBehaviour
{
    public PlayerTrigger trigger;
    public float startHeight;
    public float animationDuration;
    public float destroyDelay;

    void Start()
    {
        trigger.onPlayerEntered.AddListener(StrikeLightning);
        gameObject.SetActive(false);
    }

    private void StrikeLightning(GameObject player)
    {
        gameObject.SetActive(true);
        var startPosition = player.transform.position + transform.up * startHeight;
        transform.position = startPosition;

        transform.DOMove(player.transform.position, animationDuration)
            .SetEase(Ease.InQuart)
            .OnComplete(() =>
            {
                StartCoroutine(DestroyDelayed(destroyDelay));
            });
    }

    private IEnumerator DestroyDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
