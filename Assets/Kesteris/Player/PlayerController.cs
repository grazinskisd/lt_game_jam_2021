using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float MoveSpeed;
    Text TextW;
    Text TextA;
    Text TextS;
    Text TextD;
    CharacterController Controller;
    Animator Animator;

    bool CanHandOverPost;
    bool CanPickUpPost;
    int TriggeredHouseNo = 0;
    PostController PostCtrl;
    KeyCode NewKey = 0;

    KeyCode[] Keys = {
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P,
        KeyCode.A, KeyCode.S, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L,
        KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M
    };
    List<KeyCode> TakenKeys = new List<KeyCode>(new KeyCode[] {KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D});

    private void Awake()
    {
        TextW = GameObject.Find("TextW").GetComponent<Text>();
        TextA = GameObject.Find("TextA").GetComponent<Text>();
        TextS = GameObject.Find("TextS").GetComponent<Text>();
        TextD = GameObject.Find("TextD").GetComponent<Text>();
    }

    void Start()
    {
        Controller = transform.GetComponent<CharacterController>();
        PostCtrl = transform.GetComponent<PostController>();
        Animator = transform.GetComponent<Animator>();
    }

    void Update()
    {
        var vertical = Input.GetKey(TakenKeys[0]) ? 1 : Input.GetKey(TakenKeys[2]) ? -1 : 0;
        var horizontal = Input.GetKey(TakenKeys[3]) ? 1 : Input.GetKey(TakenKeys[1]) ? -1 : 0;
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

        if (vertical != 0 || horizontal != 0)
        {
            Animator.SetBool("IsRunning", true);
            transform.eulerAngles = new Vector3(0, rotation, 0);
            Vector3 movement = transform.forward * MoveSpeed / 10;
            Controller.Move(movement);
        }
        else
        {
            Animator.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.E) && CanHandOverPost)
        {
            HandOverPost();
        }
        if (Input.GetKeyDown(KeyCode.E) && CanPickUpPost)
        {
            PickUpPost();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            PickUpPost();
        }

        if (NewKey != 0)
        {
            if (Input.GetKeyDown(NewKey))
            {
                NewKey = 0;
            }
        }

        RefreshUI();
    }

    public void DoDamage()
    {
        Debug.Log(">> Doing damage to player");
        LooseControl();
    }

    private void HandOverPost()
    {
        PostCtrl.HandOverPost(TriggeredHouseNo);
    }

    private void PickUpPost()
    {
        PostCtrl.GetPost();
    }

    private void LooseControl()
    {
        var newKeyIndex = Random.Range(0, Keys.Length - 1);
        var replacingKeyIndex = Random.Range(0, TakenKeys.Count - 1);
        
        NewKey = Keys[newKeyIndex];
        var replacingKey = TakenKeys[replacingKeyIndex];

        if (NewKey != replacingKey && !TakenKeys.Contains(NewKey))
        {
            TakenKeys[replacingKeyIndex] = NewKey;
        }
        else
        {
            LooseControl();
        }
    }

    private void RefreshUI()
    {
        var questionMark = "?";
        TextW.text = TakenKeys[0] != NewKey ? TakenKeys[0].ToString() : questionMark;
        TextA.text = TakenKeys[1] != NewKey ? TakenKeys[1].ToString() : questionMark;
        TextS.text = TakenKeys[2] != NewKey ? TakenKeys[2].ToString() : questionMark;
        TextD.text = TakenKeys[3] != NewKey ? TakenKeys[3].ToString() : questionMark;

        TextW.color = TextW.text.Equals(questionMark) ? Color.red : Color.black;
        TextA.color = TextA.text.Equals(questionMark) ? Color.red : Color.black;
        TextS.color = TextS.text.Equals(questionMark) ? Color.red : Color.black;
        TextD.color = TextD.text.Equals(questionMark) ? Color.red : Color.black;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("house"))
        {
            CanHandOverPost = true;
            TriggeredHouseNo = Int16.Parse(other.gameObject.name);
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
