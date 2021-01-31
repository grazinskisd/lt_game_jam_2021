using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject MainCanvas;
    GameObject EndGameCanvas;
    GameObject PlayerCamera;
    GameObject CanvasCamera;
    GameObject FadePanel;
    Text Score;

    bool IsFading = false;
    float Alpha = 0;
    private void Awake()
    {
        MainCanvas = GameObject.Find("MainCanvas");
        EndGameCanvas = GameObject.Find("EndGameCanvas");
        PlayerCamera = GameObject.Find("PlayerCamera");
        CanvasCamera = GameObject.Find("CanvasCamera");
        Score = GameObject.Find("Score").GetComponent<Text>();
        FadePanel = GameObject.Find("FadePanel");
    }
    void Start()
    {
        EndGameCanvas.SetActive(false);
        CanvasCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFading)
        {
            Alpha += Time.deltaTime / 4;
            FadePanel.GetComponent<Image>().color = new Color(0, 0, 0, Alpha);
        }
    }

    public void EndGame(int postCount)
    {
        MainCanvas.SetActive(false);
        EndGameCanvas.SetActive(true);
        PlayerCamera.SetActive(false);
        CanvasCamera.SetActive(true);
        Score.text = string.Format("Post delivered: {0}", postCount);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Fade(bool flag)
    {
        IsFading = flag;
    }

    public void TurnOffPanel()
    {
        FadePanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
