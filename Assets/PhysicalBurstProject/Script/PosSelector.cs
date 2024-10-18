using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PosSelector : MonoBehaviour, PosSelectorRangeSetter
{

    [Inject]
    SelectPhazeCameraControllable cameraController;

    [Inject]
    private PosConfirmAble posConfirmAble;

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
    }

    public void OnCancel(InputValue value)
    {
        posConfirmAble.Cancel();
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

        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        cameraController.Position = transform.position;
    }
}
