using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    CharacterController Controller;
    [SerializeField]
    float MoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        float rotation = 0;
        if (horizontal > 0 && vertical > 0)
            rotation = 45;
        else if (horizontal > 0 && vertical < 0)
            rotation = 135;
        else if (horizontal < 0 && vertical > 0)
            rotation = -45;
        else if (horizontal < 0 && vertical < 0)
            rotation = -135;
        else if (vertical > 0)
            rotation = 0;
        else if (vertical < 0)
            rotation = 180;
        else if (horizontal > 0)
            rotation = 90;
        else if (horizontal < 0)
            rotation = -90;

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            transform.eulerAngles = new Vector3(0, rotation, 0);
            Vector3 movement = transform.forward * MoveSpeed / 10;
            Controller.Move(movement);
        }
    }
}
