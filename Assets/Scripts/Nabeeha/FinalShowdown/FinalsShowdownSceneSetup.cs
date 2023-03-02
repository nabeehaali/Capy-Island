using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FinalsShowdownSceneSetup : MonoBehaviour
{
    public TMP_Text gameover;
    public GameObject p1State, p2State, p3State, p4State;
    public List<GameObject> activePlayers;
    //public List<GameObject> p1Hats, p2Hats, p3Hats, p4Hats;
    
    bool isDeadP1 = false, isDeadP2 = false, isDeadP3 = false, isDeadP4 = false;
    public int hitCountP1 = 0, hitCountP2 = 0, hitCountP3 = 0, hitCountP4 = 0;
    int listIndexP1 = 0, listIndexP2 = 0, listIndexP3 = 0, listIndexP4 = 0;

    //FinalShowdownControls finalshowdowncontrolsP1, finalshowdowncontrolsP2, finalshowdowncontrolsP3, finalshowdowncontrolsP4;
    FinalsShowdownPlayerSettings finalshowdownplayersettings;
    public Transform winnerTarget;

    bool gameDone = false;
    void Start()
    {
        StartCoroutine(startGame());

        finalshowdownplayersettings = GameObject.Find("PlayerSettings").GetComponent<FinalsShowdownPlayerSettings>();

        //finalshowdowncontrolsP1 = GameObject.FindGameObjectWithTag("Player 1").transform.parent.gameObject.GetComponent<FinalShowdownControls>();
        //finalshowdowncontrolsP2 = GameObject.FindGameObjectWithTag("Player 2").transform.parent.gameObject.GetComponent<FinalShowdownControls>();
        //finalshowdowncontrolsP3 = GameObject.FindGameObjectWithTag("Player 3").transform.parent.gameObject.GetComponent<FinalShowdownControls>();
        //finalshowdowncontrolsP4 = GameObject.FindGameObjectWithTag("Player 4").transform.parent.gameObject.GetComponent<FinalShowdownControls>();

        //assign special hat ui to each player
        ActivateHats(GameObject.FindGameObjectWithTag("Player 1"), p1State);
        ActivateHats(GameObject.FindGameObjectWithTag("Player 2"), p2State);
        ActivateHats(GameObject.FindGameObjectWithTag("Player 3"), p3State);
        ActivateHats(GameObject.FindGameObjectWithTag("Player 4"), p4State);

        //value of hats starting off
        p1State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("" + ((GameObject.FindGameObjectWithTag("Player 1").transform.GetChild(3).childCount - 1) + GameObject.FindGameObjectWithTag("Player 1").transform.GetChild(3).GetChild(0).childCount));
        p2State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("" + ((GameObject.FindGameObjectWithTag("Player 2").transform.GetChild(3).childCount - 1) + GameObject.FindGameObjectWithTag("Player 2").transform.GetChild(3).GetChild(0).childCount));
        p3State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("" + ((GameObject.FindGameObjectWithTag("Player 3").transform.GetChild(3).childCount - 1) + GameObject.FindGameObjectWithTag("Player 3").transform.GetChild(3).GetChild(0).childCount));
        p4State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("" + ((GameObject.FindGameObjectWithTag("Player 4").transform.GetChild(3).childCount - 1) + GameObject.FindGameObjectWithTag("Player 4").transform.GetChild(3).GetChild(0).childCount));

        //list of active players
        activePlayers.Add(GameObject.FindGameObjectWithTag("Player 1"));
        activePlayers.Add(GameObject.FindGameObjectWithTag("Player 2"));
        activePlayers.Add(GameObject.FindGameObjectWithTag("Player 3"));
        activePlayers.Add(GameObject.FindGameObjectWithTag("Player 4"));
    }

    void Update()
    {
        // ui updates
        if(!isDeadP1)
        {
            p1State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("" + ((GameObject.FindGameObjectWithTag("Player 1").transform.GetChild(3).childCount - 1) + GameObject.FindGameObjectWithTag("Player 1").transform.GetChild(3).GetChild(0).childCount + " Hats Left!"));
        }
        if(!isDeadP2)
        {
            p2State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("" + ((GameObject.FindGameObjectWithTag("Player 2").transform.GetChild(3).childCount - 1) + GameObject.FindGameObjectWithTag("Player 2").transform.GetChild(3).GetChild(0).childCount + " Hats Left!"));
        }
        if(!isDeadP3)
        {
            p3State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("" + ((GameObject.FindGameObjectWithTag("Player 3").transform.GetChild(3).childCount - 1) + GameObject.FindGameObjectWithTag("Player 3").transform.GetChild(3).GetChild(0).childCount + " Hats Left!"));
        }
        if(!isDeadP4)
        {
            p4State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("" + ((GameObject.FindGameObjectWithTag("Player 4").transform.GetChild(3).childCount - 1) + GameObject.FindGameObjectWithTag("Player 4").transform.GetChild(3).GetChild(0).childCount + " Hats Left!"));
        }
        

        //special hats (if we do selection based)
        //if (finalshowdowncontrolsP1.moveHatL == true || finalshowdowncontrolsP2.moveHatL == true || finalshowdowncontrolsP3.moveHatL == true || finalshowdowncontrolsP4.moveHatL == true)
        //{
        //    //increase index by 1, for loop hats, diasable all except the one with active index (increase opacity)
        //    //this is when you enable a particular script based on hat that is selected
        //}
        //if (finalshowdowncontrolsP1.moveHatR == true || finalshowdowncontrolsP2.moveHatR == true || finalshowdowncontrolsP3.moveHatR == true || finalshowdowncontrolsP4.moveHatR == true)
        //{
        //    //increase index by 1, for loop hats, diasable all except the one with active index (increase opacity)
        //}

        //if the whole game is over
        if(activePlayers.Count == 1)
        {
            if(!gameDone)
            {
                Debug.Log("Game Over");
                StartCoroutine(finishGame());
                gameDone = true;
            }
        }
        
    }

    public void hatTrackingP1()
    {
        if (hitCountP1 > 0 && hitCountP1 % 5 == 0)
        {

            if (listIndexP1 == finalshowdownplayersettings.hatsOrderP1.Count - 1)
            {
                isDeadP1 = true;
                for(int i = 0; i < activePlayers.Count; i++)
                {
                    if(activePlayers[i] == GameObject.FindGameObjectWithTag("Player 1"))
                    {
                        activePlayers.RemoveAt(i);
                    }
                }

                Debug.Log("P1 is out!");
                //finalshowdownplayersettings.hatsOrderP1[listIndexP1].transform.parent.transform.position = new Vector3(0, -60, 0);
                EndGame(p1State);
                //move player up
                StartCoroutine(movePlayerUp(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(GameObject.FindGameObjectWithTag("Player 1").transform.position.x, GameObject.FindGameObjectWithTag("Player 1").transform.position.y + 50, GameObject.FindGameObjectWithTag("Player 1").transform.position.z), 5));
            }
            else
            {
                Destroy(finalshowdownplayersettings.hatsOrderP1[listIndexP1].GetComponent<HingeJoint>());
                finalshowdownplayersettings.hatsOrderP1[listIndexP1].GetComponent<Rigidbody>().useGravity = true;
                HatCheck(finalshowdownplayersettings.hatsOrderP1, listIndexP1, p1State);
                listIndexP1++;

                
            }
        }
    }

    public void hatTrackingP2()
    {
        if (hitCountP2 > 0 && hitCountP2 % 5 == 0)
        {

            if (listIndexP2 == finalshowdownplayersettings.hatsOrderP2.Count - 1)
            {
                isDeadP2 = true;
                for (int i = 0; i < activePlayers.Count; i++)
                {
                    if (activePlayers[i] == GameObject.FindGameObjectWithTag("Player 2"))
                    {
                        activePlayers.RemoveAt(i);
                    }
                }

                Debug.Log("P2 is out!");
                //finalshowdownplayersettings.hatsOrderP2[listIndexP2].transform.parent.transform.position = new Vector3(0, -60, 0);
                EndGame(p2State);
                StartCoroutine(movePlayerUp(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(GameObject.FindGameObjectWithTag("Player 2").transform.position.x, GameObject.FindGameObjectWithTag("Player 2").transform.position.y + 50, GameObject.FindGameObjectWithTag("Player 2").transform.position.z), 5));
            }
            else
            {
                Destroy(finalshowdownplayersettings.hatsOrderP2[listIndexP2].GetComponent<HingeJoint>());
                finalshowdownplayersettings.hatsOrderP2[listIndexP2].GetComponent<Rigidbody>().useGravity = true;
                HatCheck(finalshowdownplayersettings.hatsOrderP2, listIndexP2, p2State);
                listIndexP2++;
            }
        }
    }

    public void hatTrackingP3()
    {      
        if (hitCountP3 > 0 && hitCountP3 % 5 == 0)
        {

            if (listIndexP3 == finalshowdownplayersettings.hatsOrderP3.Count - 1)
            {
                isDeadP3 = true;
                for (int i = 0; i < activePlayers.Count; i++)
                {
                    if (activePlayers[i] == GameObject.FindGameObjectWithTag("Player 3"))
                    {
                        activePlayers.RemoveAt(i);
                    }
                }

                Debug.Log("P3 is out!");
                //finalshowdownplayersettings.hatsOrderP3[listIndexP3].transform.parent.transform.position = new Vector3(0, -60, 0);
                EndGame(p3State);
                StartCoroutine(movePlayerUp(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(GameObject.FindGameObjectWithTag("Player 3").transform.position.x, GameObject.FindGameObjectWithTag("Player 3").transform.position.y + 50, GameObject.FindGameObjectWithTag("Player 3").transform.position.z), 5));
            }
            else
            {
                Destroy(finalshowdownplayersettings.hatsOrderP3[listIndexP3].GetComponent<HingeJoint>());
                finalshowdownplayersettings.hatsOrderP3[listIndexP3].GetComponent<Rigidbody>().useGravity = true;
                HatCheck(finalshowdownplayersettings.hatsOrderP3, listIndexP3, p3State);
                listIndexP3++;
            }
        }
    }

    public void hatTrackingP4()
    {
        if (hitCountP4 > 0 && hitCountP4 % 5 == 0)
        {

            if (listIndexP4 == finalshowdownplayersettings.hatsOrderP4.Count - 1)
            {
                isDeadP4 = true;
                for (int i = 0; i < activePlayers.Count; i++)
                {
                    if (activePlayers[i] == GameObject.FindGameObjectWithTag("Player 4"))
                    {
                        activePlayers.RemoveAt(i);
                    }
                }

                Debug.Log("P4 is out!");
                //finalshowdownplayersettings.hatsOrderP4[listIndexP4].transform.parent.transform.position = new Vector3(0, -60, 0);
                EndGame(p4State);
                StartCoroutine(movePlayerUp(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(GameObject.FindGameObjectWithTag("Player 4").transform.position.x, GameObject.FindGameObjectWithTag("Player 4").transform.position.y + 50, GameObject.FindGameObjectWithTag("Player 4").transform.position.z), 5));
            }
            else
            {
                Destroy(finalshowdownplayersettings.hatsOrderP4[listIndexP4].GetComponent<HingeJoint>());
                finalshowdownplayersettings.hatsOrderP4[listIndexP4].GetComponent<Rigidbody>().useGravity = true;
                HatCheck(finalshowdownplayersettings.hatsOrderP4, listIndexP4, p4State);
                listIndexP4++;
            }
        }
    }

    void ActivateHats(GameObject player, GameObject ui)
    {
        for (int p = 0; p < ui.transform.GetChild(2).childCount; p++)
        {
            ui.transform.GetChild(2).GetChild(p).gameObject.SetActive(false);
        }

        //enabling hat UI that based on what hats they have in 3D
        for (int i = 0; i < player.transform.GetChild(3).GetChild(0).childCount; i++)
        {
            if (player.transform.GetChild(3).GetChild(0).GetChild(i).tag == "Wizard")
            {
                ui.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            }
            if (player.transform.GetChild(3).GetChild(0).GetChild(i).tag == "Chef")
            {
                ui.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
            }
            if (player.transform.GetChild(3).GetChild(0).GetChild(i).tag == "Hockey")
            {
                ui.transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
            }
            if (player.transform.GetChild(3).GetChild(0).GetChild(i).tag == "Cream")
            {
                ui.transform.GetChild(2).GetChild(3).gameObject.SetActive(true);
            }
        }

        //organizing hat UI
        int hatsActive = 0;
        for (int i = 0; i < player.transform.GetChild(3).GetChild(0).childCount; i++)
        {
            if(player.transform.GetChild(3).GetChild(0).GetChild(i).gameObject.activeSelf)
            {
                hatsActive++;
            }
        }

        if (hatsActive == 1 || hatsActive == 2)
        {
            ui.transform.GetChild(2).GetComponent<HorizontalLayoutGroup>().spacing = -154;
        }
        else if (hatsActive == 3)
        {
            ui.transform.GetChild(2).GetComponent<HorizontalLayoutGroup>().spacing = -74;
        }
        else if (hatsActive == 4)
        {
            ui.transform.GetChild(2).GetComponent<HorizontalLayoutGroup>().spacing = 0;
        }
    }

    void HatCheck(List<GameObject> hatsOrder, int listIndex, GameObject state)
    {
        StartCoroutine(redUI(state));

        if (hatsOrder[listIndex].tag == "Wizard")
        {
            if (state.transform.GetChild(2).GetChild(0).gameObject.activeSelf)
            {
                state.transform.GetChild(2).GetChild(0).GetComponent<Image>().CrossFadeAlpha(0f, 1.0f, true);
            }
        }
        if (hatsOrder[listIndex].tag == "Chef")
        {
            if (state.transform.GetChild(2).GetChild(1).gameObject.activeSelf)
            {
                state.transform.GetChild(2).GetChild(1).GetComponent<Image>().CrossFadeAlpha(0f, 1.0f, true);
            }
        }
        if (hatsOrder[listIndex].tag == "Hockey")
        {
            if (state.transform.GetChild(2).GetChild(2).gameObject.activeSelf)
            {
                state.transform.GetChild(2).GetChild(2).GetComponent<Image>().CrossFadeAlpha(0f, 1.0f, true);
            }
        }
        if (hatsOrder[listIndex].tag == "Cream")
        {
            if (state.transform.GetChild(2).GetChild(3).gameObject.activeSelf)
            {
                state.transform.GetChild(2).GetChild(3).GetComponent<Image>().CrossFadeAlpha(0f, 1.0f, true);
            }
        }
    }
    void EndGame(GameObject state)
    {
        state.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("Dead");
        state.GetComponent<Image>().CrossFadeAlpha(0.5f, 1.0f, true);
                
    }

    IEnumerator movePlayerUp(GameObject player, Vector3 targetPosition, float duration)
    {
        // add delay here for animation
        float time = 0;
        Vector3 startPosition = player.transform.position;
        while (time < duration)
        {
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        player.transform.position = targetPosition;
    }

    IEnumerator redUI(GameObject state)
    {
        Color32 tempCol = state.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
        state.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(1);
        state.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = tempCol;
    }
    IEnumerator startGame()
    {
        yield return new WaitForSeconds(2);
        gameover.gameObject.SetActive(true);
        int count = 3;

        while (count > 0)
        {
            gameover.SetText("" + count);
            yield return new WaitForSeconds(1);
            count--;
        }
        gameover.SetText("Start!");
        yield return new WaitForSeconds(1);
        gameover.gameObject.SetActive(false);

        GameObject.FindGameObjectWithTag("Player 1").transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 2").transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 3").transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 4").transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
    }

    IEnumerator finishGame()
    {
        yield return new WaitForSeconds(1);
        gameover.gameObject.SetActive(true);
        gameover.SetText("Game Over!");
        yield return new WaitForSeconds(2);

        if (activePlayers[0] == GameObject.FindGameObjectWithTag("Player 1"))
        {
            Debug.Log("Play p1 anim here");
        }
        else if (activePlayers[0] == GameObject.FindGameObjectWithTag("Player 2"))
        {
            Debug.Log("Play p2 anim here");
        }
        else if(activePlayers[0] == GameObject.FindGameObjectWithTag("Player 3"))
        {
            Debug.Log("Play p3 anim here");
        }
        else if (activePlayers[0] == GameObject.FindGameObjectWithTag("Player 4"))
        {
            Debug.Log("Play p4 anim here");
        }


    }

}
