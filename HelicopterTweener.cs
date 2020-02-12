using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HelicopterTweener : MonoBehaviour
{
    public Camera myCamera;
    public List<Transform> positionCamera;
    public ParticleSystem explosionPS;

    public Transform head;
    public Transform body;

    private void Start() {
        this.playFullSequence();
    }

    private void playFullSequence() {
        Sequence cameraSeq = loadCameraSequence();
        Sequence helixSeq = loadHelixSequence();
        Sequence flySeq = loadFlySequence();

        helixSeq.Insert(3, flySeq);

        //cameraSeq.Play();
        cameraSeq.Append(helixSeq);
        cameraSeq.Play();
        // helixSeq.Play();
    }

    private Sequence loadFlySequence() {
        Sequence flySequence = DOTween.Sequence();

        flySequence.Append(transform.DOMoveY(5, 7)).SetEase(Ease.InOutQuad);

        flySequence.Append(body.transform.DOShakePosition(1, .3f));

        flySequence.AppendCallback(() => explosionPS.Play());

        flySequence.Join(body.transform.DOMoveY(1, 1).SetEase(Ease.OutBounce));
        flySequence.Join(head.DOMoveY(30, .5f).SetEase(Ease.Linear));

        return flySequence;
    }

    private Sequence loadCameraSequence() {
        Sequence cameraSeq = DOTween.Sequence();

        cameraSeq.Append(myCamera.transform.DOMove(positionCamera[0].position, 3)
            .From()
            .SetEase(Ease.InOutQuad));

        return cameraSeq;
    }

    public Sequence loadHelixSequence() {
        Sequence helixSequence = DOTween.Sequence();

        helixSequence.Append(head.DORotate(new Vector3(0, 360, 0), 3f)
            .SetRelative()
            .SetEase(Ease.InQuad))
            .OnPlay(() =>  Debug.Log("CasquetVoladorInicia"));

        helixSequence.Append(head.DORotate(new Vector3(0, 360, 0), 1f)
            .SetRelative()
            .SetEase(Ease.Linear));

        helixSequence.Append(head.DORotate(new Vector3(0, 360, 0), .5f)
            .SetRelative()
            .SetLoops(666666, LoopType.Incremental)
            .SetEase(Ease.Linear));

        return helixSequence;
    }
}
