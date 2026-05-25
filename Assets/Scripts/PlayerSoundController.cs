using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shootingBulletSound;
    public AudioClip shootingWebSound;
    public AudioClip playerDamagedSound;
    public AudioClip gameOverSound;

    // Has diferent AudioSource so it can play and stop when the duration ends
    public AudioSource jackpotAudioSource;

    public void PlayShootBullet()
    {
        audioSource.PlayOneShot(shootingBulletSound);
    }

    public void PlayShootWeb()
    {
        audioSource.PlayOneShot(shootingWebSound);
    }

    public void PlayJackpot()
    {
        jackpotAudioSource.Play();
    }

    public void StopJackpot()
    {
        jackpotAudioSource.Stop();
    }

    public void PlayPlayerDamaged()
    {
        audioSource.PlayOneShot(playerDamagedSound);
    }

    public void PlayGameOver()
    {
        audioSource.PlayOneShot(gameOverSound);
    }
}
