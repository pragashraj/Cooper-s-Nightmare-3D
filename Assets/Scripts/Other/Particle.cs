using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] private GameObject effect;

    void Start()
    {
        Instantiate(effect, transform.position, transform.rotation);
    }

    void Update()
    {
        
    }
}