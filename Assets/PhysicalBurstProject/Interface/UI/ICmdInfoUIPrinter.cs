using UnityEngine;

public interface ICmdInfoUIPrinter
{
    public void PrintUI(string name, string type, string description, string useMana);
    public void PrintUI(ICommand cmd);

    public void DestroyUI();
}
