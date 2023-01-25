using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinSound;
    [SerializeField] float soundVol = 0.15f;
    GameObject audioListener;

    private void Start()
    {
        audioListener = GameObject.FindWithTag("AudioListener");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            ProcessCoinPickup();
        }
    }

    private void ProcessCoinPickup()
    {
        //TODO add coin value to score
        audioListener.transform.position = transform.position;
        AudioSource.PlayClipAtPoint(coinSound, audioListener.transform.position, soundVol);
        FindObjectOfType<GameSession>().AddToCoins();
        Destroy(gameObject);
    }
}
