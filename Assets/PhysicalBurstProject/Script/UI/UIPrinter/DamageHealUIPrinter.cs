using System.Collections;
using TMPro;
using UnityEngine;

public class DamageHealUIPrinter : UIPrinter, IDamageHealUIPrinter
{

    [SerializeField]
    GameObject DamageText;

    [SerializeField]
    GameObject HealText;

    private void PrintText(Vector2 pos, int val, GameObject text)
    {
        var realPos = WorldToUIPoint(pos);
        Debug.Log("PoinTextPos: " + realPos);
        var obj = PrintUIAsChildAt(text, realPos);
        var tmp = obj.GetComponent<TextMeshProUGUI>();
        tmp.text = val.ToString();
    }

    public void PrintDamage(Vector2 pos, int damage)
    {
        PrintText(pos, damage, DamageText);
    }

    public void PrintHeal(Vector2 pos, int heal)
    {
        PrintText(pos, heal, HealText);
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