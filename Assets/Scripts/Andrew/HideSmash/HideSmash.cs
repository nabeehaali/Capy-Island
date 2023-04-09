using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideSmash : MonoBehaviour
{
    public int playerScore;
    CapySoundTrigger soundTrigger;
    public ParticleSystem poofCloud;

    Scene currentScene;
    string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        soundTrigger = GetComponent<CapySoundTrigger>();
        playerScore = 0;

        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay(Collision other)
    {
        if (sceneName == "05-HideSmash" || sceneName == "5.2-HideSmash")
        {
            if (other.gameObject.tag == "Vase" && gameObject.GetComponentInParent<HideSmashControls>().isPush == true)
            {
                soundTrigger.PlayHit();
                Instantiate(poofCloud, other.gameObject.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
                playerScore++;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(sceneName == "05-HideSmash" || sceneName == "5.2-HideSmash")
        {
            if (collision.transform.root.CompareTag("Player"))
            {
                soundTrigger.PlayChirp();
            }
        }
        
    }
}