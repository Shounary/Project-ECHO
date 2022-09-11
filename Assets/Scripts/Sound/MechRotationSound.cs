using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MechRotationSound : MonoBehaviour
{
    [SerializeField] private AudioSource mechAudio;
    [SerializeField] private float audioSensitivity = 0.5f, maxAcceleration = 0.1f, lerpFactor = 0.2f, audioStopThreshold = 0.015f;
    [SerializeField] private Transform transformToTrack;


    private Vector3 prev, curr;
    void Start()
    {
        prev = transformToTrack.forward;
        curr = transformToTrack.forward;
        mechAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float lerp = lerpFactor * Vector3.Distance(curr, transformToTrack.position);
        if (lerp > maxAcceleration)
            lerp = maxAcceleration;

        prev = Vector3.Lerp(prev, curr, lerp);
        curr = transformToTrack.forward;
        SetAudioVolume();
    }

    private void SetAudioVolume()
    {
        if (Vector3.Distance(curr, prev) < audioStopThreshold) {
            mechAudio.Stop();
        } else if (!mechAudio.isPlaying) {
            mechAudio.Play();
        }
        mechAudio.volume = Mathf.Clamp(audioSensitivity * Vector3.Distance(curr, prev), 0f, 0.5f);
    }
}
