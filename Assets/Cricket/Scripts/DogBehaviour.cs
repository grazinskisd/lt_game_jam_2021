using UnityEngine;
using DG.Tweening;

public class DogBehaviour : MonoBehaviour
{
    public PlayerTrigger trigger;
    public float jumpDuration;
    public float jumpPower;
    public float moveSpeed;
    public Transform dogHouse;

    private Vector3 _startPosition;
    private Sequence _sequence;

    private void Awake()
    {
        _startPosition = transform.position;
        trigger.onPlayerEntered.AddListener(ReactToPlayer);
    }

    public void ReactToPlayer(GameObject player)
    {
        Debug.Log("Bark batk motherfucker!");
        var endValue = player.transform.position;
        endValue.y = _startPosition.y;

        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOJump(endValue, jumpPower, 1, jumpDuration));
        _sequence.Append(transform.DOMove(dogHouse.position, 1 / moveSpeed).SetDelay(1));
        _sequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
