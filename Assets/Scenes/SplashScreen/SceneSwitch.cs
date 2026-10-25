using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class SceneSwitch : MonoBehaviour
{
    [BoxGroup("Timeline")]
    [SerializeField]
    [Required]
    private PlayableDirector playableDirector;
    
    [BoxGroup("Timeline")]
    [SerializeField]
    [Required]
    private TimelineAsset timeline;
    
    [BoxGroup("Marker")]
    [SerializeField]
    [Required]
    private SignalAsset skipMarkerStudio;
    
    [BoxGroup("Marker")]
    [SerializeField]
    [Required]
    private SignalAsset skipMarkerGame;
    
    [Scene]
    [SerializeField]
    private int sceneToLoad;

    private double _studioMarkerTime = -1;
    private double _gameMarkerTime = -1;
    
    private void Start()
    {
        // Find the markers, and save their timestamp
        foreach (var track in timeline.GetOutputTracks())
        {
            foreach (var marker in track.GetMarkers())
            {
                if (marker is not SignalEmitter emitter) continue;
                
                if (emitter.asset == skipMarkerStudio)
                    _studioMarkerTime = marker.time;
                else if (emitter.asset == skipMarkerGame)
                    _gameMarkerTime = marker.time;
            }
        }
    }
    
    private void OnEnable() => playableDirector.stopped += SwitchScene;
    private void OnDisable() => playableDirector.stopped -= SwitchScene;

    private void SwitchScene(PlayableDirector _)
    {
        Debug.Log($"[SceneSwitch] Switching to scene {sceneToLoad}");
        SceneManager.LoadScene(sceneToLoad);
    }
    
    // Callback from PlayerInput
    private void OnSkipSplash()
    {
        Debug.Log("[SceneSwitch] Skipping...");
        
        var currentTime = playableDirector.time;
        const double threshold = 0.1;
        
        if (currentTime < _studioMarkerTime - threshold)
        {
            Debug.Log($"[SceneSwitch] Skipping from {currentTime} to studio ({_studioMarkerTime})");
            playableDirector.time = _studioMarkerTime;
        } else if (currentTime < _gameMarkerTime - threshold)
        {
            Debug.Log($"[SceneSwitch] Skipping from {currentTime} to game ({_gameMarkerTime})");
            playableDirector.time = _gameMarkerTime;
        } else
        {
            Debug.Log($"[SceneSwitch] Skipping from {currentTime} to the end");
            playableDirector.time = playableDirector.duration; 
        }
    }
}
