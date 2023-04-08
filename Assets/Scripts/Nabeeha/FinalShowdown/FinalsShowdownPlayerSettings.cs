using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class FinalsShowdownPlayerSettings : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> hatsOrderP1, hatsOrderP2, hatsOrderP3, hatsOrderP4;

    float inc = 0.6f;
    bool hatsConnected = false;
    void Start()
    {
        Debug.Log("Start");
        BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(-10, 0, -63), 0, hatsOrderP1);
        BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(16, 0, -43), -90, hatsOrderP2);
        BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(-42, 0, -45), 90, hatsOrderP3);
        BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(-11, 0, -19), 180, hatsOrderP4);
    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle, List<GameObject> hatsOrder)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.rotation = Quaternion.identity;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("UI").Disable();
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("Player").Enable();
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false;
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 30;

        player.transform.parent.gameObject.GetComponent<PlayerInstructions>().enabled = false;
        //player.transform.parent.gameObject.GetComponent<SledControls>().enabled = false;
        //player.transform.parent.gameObject.GetComponent<TorchControls>().enabled = false;
        //player.transform.parent.gameObject.GetComponent<CatchUpControls>().enabled = false;
        player.transform.parent.gameObject.GetComponent<FinalShowdownControls>().enabled = true;
        //player.transform.parent.gameObject.GetComponent<WizardHat>().enabled = true;

        player.transform.localPosition = Vector3.zero;
        player.transform.localRotation = Quaternion.identity;
        player.transform.GetChild(0).transform.localPosition = Vector3.zero;
        player.transform.GetChild(0).transform.localRotation = Quaternion.identity;

        player.GetComponent<TrailRenderer>().enabled = true;
        player.GetComponent<CatchUp>().enabled = false;
        player.GetComponent<TorchGame>().enabled = false;
        //player.GetComponent<SledGame>().enabled = false;
        player.GetComponent<FinalShowdown>().enabled = true;

        player.GetComponent<Rigidbody>().mass = 1;
        player.GetComponent<Rigidbody>().drag = 0;
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        //setting movement sound set
        player.GetComponent<CapySoundTrigger>().moveType = "GROUND";

        hatsOrder.Add(player);
        
        //enable hats
        for (int i = 0; i < player.transform.childCount; i++)
        {
            if (player.transform.GetChild(i).name == "Hats")
            {
                
                player.transform.GetChild(i).gameObject.SetActive(true);

                for (int k = 0; k < player.transform.GetChild(i).childCount; k++)
                {
                    if (player.transform.GetChild(i).GetChild(k).name == "SpecialHats")
                    {
                        (player.transform.GetChild(i).GetChild(k).gameObject).SetActive(true);

                        for (int j = 0; j < player.transform.GetChild(i).GetChild(k).childCount; j++)
                        {                            
                            player.transform.GetChild(i).GetChild(k).GetChild(j).gameObject.transform.localPosition = new Vector3(0, inc, 0.035f);
                            player.transform.GetChild(i).GetChild(k).GetChild(j).gameObject.SetActive(true);
                            inc += 1.5f;
                            //adds special hats to list
                            hatsOrder.Add((player.transform.GetChild(i).GetChild(k).GetChild(j).gameObject));
                        }

                    }
                    else
                    {
                        player.transform.GetChild(i).GetChild(k).gameObject.transform.localPosition = new Vector3(0, inc, 0.035f);
                        player.transform.GetChild(i).GetChild(k).gameObject.SetActive(true);
                        inc += 1.5f;
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

    //private void Update()
    //{
    //    connectAllHats(hatsOrderP1, GameObject.FindGameObjectWithTag("Player 1"));
    //    connectAllHats(hatsOrderP2, GameObject.FindGameObjectWithTag("Player 2"));
    //    connectAllHats(hatsOrderP3, GameObject.FindGameObjectWithTag("Player 3"));
    //    connectAllHats(hatsOrderP4, GameObject.FindGameObjectWithTag("Player 4"));
    //}

    void connectAllHats(List<GameObject> hatsOrder, GameObject player)
    {
        if(hatsOrder[0].GetComponent<Rigidbody>().velocity.y >= -0.001f && !hatsConnected)
        {
            for (int p = 0; p < hatsOrder.Count - 1; p++)
            {
                ConfigurableJoint CJ = hatsOrder[p].AddComponent<ConfigurableJoint>();
                hatsOrder[p].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                hatsOrder[p].GetComponent<Rigidbody>().useGravity = false;
                hatsOrder[p].GetComponent<Rigidbody>().mass = 3;
                hatsOrder[p].GetComponent<ConfigurableJoint>().axis = new Vector3(0, -1, 0);

                hatsOrder[p].GetComponent<ConfigurableJoint>().xMotion = ConfigurableJointMotion.Locked;
                hatsOrder[p].GetComponent<ConfigurableJoint>().yMotion = ConfigurableJointMotion.Locked;
                hatsOrder[p].GetComponent<ConfigurableJoint>().zMotion = ConfigurableJointMotion.Locked;
                hatsOrder[p].GetComponent<ConfigurableJoint>().angularXMotion = ConfigurableJointMotion.Free;
                hatsOrder[p].GetComponent<ConfigurableJoint>().angularYMotion = ConfigurableJointMotion.Locked;
                hatsOrder[p].GetComponent<ConfigurableJoint>().angularZMotion = ConfigurableJointMotion.Locked;

                JointDrive JDx = CJ.xDrive;
                JDx.positionSpring = 1000;
                JointDrive JDy = CJ.yDrive;
                JDy.positionSpring = 1000;
                JointDrive JDz = CJ.zDrive;
                JDz.positionSpring = 1000;

                JointDrive JDAx = CJ.angularXDrive;
                JDAx.positionSpring = 1000;
                JointDrive JDAyz = CJ.angularYZDrive;
                JDAyz.positionSpring = 1000;

                //connect bodies
                hatsOrder[p].GetComponent<ConfigurableJoint>().connectedBody = hatsOrder[p + 1].GetComponent<Rigidbody>();

                player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            }
            //call countdown
            StartCoroutine(GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().startGame());
            hatsConnected = true;
        }
    }
    
    
    IEnumerator connectHats(List<GameObject> hatsOrder, GameObject player)
    {
        yield return new WaitForSeconds(5);

        //hinge joints
        for (int p = 0; p < hatsOrder.Count - 1; p++)
        {
            /*hatsOrder[p].AddComponent<HingeJoint>();
            hatsOrder[p].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            hatsOrder[p].GetComponent<Rigidbody>().useGravity = false;
            hatsOrder[p].GetComponent<Rigidbody>().mass = 3;
            hatsOrder[p].GetComponent<HingeJoint>().axis = new Vector3(0, -1, 0);
            hatsOrder[p].GetComponent<HingeJoint>().breakForce = 100000;

            //connect bodies
            hatsOrder[p].GetComponent<HingeJoint>().connectedBody = hatsOrder[p+1].GetComponent<Rigidbody>();*/

            ConfigurableJoint CJ = hatsOrder[p].AddComponent<ConfigurableJoint>();
            hatsOrder[p].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            hatsOrder[p].GetComponent<Rigidbody>().useGravity = false;
            hatsOrder[p].GetComponent<Rigidbody>().mass = 3;
            hatsOrder[p].GetComponent<ConfigurableJoint>().axis = new Vector3(0, -1, 0);

            hatsOrder[p].GetComponent<ConfigurableJoint>().xMotion = ConfigurableJointMotion.Locked;
            hatsOrder[p].GetComponent<ConfigurableJoint>().yMotion = ConfigurableJointMotion.Locked;
            hatsOrder[p].GetComponent<ConfigurableJoint>().zMotion = ConfigurableJointMotion.Locked;
            hatsOrder[p].GetComponent<ConfigurableJoint>().angularXMotion = ConfigurableJointMotion.Free;
            hatsOrder[p].GetComponent<ConfigurableJoint>().angularYMotion = ConfigurableJointMotion.Locked;
            hatsOrder[p].GetComponent<ConfigurableJoint>().angularZMotion = ConfigurableJointMotion.Locked;

            JointDrive JDx = CJ.xDrive;
            JDx.positionSpring = 1000;
            JointDrive JDy = CJ.yDrive;
            JDy.positionSpring = 1000;
            JointDrive JDz = CJ.zDrive;
            JDz.positionSpring = 1000;

            JointDrive JDAx = CJ.angularXDrive;
            JDAx.positionSpring = 1000;
            JointDrive JDAyz = CJ.angularYZDrive;
            JDAyz.positionSpring = 1000;
            
            //connect bodies
            hatsOrder[p].GetComponent<ConfigurableJoint>().connectedBody = hatsOrder[p+1].GetComponent<Rigidbody>();

            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
    }
}
