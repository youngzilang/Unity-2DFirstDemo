using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private float maxSoundDistance;
    [SerializeField] private AudioSource[] bgm;

    public bool play;
    private int index;


    private bool isDestroyed = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);

    }

    private void Update()
    {
        if (!play) StopAllBGM();
        else
        {
            if (!bgm[index].isPlaying) PlayBGM(index);
        }
    }

    private void OnDestroy()
    {
        // 销毁时标记状态，终止所有协程
        isDestroyed = true;
        StopAllCoroutines();
        // 清空单例引用
        if (instance == this)
        {
            instance = null;
        }
    }

    public void PlayRandomBGM()
    {
        index = Random.Range(0, bgm.Length);
        PlayBGM(index);
    }

    public void PlaySFX(int _index,Transform _source=null)
    { 
        if (sfx[_index].isPlaying) return;

        if (_source && Vector2.Distance(_source.position, PlayerManager.instance.player.transform.position) > maxSoundDistance) return;

        if (_index < sfx.Length)
        {
            sfx[_index].pitch = Random.Range(.8f, 1.2f);
            sfx[_index].Play();
        }
    }

    public void StopSFX(int _index) => sfx[_index].Stop();

    public void PlayBGM(int _index)
    {
        index = _index;

        StopAllBGM();
        if (index < bgm.Length) bgm[index].Play();
    }

    public void StopAllBGM()
    {
        for(int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }

    public void StopSFXWithTime(int _index) => StartCoroutine(DecreaseVolume(sfx[_index]));

    private IEnumerator DecreaseVolume(AudioSource _source)
    {
        float volume = _source.volume;

        while (_source.volume > 0.1)
        {
            _source.volume -= _source.volume * 0.2f;
            yield return new WaitForSeconds(.25f);

            if (_source.volume <= 0.1)
            {
                _source.Stop();
                _source.volume = volume;
                break;
            }
        }
    }

}
