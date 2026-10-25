// #define ENABLE_SCENE_SWITCH_LOG

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using Debug = UnityEngine.Debug;

public class SceneSwitch : MonoBehaviour
{
    [BoxGroup("Timeline")] [SerializeField] [Required]
    private PlayableDirector playableDirector;

    [BoxGroup("Timeline")] [SerializeField] [Required]
    private TimelineAsset timeline;

    [BoxGroup("Timeline")] [SerializeField] [Required]
    private SignalAsset skipSignal;

    [Scene] [SerializeField] 
    private int sceneToLoad;

    private readonly List<double> _skipMarkers = new List<double>();

    private void Start()
    {
        // Find the markers, and save their timestamp
        foreach (var track in timeline.GetOutputTracks())
        {
            foreach (var marker in track.GetMarkers())
            {
                if (marker is not SignalEmitter emitter) continue;

                if (emitter.asset == skipSignal)
                    _skipMarkers.Add(marker.time);
            }
        }

        Log($"Loaded {_skipMarkers.Count} markers: ");
        foreach (var markerT in _skipMarkers)
        {
            Log($" > Marker at: {markerT}");
        }
    }

    private void OnEnable() => playableDirector.stopped += SwitchScene;
    private void OnDisable() => playableDirector.stopped -= SwitchScene;

    private void SwitchScene(PlayableDirector _)
    {
        Log($"Switching to scene {sceneToLoad}");
        SceneManager.LoadScene(sceneToLoad);
    }

    // Callback from PlayerInput
    private void OnSkipSplash()
    {
        Log("Skipping...");

        var currentTime = playableDirector.time;
        const double threshold = 0.1;

        // Find a marker we can skip to
        foreach (var markerT in _skipMarkers.Where(t => t > currentTime + threshold))
        {
            Log($"Skipping from {currentTime} to {markerT}");
            playableDirector.time = markerT;
            return;
        }

        // No maker was found, skip to the end
        Log($"Skipping from {currentTime} to the end");
        playableDirector.time = playableDirector.duration;
    }
    
    [Conditional("ENABLE_SCENE_SWITCH_LOG")]
    private static void Log(string message)
    {
        Debug.Log(message);
    }
}