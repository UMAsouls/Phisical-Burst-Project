using UnityEngine;

public interface IStandardUIPritner
{
    public void PrintUI(string name);
    public void DestroyUI(string name);
    public void PrintUIWorldPoint(string name, Vector3 point);
}
