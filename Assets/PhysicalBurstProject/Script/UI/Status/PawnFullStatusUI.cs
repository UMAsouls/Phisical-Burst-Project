using System.Collections;
using TMPro;
using UnityEngine;

public class PawnFullStatusUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Name;

    [SerializeField]
    TextMeshProUGUI HP;

    [SerializeField]
    TextMeshProUGUI Power;

    [SerializeField]
    TextMeshProUGUI Defence;

    [SerializeField]
    TextMeshProUGUI Speed;

    [SerializeField]
    FullStatusCharaController CharaController;

    public void Set(IStatus status)
    {
        Name.text = status.Name;
        HP.text = $"HP：{status.MaxHP}";
        Power.text = $"攻撃力：{status.AttackBase}";
        Defence.text = $"防御力：{status.DefenceBase}";
        Speed.text = $"素早さ：{status.SpeedBase}";

        CharaController.Set(status.Name);
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