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
    [SerializeField] string currentClip;
    Dictionary<string, AudioClip> dictionary;
    bool isTransitioning = false;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        dictionary = Zip();
    }

    public void Play(string audioClipName, float duration = 1f){
        if(!isTransitioning){
            if(dictionary.ContainsKey(audioClipName)){
                AudioClip audioClipFile = dictionary[audioClipName];
                currentClip = audioClipName;
                audioSource.PlayOneShot(audioClipFile, duration);
            }else{
                Debug.Log($"audioClipName: {audioClipName} does not exist in the dictionary");
            }
        }
    }

    public bool IsPlaying(){
        return audioSource.isPlaying == true;
    }

    public string CurrentClip(){
        return currentClip;
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
        return audioSource.volume;
    }

    public void PlayWhen(string clipName, bool condition){
        if(!isTransitioning){
            if(condition){
                if(IsNotPlaying()){
                    SetVolume(1);
                    Play(clipName.ToLower());
                }
            }else{
                Stop();
            }
        }
    }

    public void startTransition(){
        isTransitioning = true;
    }

    public void stopTransition(){
        isTransitioning = false;
    }

    private Dictionary<string, AudioClip> Zip(){
        if(audioClipNames.Count ==  audioClipFiles.Count){
            var zipped = audioClipNames.Aggregate(new Dictionary<string, AudioClip>(), (dictionary, name) => {
                int index = dictionary.Count;
                dictionary.Add(name.ToLower(), audioClipFiles[index]);
                return dictionary;
            });
            return zipped;

        }
        else{
            Debug.Log("audioClipNames Count and audioClipFiles Counts are not the same.");
            return null;
        }
    }
}
