using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CawanController : MonoBehaviour
{
    [SerializeField]
    private bool isLit;
    [SerializeField]
    private bool insideAreaInteract;
    [SerializeField]
    private bool canOpenTeleportList = false;
    [SerializeField]
    private float maxSanityRestore = 10f;

    [SerializeField]
    private Button buttonAvailable;

    [SerializeField] 
    private ParticleSystem fireParticle;
    [SerializeField]
    private GameObject restoreAreaBloomEffect;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip fireSound;
    [SerializeField]
    private Transform checkPointSpawn;

    private PlayerStateMachine players;

    private void Awake()
    {
        fireParticle.Stop();
        audioSource = GetComponent<AudioSource>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        players = player.GetComponent<PlayerStateMachine>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            insideAreaInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            insideAreaInteract = false;
            UI_Teleport_Menu.singelton.DisableTeleporterUI();
        }
    }

    private void Update()
    {
        LightTheFire();
        OpenTeleportList();
    }

    private void LightTheFire()
    {
        if (insideAreaInteract && !isLit && Input.GetKeyDown(KeyCode.F))
        {
            isLit = true;
            fireParticle.Play();

            audioSource.clip = fireSound;
            audioSource.loop = true;
            audioSource.Play();

            restoreAreaBloomEffect.SetActive(true);

            checkPointSpawn.gameObject.SetActive(true);

            buttonAvailable.interactable = true;

            players.SetSpawnOnCheckPoint(checkPointSpawn);

            players.SanityIncreases(maxSanityRestore);

            StartCoroutine(DelayBeforeOpeningTeleportList());
        }
    }

    private void OpenTeleportList()
    {
        if (insideAreaInteract && canOpenTeleportList && isLit && Input.GetKeyDown(KeyCode.F))
        {
            UI_Teleport_Menu.singelton.EnableTeleporterUI();
        }
    }

    private IEnumerator DelayBeforeOpeningTeleportList()
    {
        yield return new WaitForSeconds(2f); // Adjust delay duration as needed
        canOpenTeleportList = true;
    }
}
