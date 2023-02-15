using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerInstantiation : MonoBehaviour
{
    public Transform[] spawnPoints;
    public TMP_Text[] placements;
    GameObject[] allPlayers;

    public List<MinigamePoints> activeList;

    public List<MinigamePoints> torchRankings;
    public List<MinigamePoints> torchRankingsDistinct;

    public List<MinigamePoints> sledRankings;
    public List<MinigamePoints> sledRankingsDistinct;

    public GameObject baseHat;
    public List<GameObject> specialHat;
    void Start()
    {
        allPlayers = GameObject.FindGameObjectsWithTag("Player");

        for (int j = 0; j < allPlayers.Length; j++)
        {
            allPlayers[j].transform.GetChild(0).GetComponent<CatchUp>().enabled = false;
            allPlayers[j].transform.GetChild(0).GetComponent<TorchGame>().enabled = false;
            allPlayers[j].transform.GetChild(0).GetComponent<SledGame>().enabled = false;
            allPlayers[j].transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            allPlayers[j].transform.GetChild(0).transform.localPosition = Vector3.zero;
            allPlayers[j].transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
            allPlayers[j].transform.Rotate(0, 180, 0);

            //enable hats
            for (int i = 0; i < allPlayers[j].transform.childCount; i++)
            {
                if (allPlayers[j].transform.GetChild(i).GetChild(3).name == "Hats")
                {
                    allPlayers[j].transform.GetChild(i).GetChild(3).gameObject.SetActive(true);
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
            for (int i = 0; i < specialHat.Count; i++)
            {
                if (specialHat[i].tag == "Cream")
                {
                    specialHat.RemoveAt(i);
                }
            }
        }


        //displaying data based on which hat progress scene is active (this lets us use the same script for each progress scene)
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if(sceneName == "HatProgressTorch")
        {
            torchRankings = TorchSceneSetup.torchpoints;
            torchRankingsDistinct = TorchSceneSetup.distinct;
            activeList = torchRankings;

            displayData(torchRankings, torchRankingsDistinct);
        }
        else if (sceneName == "HatProgressSled")
        {  
            sledRankings = SledSceneSetup.sledpoints;
            sledRankingsDistinct = SledSceneSetup.sleddistinct;
            activeList = sledRankings;

            displayData(sledRankings, sledRankingsDistinct);
        }
        

        StartCoroutine(spawnHats());

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
                }
            }
        }
    }

    IEnumerator spawnHats()
    {
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
                    //currentHat.tag = "Untagged";
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
                    //currentHat.tag = "Untagged";
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
                    //currentHat.tag = "Untagged";
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
                int randHat = Random.Range(0, specialHat.Count);
                GameObject winningHat = Instantiate(specialHat[randHat], GameObject.Find(activeList[z].playerID).transform.GetChild(3).GetChild(0).transform, true);
                winningHat.transform.localPosition = new Vector3(0, 10f + inc, 0.035f);
                winningHat.transform.localRotation = Quaternion.Euler(0, 0, 0);
                winningHat.transform.localScale = new Vector3(65, 65, 65);

            }
        }

    }

}
