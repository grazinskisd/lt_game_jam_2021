using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject MainCanvas;
    GameObject EndGameCanvas;
    GameObject PlayerCamera;
    GameObject CanvasCamera;
    Text Score;

    bool IsFading = false;
    private void Awake()
    {
        MainCanvas = GameObject.Find("MainCanvas");
        EndGameCanvas = GameObject.Find("EndGameCanvas");
        PlayerCamera = GameObject.Find("PlayerCamera");
        CanvasCamera = GameObject.Find("CanvasCamera");
        Score = GameObject.Find("Score").GetComponent<Text>();
    }
    void Start()
    {
        DontDestroyOnLoad(this);
        EndGameCanvas.SetActive(false);
        CanvasCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFading)
        {

        }
    }

    public void EndGame(int postCount)
    {
        MainCanvas.SetActive(false);
        EndGameCanvas.SetActive(true);
        PlayerCamera.SetActive(false);
        CanvasCamera.SetActive(true);
        Score.text = string.Format("Posts collected: {0}", postCount);
    }

}
