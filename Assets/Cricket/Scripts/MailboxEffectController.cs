using System.Collections.Generic;
using UnityEngine;

public class MailboxEffectController : MonoBehaviour
{
    public Transform mailboxParent;

    private PostController _postController;
    private Dictionary<int, PostBox> _numberToBoxMap;

    private void Awake()
    {
        _postController = FindObjectOfType<PostController>();
        _postController.OnPostHanded += TurnOffParticles;
        _postController.OnNextPostBoxDecided += TurnOnParticles;

        _numberToBoxMap = new Dictionary<int, PostBox>();
        for (int i = 0; i < mailboxParent.childCount; i++)
        {
            var box = mailboxParent.GetChild(i).GetComponent<PostBox>();
            _numberToBoxMap.Add(box.number, box);
        }
    }

    private void TurnOnParticles(int number)
    {
        _numberToBoxMap[number].ps.Play();
    }

    private void TurnOffParticles(int number)
    {
        _numberToBoxMap[number].ps.Stop();
    }
}
