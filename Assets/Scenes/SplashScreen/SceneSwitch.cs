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
    
    private void Update()
    {
        // Wait for animations to finish
        if (playableDirector.state != PlayState.Paused) return;
        
        Debug.Log("[IntroSceneSwitch] Switch to scene {sceneToLoad}");
        SceneManager.LoadScene(sceneToLoad);
    }
}
