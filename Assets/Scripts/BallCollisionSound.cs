using UnityEngine;

public class BallCollisionSound : MonoBehaviour
{
    public AudioClip collisionSound; 
    private AudioSource audioSource; 

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = collisionSound;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionSound != null)
        {
            audioSource.Play();
        }
    }
}