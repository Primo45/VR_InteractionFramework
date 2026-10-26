using UnityEngine;

public class PersistentSoundManager : MonoBehaviour
{
    public static PersistentSoundManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // สั่งไม่ให้ทำลายทิ้งเวลาเปลี่ยนฉาก
        }
        else
        {
            Destroy(gameObject);
        }
    }
}