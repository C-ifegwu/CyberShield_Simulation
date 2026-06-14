using UnityEngine;
using System.Collections;

public class EnvironmentManager : MonoBehaviour
{
    [Header("Environment References")]
    public Light sunLight;
    public AudioSource heartbeatAudio;
    public AudioSource bgmAudio; // NEW: The Calming Music

    [Header("Atmosphere Settings")]
    public float normalIntensity = 1f;
    public float panicIntensity = 0.15f; 
    
    public Color normalColor = Color.white;
    public Color panicColor = new Color(0.8f, 0.2f, 0.2f); 
    
    [Header("Audio Volume Settings")]
    public float maxBgmVolume = 0.4f; // Normal music volume
    public float maxHeartbeatVolume = 1.0f; // Max panic volume

    public void TriggerPanic()
    {
        StopAllCoroutines();
        heartbeatAudio.Play();
        
        // Fades TO: Dark Sky, Red Light, Loud Heartbeat, ZERO Music
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
        float startBgm = bgmAudio.volume; // Capture current music volume
        
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            float percent = time / duration;
            
            // Smoothly blend the lights AND the two audio tracks simultaneously 
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