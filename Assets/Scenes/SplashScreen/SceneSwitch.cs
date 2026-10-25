using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField]
    [Required]
    private PlayableDirector playableDirector;
    
    [Scene]
    public int sceneToLoad;

    private void OnEnable()
    {
        playableDirector.stopped += SwitchScene;
    }

    private void SwitchScene(PlayableDirector _)
    {
        Debug.Log("[IntroSceneSwitch] Switching to scene {sceneToLoad}");
        SceneManager.LoadScene(sceneToLoad);
    }
}
