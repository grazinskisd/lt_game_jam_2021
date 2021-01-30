using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PostController : MonoBehaviour
{
    [SerializeField]
    GameObject UITimer;
    Text TextTimer;
    [SerializeField]
    GameObject UIPostCount;
    Text TextPostCount;
    [SerializeField]
    GameObject UIHouseNo;
    Text TextHouseNo;
    [SerializeField]
    List<GameObject> Houses = new List<GameObject>();

    float Timer;
    bool IsTimerEnabled;
    int CurrentPostCount = 0;
    int GotPostCount = 0;
    int TargetHouseNo = 0;
    int Level = 1;
    void Start()
    {
        TextTimer = UITimer.GetComponent<Text>();
        TextPostCount = UIPostCount.GetComponent<Text>();
        TextHouseNo = UIHouseNo.GetComponent<Text>();
        IsTimerEnabled = false;
    }

    void Update()
    {
        if (Timer > 0 && IsTimerEnabled)
        {
            Timer -= Time.deltaTime;
        }
        if (Timer <= 0)
        {
            GameOver();
        }
        RefreshUI();
    }

    public void GetPost()
    {
        if (CurrentPostCount == 0)
        {
            CurrentPostCount = Level;
            GotPostCount = CurrentPostCount;
            GetNewTarget();
            Timer = 100;
            StartTimer();
        }
    }

    public void HandOverPost(int houseNo)
    {
        if (houseNo != TargetHouseNo)
        {
            return;
        }
        if (CurrentPostCount > 0)
        {
            CurrentPostCount -= 1;
            Timer += 10;
            GetNewTarget();
        }
        if (CurrentPostCount == 0)
        {
            StopTimer();
            StopParticles();
        }
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
        var houseNo = Random.Range(1, 11);
        if (houseNo != TargetHouseNo)
        {
            StopParticles();   
            TargetHouseNo = houseNo;
            StartParticles();
        }
        else
        {
            GetNewTarget();
        }
    }

    private void RefreshUI()
    {
        TextTimer.text = Timer > 0 ? Math.Round(Timer,0).ToString() : string.Empty;
        TextPostCount.text = CurrentPostCount > 0 ? string.Format("{0} / {1}", CurrentPostCount, GotPostCount) : "Time to refill";
        TextHouseNo.text = TargetHouseNo != 0 ? string.Format("No. {0}", TargetHouseNo.ToString()) : string.Empty;
    }

    private void GameOver()
    {

    }

    private void StopParticles()
    {
        if (TargetHouseNo != 0)
        {
            Houses.Find(x => x.name == TargetHouseNo.ToString()).GetComponent<ParticleSystem>().Stop();
        }
    }

    private void StartParticles()
    {
        Houses.Find(x => x.name == TargetHouseNo.ToString()).GetComponent<ParticleSystem>().Play();
    }
}
