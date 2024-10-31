using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniStatusBar : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI priorityUI;

    [SerializeField]
    GameObject hpBar;

    [SerializeField]
    Vector2 barPos;

    private RectTransform rectTransform;

    private GageSetable hpbar;

    private IPawnInfo pawn;

    public IPawnInfo Pawn { set => pawn = value; }

    private void Awake()
    {
        hpbar = hpBar.GetComponent<GageSetable>();
    }

    // Use this for initialization
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        var graphic = GetComponent<Graphic>();
        var camera = graphic.canvas.worldCamera;

        Vector2 spos = RectTransformUtility.WorldToScreenPoint(camera, pawn.Position+ barPos);
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(graphic.rectTransform, spos, camera, out pos);
        rectTransform.localPosition = pos;
        */
        transform.position = pawn.Position + barPos;

        hpbar.Set(pawn.MaxHP, pawn.HP);
        priorityUI.text = pawn.Priority.ToString();
    }
}