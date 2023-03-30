using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerInstantiation : MonoBehaviour
{
    public Transform[] spawnPoints;
    public TMP_Text[] placements;
    GameObject[] allPlayers;

    public List<MinigamePoints> activeList;

    public List<MinigamePoints> idolRankings;
    public List<MinigamePoints> idolRankingsDistinct;

    public List<MinigamePoints> torchRankings;
    public List<MinigamePoints> torchRankingsDistinct;

    public List<MinigamePoints> sledRankings;
    public List<MinigamePoints> sledRankingsDistinct;

    public List<MinigamePoints> alligatorRankings;
    public List<MinigamePoints> alligatorRankingsDistinct;

    public List<MinigamePoints> catchUpRankings;
    public List<MinigamePoints> catchUpRankingsDistinct;

    public GameObject baseHat;
    public List<GameObject> specialHat;

    public GameObject skip, skipUI;

    GameObject theSpecialHat;

    int randHat;
    bool HatsDown;

    int startingHatsP1, startingHatsP2, startingHatsP3, startingHatsP4;

    public List<GameObject> hatsOrderP1, hatsOrderP2, hatsOrderP3, hatsOrderP4;

    Scene currentScene;
    string sceneName;
    void Start()
    {
        skip.SetActive(false);
        skipUI.SetActive(false);
        allPlayers = GameObject.FindGameObjectsWithTag("Player");
        
        //displaying data based on which hat progress scene is active (this lets us use the same script for each progress scene)
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        for (int j = 0; j < allPlayers.Length; j++)
        {
            allPlayers[j].transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("isRunning", false);
            allPlayers[j].transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("isWalking", false);
            //allPlayers[j].transform.GetComponent<PlayerMovement>().enabled = false;
            allPlayers[j].transform.GetChild(0).GetComponent<CatchUp>().enabled = false;
            allPlayers[j].transform.GetChild(0).GetComponent<TorchGame>().enabled = false;
            allPlayers[j].transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            allPlayers[j].transform.GetChild(0).transform.localPosition = Vector3.zero;
            allPlayers[j].transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
            allPlayers[j].transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
            allPlayers[j].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            allPlayers[j].transform.Rotate(0, 180, 0);

            if (sceneName == "15-HatProgressCatchUp")
            {
                for (int i = 0; i < allPlayers[j].transform.GetChild(0).transform.childCount; i++)
                {
                    if (allPlayers[j].transform.GetChild(0).transform.GetChild(i).name == "Hats")
                    {
                        for (int k = 0; k < allPlayers[j].transform.GetChild(0).transform.GetChild(i).childCount; k++)
                        {
                            allPlayers[j].transform.GetChild(0).transform.GetChild(i).GetChild(k).gameObject.SetActive(true);
                        }

                    }
                }
            }
            else if (sceneName == "19-HatProgressSled")
            {
                Destroy(allPlayers[j].transform.GetChild(0).GetComponent<SledGame>());

                if (allPlayers[j].transform.GetChild(0).GetComponent<BoxCollider>())
                {
                    Destroy(allPlayers[j].transform.GetChild(0).GetComponent<BoxCollider>());
                    
                }
                allPlayers[j].transform.GetChild(0).GetComponent<MeshCollider>().enabled = true;

                //allPlayers[j].transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("Selected", false);

                //enable hats
                for (int i = 0; i < allPlayers[j].transform.GetChild(0).transform.childCount; i++)
                {
                    if (allPlayers[j].transform.GetChild(0).GetChild(i).name == "Hats")
                    {
                        allPlayers[j].transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
                        allPlayers[j].transform.GetChild(0).GetChild(i).GetChild(0).gameObject.SetActive(true);

                    }
                }
            }
            else
            {
                //enable hats
                for (int i = 0; i < allPlayers[j].transform.GetChild(0).transform.childCount; i++)
                {
                    if (allPlayers[j].transform.GetChild(0).GetChild(i).name == "Hats")
                    {
                        allPlayers[j].transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
                        allPlayers[j].transform.GetChild(0).GetChild(i).GetChild(0).gameObject.SetActive(true);
                    }
                }
            }
        }

        //check what special hats are already visible and remove them from the special hat list
        if(GameObject.FindGameObjectsWithTag("Wizard").Length > 0)
        {
            for(int i = 0; i < specialHat.Count; i++)
            {
                if(specialHat[i].tag == "Wizard")
                {
                    specialHat.RemoveAt(i);
                }
            }
        }
        if (GameObject.FindGameObjectsWithTag("Chef").Length > 0)
        {
            for (int i = 0; i < specialHat.Count; i++)
            {
                if (specialHat[i].tag == "Chef")
                {
                    specialHat.RemoveAt(i);
                }
            }
        }
        if (GameObject.FindGameObjectsWithTag("Hockey").Length > 0)
        {
            for (int i = 0; i < specialHat.Count; i++)
            {
                if (specialHat[i].tag == "Hockey")
                {
                    specialHat.RemoveAt(i);
                }
            }
        }
        if (GameObject.FindGameObjectsWithTag("Cream").Length > 0)
        {
            //change angle of cream hat
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Cream").Length; i++ )
            {
                GameObject.FindGameObjectsWithTag("Cream")[i].transform.localRotation = Quaternion.Euler(0, 0, -20);
            }
            
            for (int i = 0; i < specialHat.Count; i++)
            {
                if (specialHat[i].tag == "Cream")
                {
                    specialHat.RemoveAt(i);
                }
            }
        }


        if(sceneName == "09-HatProgressTorch")
        {
            torchRankings = TorchSceneSetup.torchpoints;
            torchRankingsDistinct = TorchSceneSetup.distinct;
            activeList = torchRankings;

            displayData(torchRankings, torchRankingsDistinct);
            StartCoroutine(spawnHats());

            randHat = Random.Range(0, specialHat.Count);
        }
        else if (sceneName == "19-HatProgressSled")
        {  
            sledRankings = SledSceneSetup.sledpoints.Distinct(new ItemEqualityComparer()).ToList();
            sledRankingsDistinct = SledSceneSetup.sleddistinct;
            activeList = sledRankings;

            //track only first 4 in the list
            for(int i = 0; i < sledRankings.Count; i++)
            {
                Debug.Log(sledRankings[i].playerID);
                Debug.Log(sledRankings[i].playerPoints);
            }

            displayData(sledRankings, sledRankingsDistinct);
            StartCoroutine(spawnHats());

            randHat = Random.Range(0, specialHat.Count);
        }
        else if (sceneName == "12-HatProgressAlligator")
        {
            alligatorRankings = AlligatorSceneSetup.alligatorpoints;
            alligatorRankingsDistinct = AlligatorSceneSetup.distinct;
            activeList = alligatorRankings;

            displayData(alligatorRankings, alligatorRankingsDistinct);
            StartCoroutine(spawnHats());

            randHat = Random.Range(0, specialHat.Count);
        }
        else if (sceneName == "15-HatProgressCatchUp")
        {
            catchUpRankings = CatchUpSceneSetup.catchuppoints;
            catchUpRankingsDistinct = CatchUpSceneSetup.distinct;
            activeList = catchUpRankings;

            displayData(catchUpRankings, catchUpRankingsDistinct);
            StartCoroutine(spawnHatsCatchUp());
        }
        else if (sceneName == "05.5-HatProgressHideSmash")
        {
            idolRankings = HideSmashSetup.idolPoints;
            idolRankingsDistinct = HideSmashSetup.distinct;
            activeList = idolRankings;

            displayData(idolRankings, idolRankingsDistinct);
            StartCoroutine(spawnHats());

            randHat = Random.Range(0, specialHat.Count);
        }


        startingHatsP1 = GameObject.FindGameObjectWithTag("Player 1").transform.GetChild(3).childCount - 1;
        startingHatsP2 = GameObject.FindGameObjectWithTag("Player 2").transform.GetChild(3).childCount - 1;
        startingHatsP3 = GameObject.FindGameObjectWithTag("Player 3").transform.GetChild(3).childCount - 1;
        startingHatsP4 = GameObject.FindGameObjectWithTag("Player 4").transform.GetChild(3).childCount - 1;

        HatsDown = false;

    }

    private void Update()
    {
        //when special hat hits player, allow them to go to the next scene
        if (theSpecialHat != null && theSpecialHat.GetComponent<Rigidbody>().velocity.y >= -0.001f)
        {
            if (!HatsDown && theSpecialHat.transform.localPosition.y < 30)
            {
                skip.SetActive(true);
                skipUI.SetActive(true);

                //addJoints(GameObject.FindGameObjectWithTag("Player 1"), hatsOrderP1);
                //addJoints(GameObject.FindGameObjectWithTag("Player 2"), hatsOrderP2);
                //addJoints(GameObject.FindGameObjectWithTag("Player 3"), hatsOrderP3);
                //addJoints(GameObject.FindGameObjectWithTag("Player 4"), hatsOrderP4);

                HatsDown = true;
            }
        }

        //ui timing for catch up scene
        if (sceneName == "15-HatProgressCatchUp")
        {
            if (GameObject.FindGameObjectsWithTag("RegularHat").Length == CatchUpSceneSetup.totalHats + startingHatsP1 + startingHatsP2 + startingHatsP3 + startingHatsP4)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("RegularHat").Length; i++)
                {
                    if(GameObject.FindGameObjectsWithTag("RegularHat")[GameObject.FindGameObjectsWithTag("RegularHat").Length - 1].GetComponent<Rigidbody>().velocity.y == 0)
                    {
                        if(!HatsDown)
                        {
                            skip.SetActive(true);
                            skipUI.SetActive(true);
                            
                            //addJoints(GameObject.FindGameObjectWithTag("Player 1"), hatsOrderP1);
                            //addJoints(GameObject.FindGameObjectWithTag("Player 2"), hatsOrderP2);
                            //addJoints(GameObject.FindGameObjectWithTag("Player 3"), hatsOrderP3);
                            //addJoints(GameObject.FindGameObjectWithTag("Player 4"), hatsOrderP4);

                            HatsDown = true;
                        }
                    }
                }
            }
        }

    }

    void addJoints(GameObject player, List<GameObject> hatsOrder)
    {
        hatsOrder.Add(player);

        //adding base hats to list
        for (int i = 1; i < player.transform.GetChild(3).childCount; i++)
        {
            hatsOrder.Add(player.transform.GetChild(3).GetChild(i).gameObject);

            //remove hinge joints if they exist (remove this later)
            if(player.transform.GetChild(3).GetChild(i).GetComponent<HingeJoint>())
            {
                Destroy(player.transform.GetChild(3).GetChild(i).GetComponent<HingeJoint>());
            }
        }

        //add special hats to list
        for(int i = 0; i < player.transform.GetChild(3).GetChild(0).childCount; i++)
        {
            hatsOrder.Add(player.transform.GetChild(3).GetChild(0).GetChild(i).gameObject);

            //remove hinge joints if they exist (remove this later)
            if (player.transform.GetChild(3).GetChild(0).GetChild(i).GetComponent<HingeJoint>())
            {
                Destroy(player.transform.GetChild(3).GetChild(0).GetChild(i).GetComponent<HingeJoint>());
            }
        }

        //sorting hats based on their y position
        hatsOrder.Sort(delegate (GameObject a, GameObject b)
        {
            return (a.transform.position.y).CompareTo(b.transform.position.y);
        });
        hatsOrder.Reverse();

        //applying hinge joints
        for (int p = 0; p < hatsOrder.Count - 1; p++)
        {
            hatsOrder[p].AddComponent<HingeJoint>();
            hatsOrder[p].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            hatsOrder[p].GetComponent<Rigidbody>().useGravity = false;
            hatsOrder[p].GetComponent<Rigidbody>().mass = 3;
            hatsOrder[p].GetComponent<HingeJoint>().axis = new Vector3(0, -1, 0);

            //connect bodies
            hatsOrder[p].GetComponent<HingeJoint>().connectedBody = hatsOrder[p + 1].GetComponent<Rigidbody>();

            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }

        //change to speed value!!
        //player.transform.parent.GetComponent<PlayerMovement>().enabled = true;
        //player.GetComponent<Rigidbody>().isKinematic = false;
    }

    void displayData(List<MinigamePoints> activeRankings, List<MinigamePoints> activeRankingsDistinct)
    {
        for (int i = 0; i < activeRankings.Count; i++)
        {
            GameObject.Find(activeRankings[i].playerID).transform.parent.gameObject.transform.position = spawnPoints[i].position;

            for (int j = 0; j < activeRankingsDistinct.Count; j++)
            {
                if (activeRankings[i].playerPoints == activeRankingsDistinct[j].playerPoints)
                {
                    placements[i].SetText("" + (j + 1));
                    Debug.Log(activeRankings[i].playerID + " is in " + (j + 1) + " place!");

                    //play animation
                    if (j + 1 == 1)
                    {
                        if (sceneName == "19-HatProgressSled")
                        {
                            GameObject.Find(activeRankings[i].playerID).transform.GetChild(0).GetComponent<Animator>().SetBool("Selected", false);
                            GameObject.Find(activeRankings[i].playerID).transform.GetChild(0).GetComponent<Animator>().SetTrigger("VictorySled");
                        }
                        else
                        {
                            GameObject.Find(activeRankings[i].playerID).transform.GetChild(0).GetComponent<Animator>().SetTrigger("Victory");
                        }
                            
                    }
                    else
                    {
                        if (sceneName == "19-HatProgressSled")
                        {
                            GameObject.Find(activeRankings[i].playerID).transform.GetChild(0).GetComponent<Animator>().SetBool("Selected", false);
                            GameObject.Find(activeRankings[i].playerID).transform.GetChild(0).GetComponent<Animator>().SetBool("SledIdle", true);
                        }
                    }
                }
                
            }
        }
    }

    IEnumerator spawnHats()
    {
        //think about chaning this value based on timing of victory animation
        yield return new WaitForSeconds(2);
        float inc = 0;

        for(int z = 0; z < placements.Length; z++)
        {
            if(placements[z].text == "1")
            {
                //first place
                for (int i = 0; i < 3; i++)
                {
                    GameObject currentHat = Instantiate(baseHat, GameObject.Find(activeList[z].playerID).transform.GetChild(3).transform, true);
                    currentHat.transform.localPosition = new Vector3(0, 10f + inc, 0.035f);
                    currentHat.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    //currentHat.transform.localScale = new Vector3(1, 1, 1);
                    inc += 5;
                }
            }
            else if (placements[z].text == "2")
            {
                //second place
                for (int i = 0; i < 2; i++)
                {
                    GameObject currentHat = Instantiate(baseHat, GameObject.Find(activeList[z].playerID).transform.GetChild(3).transform, true);
                    currentHat.transform.localPosition = new Vector3(0, 10f + inc, 0.035f);
                    currentHat.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    //currentHat.transform.localScale = new Vector3(1, 1, 1);
                    inc += 5;
                }
            }
            else if (placements[z].text == "3")
            {
                //third place
                for (int i = 0; i < 1; i++)
                {
                    GameObject currentHat = Instantiate(baseHat, GameObject.Find(activeList[z].playerID).transform.GetChild(3).transform, true);
                    currentHat.transform.localPosition = new Vector3(0, 10f + inc, 0.035f);
                    currentHat.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    //currentHat.transform.localScale = new Vector3(1, 1, 1);
                    inc += 5;
                }
            }
            //fourth place (gets none)
        }

        yield return new WaitForSeconds(5);

        for (int z = 0; z < placements.Length; z++)
        {
            if (placements[z].text == "1")
            {
                //winner special hat
                theSpecialHat = Instantiate(specialHat[randHat], GameObject.Find(activeList[z].playerID).transform.GetChild(3).GetChild(0).transform, true);
                theSpecialHat.transform.localPosition = new Vector3(0, 10f + inc, 0.035f);
                theSpecialHat.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        
    }

    IEnumerator spawnHatsCatchUp()
    {
        yield return new WaitForSeconds(2);
        float inc = 0;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if(GameObject.FindGameObjectWithTag("Player 1").transform.parent.gameObject.transform.position == spawnPoints[i].transform.position)
            {
                for (int z = 0; z < GameObject.FindGameObjectWithTag("Player 1").GetComponent<CatchUp>().numHatsCollected; z++)
                {
                    GameObject currentHat = Instantiate(baseHat, GameObject.FindGameObjectWithTag("Player 1").transform.GetChild(3).transform, true);
                    currentHat.transform.localPosition = new Vector3(0, 10f + inc, 0.035f);
                    currentHat.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    //currentHat.transform.localScale = new Vector3(1, 1, 1);
                    inc += 5;

                }
            }
            else if (GameObject.FindGameObjectWithTag("Player 2").transform.parent.gameObject.transform.position == spawnPoints[i].transform.position)
            {
                for (int z = 0; z < GameObject.FindGameObjectWithTag("Player 2").GetComponent<CatchUp>().numHatsCollected; z++)
                {
                    GameObject currentHat = Instantiate(baseHat, GameObject.FindGameObjectWithTag("Player 2").transform.GetChild(3).transform, true);
                    currentHat.transform.localPosition = new Vector3(0, 10f + inc, 0.035f);
                    currentHat.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    //currentHat.transform.localScale = new Vector3(1, 1, 1);
                    inc += 5;
                }
            }
            else if (GameObject.FindGameObjectWithTag("Player 3").transform.parent.gameObject.transform.position == spawnPoints[i].transform.position)
            {
                for (int z = 0; z < GameObject.FindGameObjectWithTag("Player 3").GetComponent<CatchUp>().numHatsCollected; z++)
                {
                    GameObject currentHat = Instantiate(baseHat, GameObject.FindGameObjectWithTag("Player 3").transform.GetChild(3).transform, true);
                    currentHat.transform.localPosition = new Vector3(0, 10f + inc, 0.035f);
                    currentHat.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    //currentHat.transform.localScale = new Vector3(1, 1, 1);
                    inc += 5;
                }
            }
            else if (GameObject.FindGameObjectWithTag("Player 4").transform.parent.gameObject.transform.position == spawnPoints[i].transform.position)
            {
                for (int z = 0; z < GameObject.FindGameObjectWithTag("Player 4").GetComponent<CatchUp>().numHatsCollected; z++)
                {
                    GameObject currentHat = Instantiate(baseHat, GameObject.FindGameObjectWithTag("Player 4").transform.GetChild(3).transform, true);
                    //currentHat.tag = "Untagged";
                    currentHat.transform.localPosition = new Vector3(0, 10f + inc, 0.035f);
                    currentHat.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    //currentHat.transform.localScale = new Vector3(1, 1, 1);
                    inc += 5;
                }
            }
        }

    }

}
