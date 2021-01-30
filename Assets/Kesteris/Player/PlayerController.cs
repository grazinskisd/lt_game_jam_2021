using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private const string QUESTION_MARK = "?";
    [SerializeField]
    float MoveSpeed;
    [SerializeField]
    float StunDuration;
    GameObject StunEffect;
    float LastStunTime;
    KeyView TextW;
    KeyView TextA;
    KeyView TextS;
    KeyView TextD;
    CharacterController Controller;
    Animator Animator;
    AudioSource AudioSource;

    bool CanHandOverPost;
    bool CanPickUpPost;
    bool IsAbleToMove = true;
    int TriggeredHouseNo = 0;
    PostController PostCtrl;
    KeyCode NewKey = 0;
    int LastModifiedKeyIndex = 0;

    KeyCode[] Keys = {
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P,
        KeyCode.A, KeyCode.S, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L,
        KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M
    };
    List<KeyCode> TakenKeys = new List<KeyCode>(new KeyCode[] {KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D});

    [SerializeField]
    List<AudioClip> AudioClips = new List<AudioClip>();

    private void Awake()
    {
        TextW = GameObject.Find("TextW").GetComponent<KeyView>();
        TextA = GameObject.Find("TextA").GetComponent<KeyView>();
        TextS = GameObject.Find("TextS").GetComponent<KeyView>();
        TextD = GameObject.Find("TextD").GetComponent<KeyView>();

        StunEffect = GameObject.Find("StunEffect");
        StunEffect.SetActive(false);
    }

    void Start()
    {
        Controller = transform.GetComponent<CharacterController>();
        PostCtrl = transform.GetComponent<PostController>();
        Animator = transform.GetComponent<Animator>();
        AudioSource = transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (IsAbleToMove)
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
        }
        else
        {
            if (Time.time - LastStunTime > StunDuration)
            {
                IsAbleToMove = true;
            }
        }


        if (CanHandOverPost)
        {
            HandOverPost();
        }
        if (CanPickUpPost)
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
    IEnumerator Stunned()
    {
        GetComponent<AudioPlayer>().Play("stun");
        StunEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        StunEffect.SetActive(false);
    }
    public void DoDamage()
    {
        StartCoroutine("Stunned");
        LooseControl();
    }

    public void Stun()
    {
        IsAbleToMove = false;
        LastStunTime = Time.time;
        
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
        if (replacingKeyIndex == LastModifiedKeyIndex)
        {
            LooseControl();
            return;
        }

        LastModifiedKeyIndex = replacingKeyIndex;

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
        UpdateKeyView(TextW, 0);
        UpdateKeyView(TextA, 1);
        UpdateKeyView(TextS, 2);
        UpdateKeyView(TextD, 3);
    }

    private void UpdateKeyView(KeyView view, int index)
    {
        view.Text = TakenKeys[index] != NewKey ? TakenKeys[index].ToString() : QUESTION_MARK;
        view.Color = view.Text.Equals(QUESTION_MARK) ? Color.red : Color.black;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("house"))
        {
            var postBox = other.GetComponent<PostBox>();
            if (postBox != null)
            {
                CanHandOverPost = true;
                TriggeredHouseNo = postBox.number;
            }
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
