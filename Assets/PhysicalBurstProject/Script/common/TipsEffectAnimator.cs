using static UnityEngine.ParticleSystem;
using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Graphic))]
[ExecuteAlways]
[DisallowMultipleComponent]
[AddComponentMenu("UI/Effects/TipsCharaAnimator", 15)]
public class TipsEffectAnimator: UIBehaviour, IMaterialModifier
{
    [SerializeField]
    int AnimNum;

    [SerializeField]
    Vector2 TileSize = new Vector2(240,240);



    [NonSerialized]
    private Graphic _graphic;
    public Graphic graphic => _graphic ? _graphic : _graphic = GetComponent<Graphic>();

    /// <summary>
    /// 変更用のマテリアル
    /// </summary>
    [NonSerialized]
    private Material _slotMaterial;

    public readonly int AnimationNumID= Shader.PropertyToID("_AnimationNumber");
    public readonly int TileSizeID = Shader.PropertyToID("_TileSize");

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
        bool block = !baseMaterial.HasProperty(AnimationNumID) || 
            !baseMaterial.HasProperty(TileSizeID);

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

        _slotMaterial.SetFloat(AnimationNumID, AnimNum);
        _slotMaterial.SetVector(TileSizeID, TileSize);

        return _slotMaterial;
    }
}
