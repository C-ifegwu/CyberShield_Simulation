using UnityEngine;
using System.Collections;

public class EnvironmentManager : MonoBehaviour
{
    [Header("Environment References")]
    public Light sunLight;
    public AudioSource heartbeatAudio;
    public AudioSource bgmAudio; // NEW: Reference to the background music audio source

    [Header("Atmosphere Settings")]
    public float normalIntensity = 1f;
    public float panicIntensity = 0.15f; 
    
    public Color normalColor = Color.white;
    public Color panicColor = new Color(0.8f, 0.2f, 0.2f); 
    
    [Header("Audio Volume Settings")]
    public float maxBgmVolume = 0.4f; // Max music volume during normal state
    public float maxHeartbeatVolume = 1.0f; // Max heartbeat volume during panic state

    public void TriggerPanic()
    {
        StopAllCoroutines();
        heartbeatAudio.Play();
        
        // Fades TO: Red Sky, Dim Light, LOUD Heartbeat, Muffled Music
        StartCoroutine(TransitionAtmosphere(panicIntensity, panicColor, maxHeartbeatVolume, 0f));
    }
    
    public void TriggerResolution()
    {
        StopAllCoroutines();
        
        // Fades TO: Bright Sky, White Light, ZERO Heartbeat, Normal Music
        StartCoroutine(TransitionAtmosphere(normalIntensity, normalColor, 0f, maxBgmVolume));
    }
    
    private IEnumerator TransitionAtmosphere(float targetLight, Color targetColor, float targetHeartbeat, float targetBgm)
    {
        float duration = 2.5f; 
        
        float startLight = sunLight.intensity;
        Color startColor = sunLight.color;
        float startHeartbeat = heartbeatAudio.volume;
        float startBgm = bgmAudio.volume; // NEW: Store the starting volume of the background music
        
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            float percent = time / duration;
            
            // Lerp all the things! Smoothly transition from the current state to the target state over time.
            sunLight.intensity = Mathf.Lerp(startLight, targetLight, percent);
            sunLight.color = Color.Lerp(startColor, targetColor, percent);
            heartbeatAudio.volume = Mathf.Lerp(startHeartbeat, targetHeartbeat, percent);
            bgmAudio.volume = Mathf.Lerp(startBgm, targetBgm, percent);
            
            yield return null;
        }
        
        if (targetHeartbeat == 0f)
        {
            heartbeatAudio.Stop();
        }
    }
}