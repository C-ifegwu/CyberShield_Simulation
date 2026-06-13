using UnityEngine;
using System.Collections;

public class EnvironmentManager : MonoBehaviour
{
    [Header("Environment References")]
    public Light sunLight;
    public AudioSource heartbeatAudio;
    
    [Header("Atmosphere Settings")]
    public float normalIntensity = 1f;
    public float panicIntensity = 0.15f; // Very dark
    
    public Color normalColor = Color.white;
    public Color panicColor = new Color(0.8f, 0.2f, 0.2f); // Creepy reddish tint
    
    public void TriggerPanic()
    {
        StopAllCoroutines();
        heartbeatAudio.Play();
        StartCoroutine(TransitionAtmosphere(panicIntensity, panicColor, 1f));
    }
    
    public void TriggerResolution()
    {
        StopAllCoroutines();
        StartCoroutine(TransitionAtmosphere(normalIntensity, normalColor, 0f));
    }
    
    private IEnumerator TransitionAtmosphere(float targetIntensity, Color targetColor, float targetVolume)
    {
        float duration = 2.5f; // Takes 2.5 seconds to fade, building suspense
        
        float startIntensity = sunLight.intensity;
        Color startColor = sunLight.color;
        float startVolume = heartbeatAudio.volume;
        
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            float percent = time / duration;
            
            // Smoothly lerp (blend) all the values
            sunLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, percent);
            sunLight.color = Color.Lerp(startColor, targetColor, percent);
            heartbeatAudio.volume = Mathf.Lerp(startVolume, targetVolume, percent);
            
            yield return null;
        }
        
        // If the volume faded completely to 0, stop playing the audio file
        if (targetVolume == 0f)
        {
            heartbeatAudio.Stop();
        }
    }
}