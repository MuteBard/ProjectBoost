using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    [SerializeField] List<string> audioClipNames;
    [SerializeField] List<AudioClip> audioClipFiles;
    [SerializeField] string currentClipAlias;
    Dictionary<string, AudioClip> dictionary;

    void Awake(){
        audioSource = GetComponent<AudioSource>();
        dictionary = Zip();
    }

    public void Play(string audioClipName, float duration = 1){
        AudioClip audioClipFile = dictionary[audioClipName];
        currentClipAlias = audioClipName;
        audioSource.PlayOneShot(audioClipFile, duration);
    }

    public bool IsPlaying(){
        return audioSource.isPlaying == true;
    }

    public string CurrentClipAlias(){
        return currentClipAlias;
    }

    public bool IsNotPlaying(){
        return audioSource.isPlaying == false;
    }

    public void Stop(){
        audioSource.Stop();
    }

    public void SetVolume(float volume){
        audioSource.volume = volume;
    }

    public float GetVolume(){
        Debug.Log(audioSource.volume);
        return audioSource.volume;
    }

    public void PlayWhen(string clipName, bool condition){
        if(condition){
            if(IsNotPlaying()){
                SetVolume(1);
                Play(clipName.ToLower());
            }
        }else{
            Stop();
        }
    }

    private Dictionary<string, AudioClip> Zip(){
        var zipped = audioClipNames.Aggregate(new Dictionary<string, AudioClip>(), (dictionary, name) => {
            int index = dictionary.Count;
            dictionary.Add(name.ToLower(), audioClipFiles[index]);
            return dictionary;
        });
        return zipped;
    }
}
