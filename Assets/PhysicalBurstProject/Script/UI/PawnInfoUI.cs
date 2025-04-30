using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PawnInfoUI : MonoBehaviour, IPawnInfoUI
{

    [SerializeField]
    TextMeshProUGUI ManaText;
    
    public string Name { set => nameTag.Text = value; }

    private GageSetable[] HPbars;
    private StringSetable nameTag;

    public void SetHP(int MaxHP, int HP)
    {
        foreach (var hp in HPbars)
        {
            hp.Set(MaxHP, HP);
        }
    }

    public void SetMana(int mana)
    {
        ManaText.text = $"–‚—Í: {mana}";
    }

    private void Awake()
    {
        HPbars = GetComponentsInChildren<GageSetable>();
        nameTag = GetComponentInChildren<StringSetable>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
