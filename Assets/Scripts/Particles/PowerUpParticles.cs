using UnityEngine;

public class PowerUpParticles : MonoBehaviour
{
    [Header("Referneces")]
    [SerializeField]
    private GameObject particleLeft;
    [SerializeField]
    private GameObject particleRight;

    [Header("Material")]
    [SerializeField]
    private Material material;

    private const string _colorReference = "Color_a3e5d141c8e64507b2cd059d0bbaeb02";

    public void ActivateParticle(Vector4 color)
    {
        material.SetColor(_colorReference, color);
        particleLeft.SetActive(true);
        particleRight.SetActive(true);
    }

    public void DisableParticle()
    {
        if (particleLeft == null || particleRight == null)
            return;
        particleLeft.SetActive(false);
        particleRight.SetActive(false);
    }

    private void Start()
    {
        DisableParticle();
    }
}
