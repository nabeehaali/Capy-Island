using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstantiation : MonoBehaviour
{
    public Transform[] spawnPoints;
    //TorchSceneSetup torchScene;
    public List<TorchPoints> torchRankings;
    public List<TorchPoints> torchRankingsDistinct;
    void Start()
    {
       

        BeginGame(GameObject.FindGameObjectWithTag("Player 1"), 180);
        BeginGame(GameObject.FindGameObjectWithTag("Player 2"), 180);
        BeginGame(GameObject.FindGameObjectWithTag("Player 3"), 180);
        BeginGame(GameObject.FindGameObjectWithTag("Player 4"), 180);

        
    }

    private void BeginGame(GameObject player, float yAngle)
    {
        //for (int i = 0; i < spawnPoints.Length; i++)
        //{
        //    player.transform.position = new Vector3(0, 0, 0);
        //    player.transform.rotation = Quaternion.Euler(0, 0, 0);
        //    player.GetComponent<Rigidbody>().isKinematic = true;
        //    player.transform.parent.gameObject.transform.position = spawnPoints[i].position;
        //}

        player.transform.position = new Vector3(0, 0, 0);
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);

        torchRankings = TorchSceneSetup.torchpoints;
        torchRankingsDistinct = TorchSceneSetup.distinct;

        for (int i = 0; i < torchRankings.Count; i++)
        {
            for (int j = 0; j < torchRankingsDistinct.Count; j++)
            {
                if (torchRankings[i].playerPoints == torchRankingsDistinct[j].playerPoints)
                {
                    //this is where I would actually spawn the player in it's location (try to add extra for loop for spawnPoints
                    player.transform.parent.gameObject.transform.position = spawnPoints[i].position;
                    Debug.Log(torchRankings[i].playerID + " is in " + (j + 1) + " place!");
                }
            }
        }

        //player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("UI").Disable();
        //player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("Player").Enable();
    }
}
