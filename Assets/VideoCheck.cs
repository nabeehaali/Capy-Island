using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoCheck : MonoBehaviour
{
    [SerializeField] VideoPlayer video;
    List<GameObject> players;
    public Animator transition;
    void Start()
    {
        players = FinalsShowdownSceneSetup.allPlayers;
        //Destroy Players
        for (int i = 0; i < players.Count; i++)
        {
            if(players[i].gameObject != null)
            {
                Destroy(players[i].gameObject);
            }
        }
        transition.SetTrigger("FadeOut");
        video.loopPointReached += onVideoFinish;
    }

    void onVideoFinish(VideoPlayer vp)
    {
        StartCoroutine(nextScene("23-Credits"));
        
    }

    IEnumerator nextScene(string name)
    {
        transition.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(name);
    }
}
