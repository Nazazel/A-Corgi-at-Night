using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mutePls : MonoBehaviour {
    private bool muted;

    public void SetAudioMute(bool mute)
    {
        AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        for (int index = 0; index < sources.Length; ++index)
        {
            if(sources[index].gameObject.name != "BGController")
            {
                sources[index].mute = mute;
            }
        }
        muted = mute;
    }
}
