using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {

  //Insert where sound needs to be played
  //FindObjectOfType<AudioManager>().Play("SoundName");

  public Sound[] sounds;

  public static AudioManager instance;

	// Method is called before Update
	void Awake () {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(gameObject);
      return;
    }

    DontDestroyOnLoad(gameObject);

    foreach (Sound s in sounds)
    {
      s.source = gameObject.AddComponent<AudioSource>();
      s.source.clip = s.clip;

      s.source.volume = s.volume;
      s.source.pitch = s.pitch;
      s.source.loop = s.loop;
      s.source.mute = s.mute;
    }
		
	}
  private void Start()
  {
    Play("Theme");
  }
  public void Play(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    if (s == null)
    {
      Debug.LogWarning("Sound: " + name + " not found!");
      return;
    }
     
    s.source.Play();
  }
}
