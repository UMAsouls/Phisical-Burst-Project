
using UnityEngine;

class VirtualPawnManager: MonoBehaviour, NeedStatus, IVirtualPawnManager
{
    [SerializeField]
    private GameObject virtualObjBase;
    private GameObject virtualObj;

    IVirtualPawn virtualPawn;
    IStatus status;

    public IVirtualPawn VirtualPawn => virtualPawn;

    public void VirtualPawnSet()
    {
        virtualObj = Instantiate(virtualObjBase, transform.position, Quaternion.identity);
        virtualPawn = virtualObj.GetComponent<IVirtualPawn>();
        Debug.Log($"vp: {virtualPawn}");
        virtualPawn.VirtualPos = transform.position;
        virtualPawn.VirtualMana = status.Mana;
        virtualPawn.VirtualHP = status.HP;
        virtualPawn.VirtualRange = status.Range;
    }

    public void VirtualPawnDestroy()
    {
        virtualPawn = null;
        Destroy( virtualObj );
    }

    public void SetStatus(IStatus status)
    {
        this.status = status;
    }


}
