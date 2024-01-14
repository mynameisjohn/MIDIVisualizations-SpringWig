using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientSampler : MonoBehaviour
{
    public Gradient _SkyBoxTopColorGradient;
    public Gradient _SkyBoxBottomColorGradient;
    public Material _SkyboxMaterial;

    public Gradient _CloudTopColorGradient;
    public Gradient _CloudBottomColorGradient;
    public Material _CloudMaterial;

    public Gradient _FlowerTopColorGradient;
    public Gradient _FlowerBottomColorGradient;
    public Material _FlowerMaterial;
    public float _FlowerOffset = 0f;

    public Gradient _StarParticleGradient;
    public Material _StarParticleMaterial;

    public Gradient _SunMoonTopColorGradient;
    public Gradient _SunMoonBottomColorGradient;
    public Material _SunMoonMaterial;

    private void Start()
    {
        UpdateGradients(FindObjectOfType<GameManager>()._TimeInCycleOffset);
    }

    public void UpdateGradients(float x)
    {
        _SkyboxMaterial.SetColor("_Color2", _SkyBoxTopColorGradient.Evaluate(x));
        _SkyboxMaterial.SetColor("_Color1", _SkyBoxBottomColorGradient.Evaluate(x));

        evaluateColrMaterial(_CloudTopColorGradient, _CloudBottomColorGradient, _CloudMaterial, x);
        evaluateColrMaterial(_FlowerTopColorGradient, _FlowerBottomColorGradient, _FlowerMaterial, Mathf.Repeat(x + _FlowerOffset, 1f));
        evaluateColrMaterial(_SunMoonTopColorGradient, _SunMoonBottomColorGradient, _SunMoonMaterial, x);

        _StarParticleMaterial.SetFloat("_FadeIn", _StarParticleGradient.Evaluate(x).a);
    }

    void evaluateColrMaterial(Gradient topGradient, Gradient bottomGradient, Material m, float x)
    {
        m.SetColor("_TopColor", topGradient.Evaluate(x));
        m.SetColor("_BottomColor", bottomGradient.Evaluate(x));
    }
}
