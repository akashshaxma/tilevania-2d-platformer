using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForCoinPickup = 100;

    bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !wasCollected)
        {
            wasCollected = true;

            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);

            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);

            GetComponent<PersistentObject>().DestroyPersistentObject();
        }
    }
}