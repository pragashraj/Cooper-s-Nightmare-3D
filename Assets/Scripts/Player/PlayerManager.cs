using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerX;
    [SerializeField] private GameObject fadeOutPanel;
    [SerializeField] private Material skybox;
    [SerializeField] private GameObject directLight;
    [SerializeField] private GameObject alienArmy;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject map;

    private UIManager uIManager;
    private AudioManager audioManager;

    private string[] introMessages = new string[] {
        "Cooper: What the hell was that",
        "Cooper: The dream seem so realistic",
        "Cooper: Flora.......................",
        "Cooper: Where are you guys ?"
    };

    private void Awake()
    {
        uIManager = FindObjectOfType<UIManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }


    void Start()
    {
        RenderSettings.skybox = skybox;
    }

    private void PlayAudio(string name)
    {
        audioManager.Play(name);
    }

    public void StartPlayerX()
    {
        alienArmy.SetActive(false);
        directLight.SetActive(true);
        StartCoroutine(StartPlayerXStory());
    }

    IEnumerator StartPlayerXStory()
    {
        yield return new WaitForSeconds(5f);
        PlayAudio("ManScream");

        yield return new WaitForSeconds(5f);
        fadeOutPanel.SetActive(true);
        fadeOutPanel.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(3f);
        playerX.SetActive(false);
        player.SetActive(true);

        yield return new WaitForSeconds(3f);
        fadeOutPanel.SetActive(false);
        uIManager.SetStoryTextPanel(introMessages);
        healthBar.SetActive(true);
        map.SetActive(true);
        PlayAudio("Theme");
    }
}