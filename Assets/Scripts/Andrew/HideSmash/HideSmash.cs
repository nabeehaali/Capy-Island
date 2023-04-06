using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideSmash : MonoBehaviour
{
    public int playerScore;
    CapySoundTrigger soundTrigger;

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
        if (sceneName == "05-HideSmash")
        {
            if (other.gameObject.tag == "Vase" && gameObject.GetComponentInParent<HideSmashControls>().isPush == true)
            {
                soundTrigger.PlayHit();
                Destroy(other.gameObject);
                playerScore++;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(sceneName == "05-HideSmash")
        {
            if (collision.transform.root.CompareTag("Player"))
            {
                soundTrigger.PlayChirp();
            }
        }
        
    }
}