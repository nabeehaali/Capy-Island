using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CatchUp : MonoBehaviour
{
    public int numHatsCollected;
    public CatchUpControls catchupcontrols;

    public GameObject sandParticles;
    public GameObject regularHat;

    public List<GameObject> order;

    float inc = 0.35f;

    void Start()
    {
        catchupcontrols = transform.parent.gameObject.GetComponent<CatchUpControls>();
        Physics.IgnoreCollision(GetComponent<MeshCollider>(), GameObject.Find("Sand").GetComponent<MeshCollider>());

        order.Add(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BaseHat" && catchupcontrols.canDig == true)
        {
            //move hat a little up
            collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y + 0.2f, collision.gameObject.transform.position.z);

            //sand digging effect (collision.gameObject.transform.position)
            Instantiate(sandParticles, new Vector3(collision.gameObject.transform.position.x, -2.6f, collision.gameObject.transform.position.z), Quaternion.identity);

            //hat reaches the surface, destory it and reinstantiate hat on top of player's head
            if (collision.gameObject.transform.position.y >= -2.6f)
            {
                Destroy(collision.gameObject);
                numHatsCollected += 1;

                GameObject currentHat = Instantiate(regularHat, transform.GetChild(3).transform, true);
                currentHat.transform.localPosition = new Vector3(0, inc, 0.035f);
                currentHat.transform.localRotation = Quaternion.Euler(0, 0, 0);
                currentHat.GetComponent<Rigidbody>().isKinematic = true;
                order.Add(currentHat);
                applyHinges();
                inc += 0.65f;
                //StartCoroutine(hatmovement(currentHat));
            }

            
        }
    }

    void applyHinges()
    {
        //hinge joints
        for (int p = order.Count - 1; p > 0; p--)
        {
            if (order[p].GetComponent<HingeJoint>() == null)
            {
                order[p].AddComponent<HingeJoint>();
                order[p].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                order[p].GetComponent<Rigidbody>().useGravity = false;
                order[p].GetComponent<Rigidbody>().mass = 3;
                order[p].GetComponent<HingeJoint>().axis = new Vector3(0, -1, 0);

                //connect bodies
                order[p].GetComponent<HingeJoint>().connectedBody = order[p - 1].GetComponent<Rigidbody>();

                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                order[p].GetComponent<Rigidbody>().isKinematic = false;
            }
        }

    }

    IEnumerator hatmovement(GameObject hat)
    {
        yield return new WaitForSeconds(1.5f);
        hat.GetComponent<Rigidbody>().useGravity = false;
        //hat.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
    }
}
