using UnityEngine;
using Meta.WitAi;
using Meta.WitAi.Json;

public class VoiceAssistant : MonoBehaviour
{
    public Wit wit; // Drag the Wit GameObject here
    public Light roomLight; // Drag your Light GameObject here
    public AudioSource audioSource; // Drag an AudioSource here
    public AudioClip wakeSound, lightOnSound, lightOffSound; // Assign sounds

    private bool isAwake = false;

    void Start()
    {
        if (wit == null)
        {
            Debug.LogError("Wit is not assigned! Please drag your Wit GameObject here.");
            return;
        }

        wit.VoiceEvents.OnResponse.AddListener(OnWitResponse);
    }

    void OnWitResponse(WitResponseNode response)
    {
        if (response == null || response["intents"].Count == 0)
        {
            Debug.Log("No intent detected.");
            return;
        }

        string intent = response["intents"][0]["name"];

        Debug.Log("Detected Intent: " + intent);

        if (intent == "wake_up" && !isAwake)
        {
            Debug.Log("Alexa Activated!");
            isAwake = true;
            PlaySound(wakeSound);
        }
        else if (intent == "turn_lights_on" && isAwake)
        {
            Debug.Log("Turning Lights On!");
            if (roomLight != null) roomLight.enabled = true;
            PlaySound(lightOnSound);
        }
        else if (intent == "turn_lights_off" && isAwake)
        {
            Debug.Log("Turning Lights Off!");
            if (roomLight != null) roomLight.enabled = false;
            PlaySound(lightOffSound);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void StartListening()
    {
        if (wit != null) wit.Activate();
    }

    public void StopListening()
    {
        if (wit != null) wit.Deactivate();
    }
}
