using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
public class MapSceneSetup : MonoBehaviour
{
    public List<MinigamePoints> totalPoints = new List<MinigamePoints>();
    public List<MinigamePoints> totalPointsDistinct;

    public GameObject p1Slot, p2Slot, p3Slot, p4Slot;
    public Animator transition;

    Scene currentScene;
    string sceneName;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if(sceneName != "04-MapAnimation1")
        {
            transition.SetTrigger("FadeOut");
        }

        totalPoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 1").name, (GameObject.FindGameObjectWithTag("Player 1").transform.GetChild(3).childCount - 1) + GameObject.FindGameObjectWithTag("Player 1").transform.GetChild(3).GetChild(0).childCount));
        totalPoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 2").name, (GameObject.FindGameObjectWithTag("Player 2").transform.GetChild(3).childCount - 1) + GameObject.FindGameObjectWithTag("Player 2").transform.GetChild(3).GetChild(0).childCount));
        totalPoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 3").name, (GameObject.FindGameObjectWithTag("Player 3").transform.GetChild(3).childCount - 1) + GameObject.FindGameObjectWithTag("Player 3").transform.GetChild(3).GetChild(0).childCount));
        totalPoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 4").name, (GameObject.FindGameObjectWithTag("Player 4").transform.GetChild(3).childCount - 1) + GameObject.FindGameObjectWithTag("Player 4").transform.GetChild(3).GetChild(0).childCount));

        totalPoints.Sort();
        totalPoints.Reverse();

        totalPointsDistinct = totalPoints.Distinct(new ItemEqualityComparer()).ToList();

        for (int i = 0; i < totalPoints.Count; i++)
        {
            for (int j = 0; j < totalPointsDistinct.Count; j++)
            {
                if (totalPoints[i].playerPoints == totalPointsDistinct[j].playerPoints)
                {
                    if (totalPoints[i].playerID == GameObject.FindGameObjectWithTag("Player 1").name)
                    {
                        hatSetup(GameObject.Find(totalPoints[i].playerID), p1Slot, i);
                    }
                    else if (totalPoints[i].playerID == GameObject.FindGameObjectWithTag("Player 2").name)
                    {
                        hatSetup(GameObject.Find(totalPoints[i].playerID), p2Slot, i);
                    }
                    else if (totalPoints[i].playerID == GameObject.FindGameObjectWithTag("Player 3").name)
                    {
                        hatSetup(GameObject.Find(totalPoints[i].playerID), p3Slot, i);
                    }
                    else if (totalPoints[i].playerID == GameObject.FindGameObjectWithTag("Player 4").name)
                    {
                        hatSetup(GameObject.Find(totalPoints[i].playerID), p4Slot, i);
                    }
                }
            }
        }

    }
    void hatSetup(GameObject player, GameObject slot, int childIndex)
    {
        slot.transform.SetSiblingIndex(childIndex);
        slot.transform.GetChild(0).GetComponent<TMP_Text>().SetText("" + ((player.transform.GetChild(3).childCount - 1) + player.transform.GetChild(3).GetChild(0).childCount) + " Hats");
    }
}
