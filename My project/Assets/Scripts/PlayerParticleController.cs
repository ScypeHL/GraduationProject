using UnityEngine;
using UnityEngine.Rendering;

public class PlayerParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    AbilityHitboxController hitbox;

    private void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        hitbox = GetComponent<AbilityHitboxController>();
        particle.Stop();
    }

    public void particle_start() 
    { 
        particle.Play();
    }
}
