using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoCheck : MonoBehaviour
{
    [SerializeField] VideoPlayer video;
    List<GameObject> players;
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
 
        video.loopPointReached += onVideoFinish;
    }

    void onVideoFinish(VideoPlayer vp)
    {
        SceneManager.LoadScene("23-Credits");
    }
}
