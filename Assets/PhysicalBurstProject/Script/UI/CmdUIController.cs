using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ICmdTextRectGetter))]
public class CmdUIController : MonoBehaviour
{

    private ICmdTextRectGetter getter;
    private ISelectorController selector;

    private int selectorIndex;
    private List<RectTransform> cmdTextRects;

    public void OnSelectorMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (!context.performed) return;

        if (moveInput.y > 0)
        {
            Move(-1);
        }

        if(moveInput.y < 0)
        {
            Move(1);
        }
        
    }

    public void Move(int dir)
    {
        cmdTextRects = getter.CmdTextRects;
        selectorIndex = (int)Mathf.Repeat(selectorIndex+dir, cmdTextRects.Count);

        var rectTransform = cmdTextRects[selectorIndex];
        Vector2 pos = new Vector2(rectTransform.rect.xMax, rectTransform.rect.yMax);
        pos += (Vector2)rectTransform.localPosition;

        selector.Move(pos);
    }

    private void Awake()
    {
        getter = GetComponent<ICmdTextRectGetter>();
        selector = GetComponentInChildren<ISelectorController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Move(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
