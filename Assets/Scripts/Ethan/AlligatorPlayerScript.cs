using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class AlligatorPlayerScript : MonoBehaviour
{
    public bool hasCrown = true;
    public int points = 0;
    public int increase = 1;
    public float interval = 0.5f;
    public float startTime = 5f;

    public TextMeshProUGUI display;
    public GameObject crownObj;
    public GameObject playerObj;

    float lastUpdate = 0;
    public bool isBit = false;
    string playerID;
    string playerShortDisplay;

    bool hasStarted = false;

    public bool canSteal = false;
    public bool isImmune = false;


    // Start is called before the first frame update
    void Start()
    {
        playerID = gameObject.transform.GetChild(0).tag;
        playerObj = gameObject.transform.GetChild(0).gameObject;
        Regex rgx = new Regex(@"([^A-Z0-9 -]|\s|)"); // remove spaces, numbers and non-capitals
        playerShortDisplay = rgx.Replace(playerID, "");
        display.text = playerShortDisplay + ":" + points;

        // finding the crown (only one should be in scene)
        // change this if that design changes in the future
        crownObj = GameObject.FindGameObjectsWithTag("Alligator Crown")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted && Time.time >= startTime)
        {
            gameObject.GetComponent<PlayerMovement>().enabled = true;
            hasStarted = true;
        }

        if(hasStarted && hasCrown && Time.time - lastUpdate > interval)
        {
            points += increase;
            lastUpdate = Time.time;
        }
    }

    private void FixedUpdate()
    {
        // uncomment this soon!
        display.text = playerShortDisplay + ":" + points;

        // probably a better way to do this LOL
        if(hasCrown)
        {
            gameObject.GetComponent<PlayerMovement>().speed = 30;
        } else
        {
            gameObject.GetComponent<PlayerMovement>().speed = 20;
        }
    }

    public void Bit()
    {
        // freezing player movement
        if (hasCrown)
        {
            crownObj.transform.parent = null;
            crownObj.transform.position = new Vector3(playerObj.transform.position.x, crownObj.transform.position.y, playerObj.transform.position.z);
            hasCrown = false;
        }
        isBit = true;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(180, gameObject.transform.GetChild(0).transform.rotation.eulerAngles.y, 0);
        StartCoroutine(BiteReset());   
    }

    public void Steal()
    {
        if(canSteal && !isBit)
        {
            if(crownObj.transform.parent != null)
            {
                Debug.Log("Reached!");
                // basically just a catch statement before actually stealing the crown
                if (crownObj.GetComponentInParent<AlligatorPlayerScript>().hasCrown 
                    && !crownObj.GetComponentInParent<AlligatorPlayerScript>().isImmune)
                {
                    Debug.Log(playerID + "has tried to steal from " + crownObj.transform.parent.name);
                    crownObj.GetComponentInParent<AlligatorPlayerScript>().hasCrown = false;
                    crownObj.transform.parent = playerObj.transform;
                    crownObj.transform.position = new Vector3(playerObj.transform.position.x, crownObj.transform.position.y, playerObj.transform.position.z);
                    hasCrown = true;
                }
            } else
            {
                // Debug.Log(playerID + "has tried to steal!");
                crownObj.transform.parent = playerObj.transform;
                crownObj.transform.position = new Vector3(playerObj.transform.position.x, crownObj.transform.position.y, playerObj.transform.position.z);
                hasCrown = true;
            }
        }
    }

    IEnumerator BiteReset()
    {
        yield return new WaitForSeconds(5f);
        // Debug.Log(playerID + "has recovered");
        gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, gameObject.transform.GetChild(0).transform.rotation.eulerAngles.y, 0);
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        isBit = false;
    }
}
