using UnityEngine;
using DG.Tweening;

public class CarManager : MonoBehaviour
{
    public Transform carPathParent;
    public GameObject carPrototype;
    public float driveDuration;
    public float spawnDelayMin;
    public float spawnDelayMax;
    public float maxCarOffset;

    private Vector3[] _waypoints;

    private float _timeSinceSpawn;
    private float _timeToNextSpawn;

    private void Awake()
    {
        _waypoints = new Vector3[carPathParent.childCount];
        for (int i = 0; i < _waypoints.Length; i++)
        {
            _waypoints[i] = carPathParent.GetChild(i).position;
        }
    }

    private void Start()
    {
        ResetSpawnDelay();
        CreateCarOnTheRoad();
    }

    private void CreateCarOnTheRoad()
    {
        Transform carParent = GetCarParent();
        AddNewCarToParent(carParent);
        AnimateCar(carParent);
    }

    private void AnimateCar(Transform car)
    {
        car.transform.position = _waypoints[0];
        car.transform.DOPath(_waypoints, driveDuration)
            .SetEase(Ease.Linear)
            .SetLookAt(0.005f, false)
            .OnComplete(() =>
            {
                Destroy(car.gameObject);
            });
    }

    private void AddNewCarToParent(Transform carParent)
    {
        var newCar = Instantiate(carPrototype);
        newCar.transform.SetParent(carParent);
        newCar.transform.localPosition = Vector3.zero + Vector3.left * Random.Range(-1f, 1f) * maxCarOffset;
    }

    private Transform GetCarParent()
    {
        var carParent = new GameObject("Car parent").transform;
        carParent.SetParent(transform);
        return carParent;
    }

    private void ResetSpawnDelay()
    {
        _timeSinceSpawn = 0;
        _timeToNextSpawn = Random.Range(spawnDelayMin, spawnDelayMax);
    }

    private void Update()
    {
        _timeSinceSpawn += Time.deltaTime;
        if(_timeSinceSpawn >= _timeToNextSpawn)
        {
            ResetSpawnDelay();
            CreateCarOnTheRoad();
        }
    }
}
