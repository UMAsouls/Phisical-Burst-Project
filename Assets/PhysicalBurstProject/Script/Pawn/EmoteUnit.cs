using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EmoteUnit : MonoBehaviour
{

    [SerializeField]
    [Range(1f, 1.5f)]
    private float AttackMove;

    public void AttackEmote(Vector2 dis)
    {
        var dir = dis.normalized;

        var move1 = AttackMove;
        var move2 = dis.magnitude - 0.5f;

        float amove;

        if(move1 > move2) amove = move2;
        else amove = move1;

        transform.DOLocalMove((Vector3)dir*amove, 0.1f).SetEase(Ease.OutBounce).SetLoops(2, LoopType.Yoyo);
    }

    public void DodgeEffect(Vector2 dis)
    {
        var dir = -1*dis.normalized;

        transform.DOLocalMove(dir*0.8f, 0.2f)
            .SetDelay(0.3f)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.OutBack);
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}