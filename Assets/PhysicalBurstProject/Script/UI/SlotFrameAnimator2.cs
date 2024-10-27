using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Graphic))]
[ExecuteAlways]
[DisallowMultipleComponent]
[AddComponentMenu("UI/Effects/SlotAnimator", 15)]
public class SlotFrameAnimator2 : UIBehaviour, IMaterialModifier
{
    [SerializeField]
    private float YellowFocus;

    [SerializeField]
    private float BlueFocus;

    [SerializeField]
    [Range(0, 1)]
    private int Burst;

    [SerializeField]
    [Range(0, 1)]
    private int Focus;

    [NonSerialized]
    private Graphic _graphic;
    public Graphic graphic => _graphic ? _graphic : _graphic = GetComponent<Graphic>();

    /// <summary>
    /// 変更用のマテリアル
    /// </summary>
    [NonSerialized]
    private Material _slotMaterial;

    public readonly int YellowFocusId = Shader.PropertyToID("_Yellow");
    public readonly int BlueFocusId = Shader.PropertyToID("_Blue");
    public readonly int BurstId = Shader.PropertyToID("_Burst");
    public readonly int FocusId = Shader.PropertyToID("_Selected");

    protected override void OnEnable()
    {
        base.OnEnable();
        if (graphic == null) return;
        _graphic.SetMaterialDirty();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (_slotMaterial != null) DestroyImmediate(_slotMaterial);
        _slotMaterial = null;

        if (graphic != null) _graphic.SetMaterialDirty();

    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        if (!IsActive() || graphic == null) return;
        graphic.SetMaterialDirty();
    }
#endif

    protected override void OnDidApplyAnimationProperties()
    {
        base.OnDidApplyAnimationProperties();
        if (!IsActive() || graphic == null) return;
        graphic.SetMaterialDirty();
    }

    public Material GetModifiedMaterial(Material baseMaterial)
    {
        bool block = !baseMaterial.HasProperty(YellowFocusId) ||
            !baseMaterial.HasProperty(BlueFocusId) ||
            !baseMaterial.HasProperty(BurstId) ||
            !baseMaterial.HasProperty(FocusId);

        // 変更に対応していないマテリアルを弾く
        if (IsActive() == false || _graphic == null || block)
            return baseMaterial;

        // マテリアル複製
        if (_slotMaterial == null)
        {
            _slotMaterial = new Material(baseMaterial);
            _slotMaterial.hideFlags = HideFlags.HideAndDontSave;
        }

        // これまでのプロパティを引き継ぐ
        _slotMaterial.CopyPropertiesFromMaterial(baseMaterial);

        _slotMaterial.SetFloat(YellowFocusId, YellowFocus);
        _slotMaterial.SetFloat(BlueFocusId, BlueFocus);
        _slotMaterial.SetInt(FocusId, Focus);
        _slotMaterial.SetInt(BurstId, Burst);

        return _slotMaterial;
    }
}