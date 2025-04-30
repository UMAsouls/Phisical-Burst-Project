using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip bgm;

    [SerializeField]
    private float loopStart;

    [SerializeField] 
    private float loopEnd;

    [SerializeField]
    private AudioSource bgmSource;


    private bool bgmStop;

    public void MakeCts()
    {
    }

    public async UniTask PlayBGM()
    {
        

        bgmStop = false;

        bgmSource.clip = bgm;
        bgmSource.Play();

        await UniTask.WaitUntil(() => bgmSource.time >= loopEnd || bgmStop, cancellationToken: destroyCancellationToken);
        bgmSource.Stop();

        while(!bgmStop)
        {
            bgmSource.time = loopStart;
            bgmSource.Play();

            await UniTask.WaitUntil(() => bgmSource.time >= loopEnd || bgmStop, cancellationToken: destroyCancellationToken);
            bgmSource.Stop();
        }


    }

    //BGMループテスト用
    private async UniTask LoopTest()
    {

        bgmSource.clip = bgm;
        bgmSource.time = loopEnd - 5f;
        bgmSource.Play();

        await UniTask.WaitUntil(() => (bgmSource.time >= loopEnd || bgmStop), cancellationToken: destroyCancellationToken);
        bgmSource.Stop();

        bgmSource.time = loopStart;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmStop = true;
    }

    private void OnDestroy()
    {
    }

    // Use this for initialization
    void Start()
    {
        //PlayBGM().Forget();
        //LoopTest().Forget();
    }

    // Update is called once per frame
    void Update()
    {

    }
}