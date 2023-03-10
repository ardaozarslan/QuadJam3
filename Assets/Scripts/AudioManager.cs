using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AudioManager : Singleton<AudioManager>
{
	[ReorderableList]
	public List<AudioClip> narratorAudioClips = new List<AudioClip>();
	public AudioSource narratorAudioSource;

	private void Start() {
		narratorAudioSource = Player.Instance.playerAudioSource.GetComponent<AudioSource>();
	}

	public void PlayNarratorAudio(int index)
	{
		GameObject newNarratorAudioSource = new GameObject("NarratorAudioSource");
		newNarratorAudioSource.transform.parent = Player.Instance.playerAudioSource.transform;
		newNarratorAudioSource.transform.position = Player.Instance.playerAudioSource.transform.position;
		newNarratorAudioSource.AddComponent<AudioSource>();
		newNarratorAudioSource.GetComponent<AudioSource>().clip = narratorAudioClips[index];
		newNarratorAudioSource.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
		newNarratorAudioSource.GetComponent<AudioSource>().Play();
		Destroy(newNarratorAudioSource, 1f);
	}

	public void PlayRandomNarratorAudio()
	{
		PlayNarratorAudio(Random.Range(0, narratorAudioClips.Count));
	}
}
