using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class GameController : MonoBehaviour
{
    [SerializeField] private VideoPlayer introVideoPlayer;
    [SerializeField] private GameObject introVideoPanel;
    [SerializeField] private GameObject fadeInPanel;
    [SerializeField] private GameObject alienCompound;
    [SerializeField] private GameObject cinematicAlien1;
    [SerializeField] private GameObject cinematicAlien2;
    [SerializeField] private GameObject[] cutCameras;
    [SerializeField] private GameObject fadeOutPanel;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject cut3Trigger;
    [SerializeField] private GameObject alienArmy;
    [SerializeField] private GameObject playerX;
    [SerializeField] private Material skybox;
    [SerializeField] private float alienWalkingSpeed = 0f;
    [SerializeField] private float forwardZ = 1f;

    private AudioManager audioManager;
    private PlayerManager playerManager;
    private GameManager gameManager;

    private bool cutTriggeredEntered = false;
    private bool introVideoSkipped = false;
    private bool introVideoSkipHandled = false;

    public bool isCut3Triggered { get => cutTriggeredEntered; set => cutTriggeredEntered = value; }

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        playerManager = FindObjectOfType<PlayerManager>();
	gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        introVideoPanel.SetActive(true);
        introVideoPlayer.loopPointReached += CheckOver;

        RenderSettings.skybox = skybox;
    }

    void Update()
    {
	    if (introVideoSkipped && !introVideoSkipHandled)
	    {
	        introVideoSkipHandled = true;
	        StartCoroutine(stopIntroVideo(introVideoPlayer));
	    }

        if (isCut3Triggered)
        {
            HandleCut3Triggered();
        }

        HandleAlienWalk();
    }

    private void HandleCut3Triggered()
    {
        cinematicAlien1.SetActive(false);
        blackScreen.SetActive(false);

        fadeInPanel.SetActive(true);
        fadeInPanel.GetComponent<Animation>().Play();

        cinematicAlien2.SetActive(true);
        cutCameras[2].SetActive(true);
        cutCameras[1].SetActive(false);

        alienArmy.SetActive(true);
        StartCoroutine(StopFadeIn());

        alienWalkingSpeed = 0f;
        isCut3Triggered = false;
    }

    private void CheckOver(VideoPlayer vp)
    {
	    StartCoroutine(stopIntroVideo(vp));
    }

    public void HandleSkipOnClick() 
    {
	    introVideoSkipped = true;
    }

    private void HandleAlienWalk()
    {
        Vector3 alienMovement = new Vector3(0f, 0f, forwardZ) * alienWalkingSpeed * Time.deltaTime;
        cinematicAlien1.transform.Translate(alienMovement, Space.Self);
    }

    private void PlayAudio(string name)
    {
        audioManager.Play(name);
    }

    private void StopAudio(string name)
    {
        audioManager.Stop(name);
    }

    private void ActivateAlienCompound()
    {
        alienCompound.SetActive(true);
        fadeInPanel.SetActive(true);
        fadeInPanel.GetComponent<Animation>().Play();

        PlayAudio("ThemeForAliens");
        StartCoroutine(FadeOut());
    }

    IEnumerator stopIntroVideo(VideoPlayer vp)
    {
	introVideoPanel.SetActive(false);
        vp.Stop();
        ActivateAlienCompound();
        yield return new WaitForSeconds(2f);
	fadeInPanel.SetActive(false);
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(3f);
        fadeOutPanel.SetActive(true);
        fadeOutPanel.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(3f);
        cutCameras[1].SetActive(true);
        cutCameras[0].SetActive(false);
        fadeOutPanel.SetActive(false);

        yield return new WaitForSeconds(3f);
        fadeOutPanel.SetActive(true);
        fadeOutPanel.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(3f);
        fadeOutPanel.SetActive(false);
        blackScreen.SetActive(true);
    }

    IEnumerator StopFadeIn()
    {
        yield return new WaitForSeconds(2f);
        fadeInPanel.SetActive(false);
        cut3Trigger.SetActive(false);

        yield return new WaitForSeconds(8f);
        fadeOutPanel.SetActive(true);
        fadeOutPanel.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(3f);
        cutCameras[2].SetActive(false);
        cinematicAlien2.SetActive(false);
        StopAudio("ThemeForAliens");
        playerX.SetActive(true);
        playerManager.StartPlayerX();
	gameManager.isIntroEnd = true;
    }
}