using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // โหลดเข้าหน้า Login (Index 1)
    public void GoToLogin()
    {
        SceneManager.LoadScene("Scene_01_Login");
    }

    // โหลดเข้าหน้า Register (Index 2)
    public void GoToRegister()
    {
        SceneManager.LoadScene("Scene_02_Register");
    }

    // ย้อนกลับมาหน้า Launcher (Index 0)
    public void GoToLauncher()
    {
        SceneManager.LoadScene("Scene_00_Launcher");
    }

    // โหลดตามชื่อซีนที่พิมพ์ใส่ใน Unity Event inspector
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ปิดเกม
    public void QuitGame()
    {
        Application.Quit();
    }
}