using System.Collections;
using TMPro;
using UnityEngine;

public class PawnStatusUI : MonoBehaviour, IPawnStatusUISetter
{
    [SerializeField]
    TextMeshProUGUI Name;

    [SerializeField]
    HPbar hpBar;

    [SerializeField]
    TextMeshProUGUI Priority;

    [SerializeField]
    TextMeshProUGUI PawnSentence;

    [SerializeField]
    TextMeshProUGUI PawnActionHabit;

    public void SetPawnStatus(IStatus status)
    {
        Name.text = status.Name;
        hpBar.Set(status.MaxHP, status.HP);
        Priority.text = status.Priority.ToString();
        PawnSentence.text = status.PawnSentence;
        PawnActionHabit.text = status.PawnActionHabit;
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