using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField]
    float RotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, new Vector3(0, 1, 0), RotationSpeed);
    }
}
