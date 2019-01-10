using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {

  //Insert where sound needs to be played
  //FindObjectOfType<AudioManager>().PlayAt("SoundName");

  public Sound[] sounds;

  public static AudioManager instance;

  static float stopTimeTheme;
  static float resumeTimeTheme;

  static float stopTimeElevator;
  static float resumeTimeElevator;

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
    PlayAt("Theme");
  }
  private void Update()
  {
    DecideCurrentMusic();
  }
  private void DecideCurrentMusic()
  {
    if (AreaLevelChanger.initiatedLevelChange)
    {
      //Collided with LevelChanger in Theme Level?
      if (AreaLevelChanger.CurrentLevel % 2 == 0)
      {
        stopTimeTheme = GetTime("Theme");
        resumeTimeTheme = stopTimeTheme;

        Debug.Log("stopTimeTheme:" + stopTimeTheme);
        Debug.Log("resumeTimeTheme:" + resumeTimeTheme);

        Pause("Theme");
        PlayAt("Elevator", resumeTimeElevator);
        AreaLevelChanger.initiatedLevelChange = false;
      }
      //Collided with LevelChanger in Elevator Level?
      else
      {
        stopTimeElevator = GetTime("Elevator");
        resumeTimeElevator = stopTimeElevator;

        Debug.Log("stopTimeElevator:" + stopTimeElevator);
        Debug.Log("resumeTimeElevator:" + resumeTimeElevator);

        Pause("Elevator");
        PlayAt("Theme", resumeTimeTheme);
        AreaLevelChanger.initiatedLevelChange = false;
      }
    }
  }
  public void PlayAt(string name, float startTime = 0)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    
    if (s == null)
    {
      Debug.LogWarning("Sound: " + name + " not found!");
      return;
    }
    s.source.time = startTime;
    s.source.Play();
  }
  public void Pause(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    if (s == null)
    {
      Debug.LogWarning("Sound" + name + " not found!");
      return;
    }
    s.source.Pause();
  }
  public float GetTime(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    if (s == null)
    {
      Debug.LogWarning("Sound" + name + " not found!");
      return 0;
    }
    return s.source.time;
  }
}
