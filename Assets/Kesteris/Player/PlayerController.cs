using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float MoveSpeed;

    CharacterController Controller;
    CapsuleCollider Trigger;
    

    bool CanHandOverPost;
    bool CanPickUpPost;
    GameObject Office;
    PostController PostCtrl;
    void Start()
    {
        Trigger = transform.GetComponent<CapsuleCollider>();
        Controller = transform.GetComponent<CharacterController>();
        PostCtrl = transform.GetComponent<PostController>();
    }

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

        if (Input.GetKeyDown(KeyCode.E) && CanHandOverPost)
        {
            HandOverPost();
        }
        if (Input.GetKeyDown(KeyCode.E) && CanPickUpPost)
        {
            Debug.Log(CanPickUpPost);
            PickUpPost();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            PickUpPost();
        }
    }

    public void DoDamage()
    {
        Debug.Log(">> Doing damage to player");
    }

    private void HandOverPost()
    {
        PostCtrl.HandOverPost();
    }

    private void PickUpPost()
    {
        PostCtrl.GetPost();
    }

    private void RefreshUI()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("house"))
        {
            CanHandOverPost = true;
        }
        if (other.CompareTag("office"))
        {
            CanPickUpPost = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("house"))
        {
            CanHandOverPost = false;
        }
        if (other.CompareTag("office"))
        {
            CanPickUpPost = false;
        }
    }
}
