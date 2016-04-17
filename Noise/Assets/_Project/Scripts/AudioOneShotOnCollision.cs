using UnityEngine;
using System.Collections;

/// <summary>
/// Emits a one shot audio clip on collision
/// </summary>

[RequireComponent(typeof(AudioSource))]
public class AudioOneShotOnCollision : MonoBehaviour
{    
	AudioSource m_AudioSource;          // Audio source to emit from
	public AudioClip[] m_AudioClips;    // Audio clip array, chooses randomly from clips

	void Start () 
	{
		m_AudioSource = GetComponent<AudioSource>();    // Get the audio source component
	}

	void OnCollisionEnter(Collision collision)
	{
		AudioClip clip = m_AudioClips[ Random.Range(0, m_AudioClips.Length - 1 ) ]; // Choose random clip
		m_AudioSource.PlayOneShot( clip );  // Play audio clip
	}
}
