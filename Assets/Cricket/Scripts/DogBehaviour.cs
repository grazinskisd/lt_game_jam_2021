using UnityEngine;
using DG.Tweening;

public class DogBehaviour : AttackBehaviour
{
    public float jumpDuration;
    public float jumpPower;
    public float moveSpeed;
    public Transform dogHouse;

    private Vector3 _startPosition;
    private Sequence _sequence;

    private void Start()
    {
        _startPosition = transform.position;
    }

    protected override void OnPlayerEntered(PlayerController player)
    {
        Debug.Log("Bark batk motherfucker!");
        var endValue = player.transform.position;
        endValue.y = _startPosition.y;

        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOJump(endValue, jumpPower, 1, jumpDuration).OnComplete(player.DoDamage));
        _sequence.Append(transform.DOMove(dogHouse.position, 1 / moveSpeed).SetDelay(1));
        _sequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
