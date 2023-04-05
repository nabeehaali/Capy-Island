using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlligatorControls : MonoBehaviour
{
    public bool isLeader = false;
    public int points = 0;
    public int increase = 1;
    public float interval = 0.5f;

    //public TextMeshProUGUI display;
    public GameObject crownObj;
    public GameObject playerObj;
    CapySoundTrigger soundTrigger;

    float lastUpdate = 0;
    public bool isBit = false;
    string playerID;
    string playerShortDisplay;

    public bool hasStarted = false;
    public bool hasEnded = false; // accessed by game manager script to stop game for all players

    public bool canSteal = false;
    public bool isImmune = false;

    public float immuneTimeDuration = 2f;

    public ParticleSystem stealParticles;

    // Start is called before the first frame update
    void Start()
    {
        playerID = gameObject.transform.GetChild(0).tag;
        playerObj = gameObject.transform.GetChild(0).gameObject;
        soundTrigger = playerObj.GetComponent<CapySoundTrigger>();
        Regex rgx = new Regex(@"([^A-Z0-9 -]|\s|)"); // remove spaces, numbers and non-capitals
        playerShortDisplay = rgx.Replace(playerID, "");

        // finding the crown (only one should be in scene)
        // change this if that design changes in the future
        crownObj = GameObject.FindGameObjectsWithTag("Alligator Crown")[0];

        //DELETE AFTER TESTING
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted && isLeader && Time.time - lastUpdate > interval && !hasEnded)
        {
            points += increase;
            lastUpdate = Time.time;
        }
    }

    private void FixedUpdate()
    {
        // probably a better way to do this LOL
        if (isLeader)
        {
            gameObject.GetComponent<PlayerMovement>().speed = 30;
        }
        else
        {
            gameObject.GetComponent<PlayerMovement>().speed = 20;
        }
    }

    public void Bit()
    {
        // freezing player movement
        if (isLeader)
        {
            crownObj.transform.parent = null;
            crownObj.transform.position = new Vector3(playerObj.transform.position.x, crownObj.transform.position.y, playerObj.transform.position.z);
            isLeader = false;
        }
        isBit = true;
        gameObject.GetComponent<PlayerMovement>().animator.SetBool("isWalking", false); // a lil ugly lol
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        playerObj.transform.rotation = Quaternion.Euler(180, gameObject.transform.GetChild(0).transform.rotation.eulerAngles.y, 0);
        playerObj.transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y + 1.96f, playerObj.transform.position.z);
        soundTrigger.PlayHit();
        StartCoroutine(BiteReset());
    }

    public void Steal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (canSteal && !isBit)
            {
                if (crownObj.transform.parent != null)
                {
                    // basically just a catch statement before actually stealing the crown
                    if (crownObj.GetComponentInParent<AlligatorControls>().isLeader
                        && !crownObj.GetComponentInParent<AlligatorControls>().isImmune)
                    {
                        Debug.Log(playerID + "has tried to steal from " + crownObj.transform.parent.name);
                        crownObj.GetComponentInParent<AlligatorControls>().isLeader = false;
                        crownObj.transform.parent = playerObj.transform;
                        crownObj.transform.position = new Vector3(playerObj.transform.position.x, crownObj.transform.position.y, playerObj.transform.position.z);
                        isLeader = true;
                        isImmune = true;
                        soundTrigger.PlayChirp();
                        StealParticles();
                        StartCoroutine(ImmuneTimeout());
                    }
                }
                else
                {
                    // Debug.Log(playerID + "has tried to steal!");
                    crownObj.transform.parent = playerObj.transform;
                    crownObj.transform.position = new Vector3(playerObj.transform.position.x, crownObj.transform.position.y, playerObj.transform.position.z);
                    isLeader = true;
                    soundTrigger.PlayChirp();
                    StealParticles();
                }
            }
        }
        else if (context.canceled)
        {
            canSteal = false;
        }
    }

    void StealParticles() {
        Vector3 spawnPos = new Vector3(playerObj.transform.position.x, crownObj.transform.position.y, playerObj.transform.position.z);
        Instantiate(stealParticles, spawnPos, Quaternion.identity);
    }


    IEnumerator BiteReset()
    {
        yield return new WaitForSeconds(5f);
        // Debug.Log(playerID + "has recovered");
        playerObj.transform.position = new Vector3(gameObject.transform.GetChild(0).transform.position.x, gameObject.transform.GetChild(0).transform.position.y - 1.96f, gameObject.transform.GetChild(0).transform.position.z);
        playerObj.transform.rotation = Quaternion.Euler(0, gameObject.transform.GetChild(0).transform.rotation.eulerAngles.y, 0);
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        isBit = false;
    }

    IEnumerator ImmuneTimeout()
    {
        // resetting immunity after set duration
        yield return new WaitForSeconds(immuneTimeDuration);
        isImmune = false;
    }
}
