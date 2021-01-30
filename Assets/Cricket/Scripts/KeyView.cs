using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class KeyView : MonoBehaviour
{
    [SerializeField]
    private Text _text = null;

    [SerializeField]
    private RectTransform _scaleTarget = null;

    private Vector2 _startSize;
    private TweenerCore<Vector2, Vector2, VectorOptions> _tween;

    private void Awake()
    {
        _startSize = _scaleTarget.sizeDelta;
    }

    public Color Color
    {
        set
        {
            if (_text.color != value)
            {
                _text.color = value;
                PlayScaleAnimation();
            }
        }
    }

    public string Text
    {
        set
        {
            if (!_text.text.Equals(value))
            {
                _text.text = value;
                PlayScaleAnimation();
            }
        }

        get
        {
            return _text.text;
        }
    }

    private void PlayScaleAnimation()
    {
        if(_tween != null && _tween.IsPlaying())
        {
            _tween.Complete();
        }
        _tween = _scaleTarget.DOSizeDelta(_startSize * 1.4f, 0.2f).SetLoops(4, LoopType.Yoyo);
    }
}
