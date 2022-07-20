using DG.Tweening;
using UnityEngine;

public class CameraFollowWithLerp : MonoBehaviour
{
    public Transform player;
    public ClickToMove move;
    private Vector3 _offset;
    bool _endStartTween;
    public float duration = 2.5f;
    public float startOffsetRatio = 0.2f;
    public Transform fastpass;

    void Start()
    {
        _offset = transform.position - player.position;

        _endStartTween = false;
        move.enabled = false;

        transform.position = player.position + _offset * startOffsetRatio;
        transform.DOMove(player.position + _offset, duration).SetEase(Ease.InOutQuad).OnComplete(
            () =>
            {
                if (fastpass != null)
                {
                    move.startDest = null;
                    player.gameObject.SetActive(false);
                    player.position = fastpass.position;
                    Invoke("ResumeFastPass", 0.5f);
                    return;
                }

                move.enabled = true;
                _endStartTween = true;
            });
    }

    void ResumeFastPass()
    {
        player.position = fastpass.position;
        player.gameObject.SetActive(true);
        move.enabled = true;
        _endStartTween = true;
    }

    void Update()
    {
        if (!_endStartTween)
            return;
        transform.position = player.position + _offset;
    }
}