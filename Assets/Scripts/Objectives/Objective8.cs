using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Objective8 : MonoBehaviour
{
    [SerializeField] private GameObject fadeOutPanel;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerY;
    [SerializeField] private GameObject flora;
    [SerializeField] private GameObject[] cutCams;
    [SerializeField] private GameObject levelEnd;
    [SerializeField] private GameObject cinematicAlien1;
    [SerializeField] private GameObject cinematicAlien2;
    [SerializeField] private GameObject alienCompund;
    [SerializeField] private GameObject health;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject nightmare;
    [SerializeField] private GameObject storyTextPanel;
    [SerializeField] private Text storyText;

    private ObjectiveManager objectiveManager;
    private AudioManager audioManager;
    private GameManager gameManager;

    private void Awake()
    {
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ObjectiveManager.LastCompletedObjective obj = objectiveManager.GetLastCompletedObjective();

            if (obj.Equals(ObjectiveManager.LastCompletedObjective.OBJ7))
            {
                fadeOutPanel.SetActive(true);
                fadeOutPanel.GetComponent<Animation>().Play();
                CompleteObject8();
                StartCoroutine(HandleObjectveFlow());
            }
        }
    }

    private void CompleteObject8()
    {
        objectiveManager.SetLastCompletedObjective(ObjectiveManager.LastCompletedObjective.OBJ8);
        objectiveManager.SetObjectivePanel("Objective 8 completed", 8);
    }

    IEnumerator HandleObjectveFlow()
    {
        yield return new WaitForSeconds(1.5f);
        PlayAudio("HeliCopter");
        flora.SetActive(false);
	    health.SetActive(false);
	    map.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        PlayAudio("ThemeForAliens");
        player.SetActive(false);
        alienCompund.SetActive(true);
        cinematicAlien1.SetActive(false);
        cinematicAlien2.SetActive(true);
        cutCams[0].SetActive(true);
        cutCams[0].GetComponent<Animation>().Play();
        fadeOutPanel.SetActive(false);

        yield return new WaitForSeconds(2.5f);
        fadeOutPanel.SetActive(true);
        fadeOutPanel.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(2f);
        PlayAudio("ManScream");

        yield return new WaitForSeconds(1f);
        playerY.SetActive(true);
        cutCams[0].SetActive(false);
        cutCams[1].SetActive(true);

        yield return new WaitForSeconds(1f);
        fadeOutPanel.SetActive(true);
        fadeOutPanel.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(2f);
        storyTextPanel.SetActive(true);
        cutCams[1].SetActive(false);
        cutCams[2].SetActive(true);
        nightmare.SetActive(true);
        playerY.GetComponentInChildren<Animator>().SetTrigger("FallenIdle");
        playerY.transform.position = new Vector3(playerY.transform.position.x, 1, playerY.transform.position.z);

        yield return new WaitForSeconds(2f);
        storyText.text = "Nighmare: You can't escape from me cooper";

        yield return new WaitForSeconds(6f);
        gameManager.isLevelEnd = true;
        storyText.text = "";
        storyTextPanel.SetActive(false);
        levelEnd.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void PlayAudio(string name)
    {
        audioManager.Play(name);
    }
}