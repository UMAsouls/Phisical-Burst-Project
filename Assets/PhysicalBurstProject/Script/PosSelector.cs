using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PosSelector : MonoBehaviour, PosSelectorRangeSetter
{

    [Inject]
    CameraControllable cameraController;

    [Inject]
    private PosConfirmAble posConfirmAble;

    [Inject]
    private SystemSEPlayable systemSEPlayer;

    private Vector2 movedir;

    [SerializeField]
    float moveSpeed = 0.05f;

    private float range;

    private Vector2 firstPos;

    public float Range { set => range = value; }

    public void OnMove(InputValue value)
    {
       movedir = value.Get<Vector2>();
    }

    public void OnConfirm(InputValue value)
    {
        posConfirmAble.PosConfirm(transform.position);
        systemSEPlayer.ConfirmSE();
    }

    public void OnCancel(InputValue value)
    {
        posConfirmAble.Cancel();
        systemSEPlayer.CancelSE();
    }

    // Start is called before the first frame update
    void Start()
    {
        firstPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        transform.position += (Vector3)movedir*moveSpeed*dt;
        if(Vector2.Distance(transform.position, firstPos) > range)
        {
            transform.position = firstPos + ((Vector2)transform.position - firstPos).normalized*range;
        }
        float x = transform.position.x;
        float y = transform.position.y;

        transform.position = new Vector3(Mathf.Clamp(x, -20f, 20f), Mathf.Clamp(y, -15f, 15f), -1);

        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        cameraController.Position = transform.position;
    }
}
