using UnityEngine;
using UnityEngine.UI;

public class PostController : MonoBehaviour
{
    [SerializeField]
    GameObject UITimer;
    Text TextTimer;
    [SerializeField]
    GameObject UIPostCount;
    Text TextPostCount;

    float Timer;
    bool IsTimerEnabled;
    int CurrentPostCount = 0;
    int GotPostCount = 0;
    void Start()
    {
        TextTimer = UITimer.GetComponent<Text>();
        TextPostCount = UIPostCount.GetComponent<Text>();
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
            CurrentPostCount = Random.Range(1, 4);
            GotPostCount = CurrentPostCount;
            Timer = 100;
            StartTimer();
        }
    }

    public void HandOverPost()
    {
        if (CurrentPostCount > 0)
        {
            CurrentPostCount -= 1;
            Timer += 10;
        }
        if (CurrentPostCount == 0)
        {
            StopTimer();
        }
    }

    private void StartTimer()
    {
        IsTimerEnabled = true;
    }

    private void StopTimer()
    {
        IsTimerEnabled = false;
    }
    private void RefreshUI()
    {
        TextTimer.text = Timer > 0 ? Timer.ToString() : string.Empty;
        TextPostCount.text = string.Format("{0} / {1}", CurrentPostCount, GotPostCount);
    }

    private void GameOver()
    {

    }
}
