using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class DisasterSceneSetup : MonoBehaviour
{
    public static List<MinigamePoints> totalPoints = new List<MinigamePoints>();
    public List<MinigamePoints> totalPointsDistinct;

    public GameObject p1Slot, p2Slot, p3Slot, p4Slot;

    public static int p1HatsOff, p2HatsOff, p3HatsOff, p4HatsOff;
    public int removeFactor = 2;
    
    void Start()
    {
        //TODO: Need to add value of special hat children
        totalPoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 1").name, GameObject.FindGameObjectWithTag("Player 1").transform.GetChild(3).childCount - 1));
        totalPoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 2").name, GameObject.FindGameObjectWithTag("Player 2").transform.GetChild(3).childCount - 1));
        totalPoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 3").name, GameObject.FindGameObjectWithTag("Player 3").transform.GetChild(3).childCount - 1));
        totalPoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 4").name, GameObject.FindGameObjectWithTag("Player 4").transform.GetChild(3).childCount - 1));

        totalPoints.Sort();
        totalPoints.Reverse();

        totalPointsDistinct = totalPoints.Distinct(new ItemEqualityComparer()).ToList();

        p1HatsOff = (GameObject.FindGameObjectWithTag("Player 1").transform.GetChild(3).childCount - 1) / removeFactor;
        if ((GameObject.FindGameObjectWithTag("Player 1").transform.GetChild(3).childCount - 1) % removeFactor != 0) { p1HatsOff++; }
        p2HatsOff = (GameObject.FindGameObjectWithTag("Player 2").transform.GetChild(3).childCount - 1) / removeFactor;
        if ((GameObject.FindGameObjectWithTag("Player 2").transform.GetChild(3).childCount - 1) % removeFactor != 0) { p2HatsOff++; }
        p3HatsOff = (GameObject.FindGameObjectWithTag("Player 3").transform.GetChild(3).childCount - 1) / removeFactor;
        if ((GameObject.FindGameObjectWithTag("Player 3").transform.GetChild(3).childCount - 1) % removeFactor != 0) { p3HatsOff++; }
        p4HatsOff = (GameObject.FindGameObjectWithTag("Player 4").transform.GetChild(3).childCount - 1) / removeFactor;
        if ((GameObject.FindGameObjectWithTag("Player 4").transform.GetChild(3).childCount - 1) % removeFactor != 0) { p4HatsOff++; }

        Debug.Log(p1HatsOff);
        Debug.Log(p2HatsOff);
        Debug.Log(p3HatsOff);
        Debug.Log(p4HatsOff);

        for (int i = 0; i < totalPoints.Count; i++)
        {
           for (int j = 0; j < totalPointsDistinct.Count; j++)
            {
                if (totalPoints[i].playerPoints == totalPointsDistinct[j].playerPoints)
                {
                    if(totalPoints[i].playerID == GameObject.FindGameObjectWithTag("Player 1").name)
                    {
                        hatSetup(GameObject.Find(totalPoints[i].playerID), p1Slot, i);
                        StartCoroutine(countDown(GameObject.FindGameObjectWithTag("Player 1").transform.GetChild(3).childCount - 1, p1Slot, p1HatsOff));
                        destroyHats(GameObject.FindGameObjectWithTag("Player 1"), p1HatsOff);
                    }
                    else if (totalPoints[i].playerID == GameObject.FindGameObjectWithTag("Player 2").name)
                    {
                        hatSetup(GameObject.Find(totalPoints[i].playerID), p2Slot, i);
                        StartCoroutine(countDown(GameObject.FindGameObjectWithTag("Player 2").transform.GetChild(3).childCount - 1, p2Slot, p2HatsOff));
                        destroyHats(GameObject.FindGameObjectWithTag("Player 2"), p2HatsOff);
                    }
                    else if (totalPoints[i].playerID == GameObject.FindGameObjectWithTag("Player 3").name)
                    {
                        hatSetup(GameObject.Find(totalPoints[i].playerID), p3Slot, i);
                        StartCoroutine(countDown(GameObject.FindGameObjectWithTag("Player 3").transform.GetChild(3).childCount - 1, p3Slot, p3HatsOff));
                        destroyHats(GameObject.FindGameObjectWithTag("Player 3"), p3HatsOff);
                    }
                    else if (totalPoints[i].playerID == GameObject.FindGameObjectWithTag("Player 4").name)
                    {
                        hatSetup(GameObject.Find(totalPoints[i].playerID), p4Slot, i);
                        StartCoroutine(countDown(GameObject.FindGameObjectWithTag("Player 4").transform.GetChild(3).childCount - 1, p4Slot, p4HatsOff));
                        destroyHats(GameObject.FindGameObjectWithTag("Player 4"), p4HatsOff);
                    }
                }
            }
        }

    }
    void hatSetup(GameObject player, GameObject slot, int childIndex)
    {
        slot.transform.SetSiblingIndex(childIndex);
        slot.transform.GetChild(0).GetComponent<TMP_Text>().SetText("" + (player.transform.GetChild(3).childCount - 1) + " Hats");
    }

    void destroyHats(GameObject player, int numHats)
    {
        //boldest assumption ever! Assumes that the first child GO under each Hats GO is the special one and because of that an offset is created
        for (int i = 1; i < numHats + 1; i++)
        {
            Destroy(player.transform.GetChild(3).GetChild(i).gameObject);
        }
    }
    IEnumerator countDown(int seconds, GameObject slot, int hatsOff)
    {
        int count = seconds;

        while (count >= (seconds - hatsOff))
        {
            slot.transform.GetChild(0).GetComponent<TMP_Text>().SetText("" + count + " Hats");
            //change value here depending on length of animation
            yield return new WaitForSeconds(2);
            count--;
        }
    }
}
