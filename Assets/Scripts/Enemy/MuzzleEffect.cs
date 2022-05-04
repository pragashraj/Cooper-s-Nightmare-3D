using UnityEngine;

public class MuzzleEffect : MonoBehaviour
{
    [SerializeField] private GameObject effect;

    private AudioManager audioManager;

    private float HealthInterval = 0.5f;
    private float healthIntervalAccumulator;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void PlayAudio(string name)
    {
        audioManager.Play(name);
    }

    public void PlayMuzzleEffect()
    {
        if ((healthIntervalAccumulator += Time.deltaTime) >= HealthInterval)
        {
            healthIntervalAccumulator = 0.0f;

            GameObject effectInstance = Instantiate(effect, transform.position, transform.rotation);
            PlayAudio("Fire");
            Destroy(effectInstance, 20);
        }
    }
}