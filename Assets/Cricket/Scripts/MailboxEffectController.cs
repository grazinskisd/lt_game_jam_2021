using System.Collections.Generic;
using UnityEngine;

public class MailboxEffectController : MonoBehaviour
{
    public Transform mailboxParent;
    public PostController postController;

    private Dictionary<int, PostBox> _numberToBoxMap;

    private void Awake()
    {
        postController.OnPostHanded += TurnOffParticles;
        postController.OnNextPostBoxDecided += TurnOnParticles;

        _numberToBoxMap = new Dictionary<int, PostBox>();
        for (int i = 0; i < mailboxParent.childCount; i++)
        {
            var box = mailboxParent.GetChild(i).GetComponent<PostBox>();
            _numberToBoxMap.Add(box.number, box);
        }
    }

    private void TurnOnParticles(int number)
    {
        Debug.Log("Turn on " + number);
        _numberToBoxMap[number].ps.Play();
    }

    private void TurnOffParticles(int number)
    {
        Debug.Log("Turn off " + number);
        _numberToBoxMap[number].ps.Stop();
    }
}
