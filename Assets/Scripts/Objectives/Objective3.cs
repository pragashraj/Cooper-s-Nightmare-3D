using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Objective3 : MonoBehaviour
{
    [SerializeField] private GameObject characterFlora;
    [SerializeField] private Vector3 pos;
    [SerializeField] private Vector3 rotation;

    private ObjectiveManager objectiveManager;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ObjectiveManager.LastCompletedObjective obj = objectiveManager.GetLastCompletedObjective();

            if (obj.Equals(ObjectiveManager.LastCompletedObjective.OBJ2))
            {
                characterFlora.transform.position = pos;
                characterFlora.transform.Rotate(rotation);
                characterFlora.SetActive(true);
                characterFlora.GetComponent<CharacterFloraController>().HandleStandAndIdle();
                characterFlora.GetComponent<CharacterMovement>().enabled = false;
                characterFlora.GetComponent<NavMeshAgent>().enabled = true;
                characterFlora.GetComponent<CharacterAI>().enabled = true;
                characterFlora.GetComponent<NavMeshAgent>().isStopped = true;
                CompleteObject3();
                StartCoroutine(HandleObjectveFlow());
            }
        }
    }

    private void CompleteObject3()
    {
        objectiveManager.SetLastCompletedObjective(ObjectiveManager.LastCompletedObjective.OBJ3);
        objectiveManager.SetObjectivePanel("Objective 3 completed", 3);
    }

    private void PlayAudio(string name)
    {
        audioManager.Play(name);
    }

    IEnumerator HandleObjectveFlow()
    {
        PlayAudio("Floraa_oh my_i need");

        yield return new WaitForSeconds(4f);
	    characterFlora.GetComponent<CharacterFloraController>().HandleIdleAnim();
    }
}