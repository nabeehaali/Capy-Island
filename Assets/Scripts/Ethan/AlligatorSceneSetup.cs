using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlligatorSceneSetup : MonoBehaviour
{

    // !!! this doesn't do anything, just remnants from trying to dodge some compiler errors :(

    public TMP_Text countdown;
    public int countdownTime;

    float timePassed;
    bool gameDone = false;

    public GameObject p1State, p2State, p3State, p4State;

    public static List<MinigamePoints> alligatorpoints = new List<MinigamePoints>();
    public static List<MinigamePoints> distinct;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
