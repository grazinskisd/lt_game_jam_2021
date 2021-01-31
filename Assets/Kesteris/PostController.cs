using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public delegate void PostControllerEvent(int number);
public delegate void PostDeliveredEvent();

public class PostController : MonoBehaviour
{
    public event PostControllerEvent OnNextPostBoxDecided;
    public event PostControllerEvent OnPostHanded;
    public event PostDeliveredEvent OnAllPostDelivered;

    [SerializeField]
    GameObject UITimer;
    Text TextTimer;
    [SerializeField]
    GameObject UIPostCount;
    Text TextPostCount;
    [SerializeField]
    GameObject UIHouseNo;
    [SerializeField]
    public float timePerPost;
    Text TextHouseNo;
    UIManager UIManager;
    float Timer;
    bool IsTimerEnabled;
    int CurrentPostCount = 0;
    int GotPostCount = 0;
    int TargetHouseNo = 0;
    int Level = 1;
    int Collected = 0;
    private void Awake()
    {
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
    void Start()
    {
        TextTimer = UITimer.GetComponent<Text>();
        TextPostCount = UIPostCount.GetComponent<Text>();
        TextHouseNo = UIHouseNo.GetComponent<Text>();
        IsTimerEnabled = false;
        OnAllPostDelivered?.Invoke();
    }

    void Update()
    {
        if (Timer > 0 && IsTimerEnabled)
        {
            Timer -= Time.deltaTime;
        }
        if (Timer <= 0 && IsTimerEnabled)
        {
            GameOver();
        }
        RefreshUI();
    }

    public bool GetPost()
    {
        if (CurrentPostCount == 0)
        {
            CurrentPostCount = Level;
            GotPostCount = CurrentPostCount;
            GetNewTarget();
            Timer += timePerPost * CurrentPostCount;
            StartTimer();
            GetComponent<AudioPlayer>().Play("paper2");
            return true;
        }
        return false;
    }

    public bool HandOverPost(int houseNo)
    {
        if (houseNo != TargetHouseNo)
        {
            return false;
        }
        OnPostHanded?.Invoke(TargetHouseNo);
        if (CurrentPostCount > 0)
        {
            CurrentPostCount -= 1;
            Collected++;
            GetNewTarget();
            GetComponent<AudioPlayer>().Play("throw");
            GetComponent<AudioPlayer>().Play("paper1");
        }
        if (CurrentPostCount == 0)
        {
            StopTimer();
            GetComponent<AudioPlayer>().Play("bling");
            OnAllPostDelivered?.Invoke();
        }
        return true;
    }

    private void StartTimer()
    {
        IsTimerEnabled = true;
    }

    private void StopTimer()
    {
        IsTimerEnabled = false;
        Level++;
    }

    private void GetNewTarget()
    {
        if (CurrentPostCount == 0)
        {
            TargetHouseNo = 0;
            return;
        }

        TargetHouseNo = GetNextRandomHouse();
        OnNextPostBoxDecided?.Invoke(TargetHouseNo);
    }

    private int GetNextRandomHouse()
    {
        var houseNo = Random.Range(1, 13);  // 13 beacuse max is exlusive
        while (houseNo == TargetHouseNo)
        {
            houseNo = Random.Range(1, 13);
        }

        return houseNo;
    }

    private void RefreshUI()
    {
        TextTimer.text = Timer > 0 ? Math.Round(Timer,0).ToString() : string.Empty;
        TextPostCount.text = CurrentPostCount > 0 ? string.Format("{0} / {1}", CurrentPostCount, GotPostCount) : "Time to refill";
        TextHouseNo.text = TargetHouseNo != 0 ? string.Format("No. {0}", TargetHouseNo.ToString()) : string.Empty;
    }

    private void GameOver()
    {
        StartCoroutine("End");
        IsTimerEnabled = false;
    }

    IEnumerator End()
    {
        GetComponent<AudioPlayer>().Play("gameover");
        yield return new WaitForSeconds(3f);
        UIManager.EndGame(Collected);
    }
}
