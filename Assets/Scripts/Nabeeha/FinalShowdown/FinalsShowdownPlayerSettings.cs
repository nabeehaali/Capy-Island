using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class FinalsShowdownPlayerSettings : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> hatsOrderP1, hatsOrderP2, hatsOrderP3, hatsOrderP4;

    float inc = 0;
    void Start()
    {
        BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(-10, 0, -60), 0, hatsOrderP1);
        BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(0, 0, -60), 0, hatsOrderP2);
        BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(-20, 0, -60), 0, hatsOrderP3);
        BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(10, 0, -60), 0, hatsOrderP4);
    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle, List<GameObject> hatsOrder)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("UI").Disable();
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("Player").Enable();
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 30;
        
        player.transform.parent.gameObject.GetComponent<SledControls>().enabled = false;
        player.transform.parent.gameObject.GetComponent<TorchControls>().enabled = false;
        player.transform.parent.gameObject.GetComponent<CatchUpControls>().enabled = false;
        player.transform.localPosition = Vector3.zero;
        player.GetComponent<TrailRenderer>().enabled = true;
        player.GetComponent<CatchUp>().enabled = false;
        player.GetComponent<TorchGame>().enabled = false;
        player.GetComponent<SledGame>().enabled = false;
        
        player.GetComponent<Rigidbody>().drag = 0;
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        hatsOrder.Add(player);

        //enable hats
        for (int i = 0; i < player.transform.childCount; i++)
        {
            if (player.transform.GetChild(i).name == "Hats")
            {

                for (int k = 0; k < player.transform.GetChild(i).childCount; k++)
                {
                    player.transform.GetChild(i).GetChild(k).gameObject.transform.localPosition = new Vector3(0, 0.6f + inc, 0.035f);
                    player.transform.GetChild(i).GetChild(k).gameObject.SetActive(true);
                    inc += 1;

                    if (player.transform.GetChild(i).GetChild(k).name == "SpecialHats")
                    {
                        for (int j = 0; j < player.transform.GetChild(i).GetChild(k).childCount; j++)
                        {
                            player.transform.GetChild(i).GetChild(k).GetChild(j).gameObject.transform.localPosition = new Vector3(0, 0.6f + inc, 0.035f);
                            inc += 1;
                            //adds special hats to list
                            hatsOrder.Add((player.transform.GetChild(i).GetChild(k).GetChild(j).gameObject));
                        }

                    }
                    else
                    {
                        //adds regular hats to list
                        hatsOrder.Add((player.transform.GetChild(i).GetChild(k).gameObject));
                    }
                }

            }
        }

        //sorting hats based on their y position
        hatsOrder.Sort(delegate (GameObject a, GameObject b)
        {
            return (a.transform.position.y).CompareTo(b.transform.position.y);
        });
        hatsOrder.Reverse();

        StartCoroutine(connectHats(hatsOrder, player));
       
        
    }

    IEnumerator connectHats(List<GameObject> hatsOrder, GameObject player)
    {
        yield return new WaitForSeconds(5);

        //hinge joints
        for (int p = 0; p < hatsOrder.Count - 1; p++)
        {
            hatsOrder[p].AddComponent<HingeJoint>();
            hatsOrder[p].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            hatsOrder[p].GetComponent<Rigidbody>().useGravity = false;
            hatsOrder[p].GetComponent<HingeJoint>().axis = new Vector3(0, 1, 0);

            //connect bodies
            hatsOrder[p].GetComponent<HingeJoint>().connectedBody = hatsOrder[p+1].GetComponent<Rigidbody>();

            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
    }
}
