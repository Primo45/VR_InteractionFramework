using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultFailManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [Tooltip("ลาก SoundManager มาใส่")]
    public AudioSource clickSound;
    public float soundDelay = 0.2f;

    [Header("Scene Navigation")]
    [Tooltip("Scene สำหรับกดย้อนกลับไปลองทำใหม่")]
    public string retrySceneName = "Scene_06_SelectPingPong";
    [Tooltip("Scene สำหรับกลับสู่หน้าแรก")]
    public string menuSceneName = "Scene_01_Login";

    // ฟังก์ชันปุ่ม Retry
    public void OnClickRetry()
    {
        StartCoroutine(PlaySoundAndLoad(retrySceneName));
    }

    // ฟังก์ชันปุ่ม Main Menu
    public void OnClickMainMenu()
    {
        StartCoroutine(PlaySoundAndLoad(menuSceneName));
    }

    private IEnumerator PlaySoundAndLoad(string targetScene)
    {
        if (clickSound != null)
        {
            clickSound.Play();
        }

        yield return new WaitForSeconds(soundDelay);

        Debug.Log("กำลังโหลดฉาก: " + targetScene);
        SceneManager.LoadScene(targetScene);
    }
}