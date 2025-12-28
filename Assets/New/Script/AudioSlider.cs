using UnityEngine;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
    [SerializeField]
    private AudioMixer Mixer;
    [SerializeField]
    private AudioSource AudioSource;

    [SerializeField]
    private string MixerGroupName;

    public void OnChangeSlider(float value)
    {
        float volume = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20;

        if (value == 0)
        {
            volume = -80f;
        }

        Mixer.SetFloat(MixerGroupName, volume);
    }
}
