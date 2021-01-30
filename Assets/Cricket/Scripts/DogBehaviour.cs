using UnityEngine;
using DG.Tweening;

public class DogBehaviour : MonoBehaviour
{
    public float jumpDuration;
    public float jumpPower;
    public float moveSpeed;
    public Transform dogHouse;

    private Vector3 _startPosition;
    private Sequence _sequence;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    public void ReactToPlayer(GameObject player)
    {
        Debug.Log("Bark batk motherfucker!");
        var endValue = player.transform.position;
        endValue.y = _startPosition.y;

        if(_sequence != null)
        {
            _sequence.Kill();
            _sequence = null;
        }

        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOJump(endValue, jumpPower, 1, jumpDuration));
        _sequence.Append(transform.DOMove(dogHouse.position, 1 / moveSpeed).SetDelay(1));
    }
}
