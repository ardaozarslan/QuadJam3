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

	public void PlayNarratorAudio(int index, float pitchCenter = 1f)
	{
		GameObject newNarratorAudioSource = new GameObject("NarratorAudioSource");
		newNarratorAudioSource.transform.parent = Player.Instance.playerAudioSource.transform;
		newNarratorAudioSource.transform.position = Player.Instance.playerAudioSource.transform.position;
		newNarratorAudioSource.AddComponent<AudioSource>();
		newNarratorAudioSource.GetComponent<AudioSource>().clip = narratorAudioClips[index];
		newNarratorAudioSource.GetComponent<AudioSource>().pitch = Random.Range(pitchCenter - 0.1f, pitchCenter + 0.1f);
		// newNarratorAudioSource.AddComponent<AudioEchoFilter>();
		// newNarratorAudioSource.GetComponent<AudioEchoFilter>().delay = 10;
		// newNarratorAudioSource.GetComponent<AudioEchoFilter>().decayRatio = 0.88f;
		// newNarratorAudioSource.GetComponent<AudioEchoFilter>().wetMix = 0.16f;
		// newNarratorAudioSource.GetComponent<AudioEchoFilter>().dryMix = 0.85f;
		newNarratorAudioSource.GetComponent<AudioSource>().Play();
		Destroy(newNarratorAudioSource, 1f);
	}

	public void PlayRandomNarratorAudio(float pitchCenter = 1f)
	{
		PlayNarratorAudio(Random.Range(0, narratorAudioClips.Count), pitchCenter);
	}
}
