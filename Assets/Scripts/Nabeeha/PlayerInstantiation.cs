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

    //List<string> sledRankings = new List<string>();

    public GameObject baseHat;
    public GameObject specialHat;
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
        }

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if(sceneName == "HatProgressTorch")
        {
            torchRankings = TorchSceneSetup.torchpoints;
            torchRankingsDistinct = TorchSceneSetup.distinct;
            activeList = torchRankings;

            for (int i = 0; i < torchRankings.Count; i++)
            {
                GameObject.Find(torchRankings[i].playerID).transform.parent.gameObject.transform.position = spawnPoints[i].position;

                for (int j = 0; j < torchRankingsDistinct.Count; j++)
                {
                    if (torchRankings[i].playerPoints == torchRankingsDistinct[j].playerPoints)
                    {
                        placements[i].SetText("" + (j + 1));
                        Debug.Log(torchRankings[i].playerID + " is in " + (j + 1) + " place!");
                    }
                }
            }
        }
        else if (sceneName == "HatProgressSled")
        {  
            sledRankings = SledSceneSetup.sledpoints;
            sledRankingsDistinct = SledSceneSetup.sleddistinct;
            activeList = sledRankings;

            for (int i = 0; i < sledRankings.Count; i++)
            {
                GameObject.Find(sledRankings[i].playerID).transform.parent.gameObject.transform.position = spawnPoints[i].position;

                for (int j = 0; j < sledRankingsDistinct.Count; j++)
                {
                    if (sledRankings[i].playerPoints == sledRankingsDistinct[j].playerPoints)
                    {
                        placements[i].SetText("" + (j + 1));
                        Debug.Log(sledRankings[i].playerID + " is in " + (j + 1) + " place!");
                    }
                }
            }
        }
        

        StartCoroutine(spawnHats());

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
                    GameObject currentHat = Instantiate(baseHat, GameObject.Find(activeList[z].playerID).transform, true);
                    currentHat.tag = "Untagged";
                    currentHat.transform.localPosition = new Vector3(0, 0.1f + inc, 0.035f);
                    currentHat.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    currentHat.transform.localScale = new Vector3(1, 1, 1);
                    inc += 0.1f;
                }
            }
            else if (placements[z].text == "2")
            {
                //second place
                for (int i = 0; i < 2; i++)
                {
                    GameObject currentHat = Instantiate(baseHat, GameObject.Find(activeList[z].playerID).transform, true);
                    currentHat.tag = "Untagged";
                    currentHat.transform.localPosition = new Vector3(0, 0.1f + inc, 0.035f);
                    currentHat.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    currentHat.transform.localScale = new Vector3(1, 1, 1);
                    inc += 0.1f;
                }
            }
            else if (placements[z].text == "3")
            {
                //third place
                for (int i = 0; i < 1; i++)
                {
                    GameObject currentHat = Instantiate(baseHat, GameObject.Find(activeList[z].playerID).transform, true);
                    currentHat.tag = "Untagged";
                    currentHat.transform.localPosition = new Vector3(0, 0.1f + inc, 0.035f);
                    currentHat.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    currentHat.transform.localScale = new Vector3(1, 1, 1);
                    inc += 0.1f;
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
                GameObject winningHat = Instantiate(specialHat, GameObject.Find(activeList[z].playerID).transform, true);
                winningHat.transform.localPosition = new Vector3(0, 0.1f + inc, 0.035f);
                winningHat.transform.localRotation = Quaternion.Euler(0, 0, 0);
                winningHat.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }
        }

    }

}
