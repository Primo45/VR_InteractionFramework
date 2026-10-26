using UnityEngine;

public class GlobalSoundManager : MonoBehaviour
{
    public static GlobalSoundManager Instance;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip clickClip;

    private void Awake()
    {
        // ถ้ามีอยู่แล้วให้ทำลายตัวซ้ำ ถ้ายังไม่มีให้อยู่ข้ามซีน
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // ฟังก์ชันสำหรับเรียกเล่นเสียงคลิก
    public void PlayClickSound()
    {
        if (audioSource != null && clickClip != null)
        {
            audioSource.PlayOneShot(clickClip);
        }
    }
}