using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class KeyView : MonoBehaviour
{
    [SerializeField]
    private Text _text = null;

    [SerializeField]
    private RectTransform _scaleTarget = null;

    public Color Color
    {
        set
        {
            _text.color = value;
            PlayScaleAnimation();
        }
    }

    public string Text
    {
        set
        {
            _text.text = value;
            PlayScaleAnimation();
        }

        get
        {
            return _text.text;
        }
    }

    private void PlayScaleAnimation()
    {
        
    }
}
