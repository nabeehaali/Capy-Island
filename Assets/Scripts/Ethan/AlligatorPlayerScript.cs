using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class AlligatorPlayerScript : MonoBehaviour
{
    public bool isLeader = true;
    public int points = 0;
    public int increase = 1;
    public float interval = 0.5f;
    public float startTime = 5f;

    public TextMeshProUGUI display;
    public GameObject crownObj;

    float lastUpdate = 0;
    public bool isBit = false;
    string playerID;
    string playerShortDisplay;

    bool hasStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        playerID = gameObject.transform.GetChild(0).tag;
        Regex rgx = new Regex(@"([^A-Z0-9 -]|\s|)"); // remove spaces, numbers and non-capitals
        playerShortDisplay = rgx.Replace(playerID, "");
        display.text = playerShortDisplay + ":" + points;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted && Time.time >= startTime)
        {
            Debug.Log("Start!");
            gameObject.GetComponent<PlayerMovement>().enabled = true;
            hasStarted = true;
        }

        if(hasStarted && isLeader && Time.time - lastUpdate > interval)
        {
            points += increase;
            lastUpdate = Time.time;
        }
    }

    private void FixedUpdate()
    {
        // uncomment this soon!
        display.text = playerShortDisplay + ":" + points;
    }

    public void Bit()
    {
        // freezing player movement
        isBit = true;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(180, gameObject.transform.GetChild(0).transform.rotation.eulerAngles.y, 0);
        StartCoroutine(BiteReset());   
    }

    private void Steal()
    {
        Debug.Log(playerID + "has tried to steal!");
    }

    IEnumerator BiteReset()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log(playerID + "has recovered");
        gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, gameObject.transform.GetChild(0).transform.rotation.eulerAngles.y, 0);
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        isBit = false;

    }
}
