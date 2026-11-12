using UnityEngine;
using UnityEngine.Events;

public class PingPongJarTrigger : MonoBehaviour
{
    public enum JarColor
    {
        White,      // สีขาว (ปกติ)
        LightGreen, // สีเขียวอ่อน (กลุ่มเสี่ยง)
        DarkGreen,  // สีเขียวเข้ม (ป่วยระดับ 0)
        Yellow,     // สีเหลือง (ป่วยระดับ 1)
        Orange,     // สีส้ม (ป่วยระดับ 2)
        Red,        // สีแดง (ป่วยระดับ 3)
        Black       // สีดำ (ป่วยรุนแรง/โรคแทรกซ้อน)
    }

    [Header("ตั้งค่าสีของโถใบนี้")]
    public JarColor thisJarColor;

    [Header("สีที่ถูกต้องสำหรับเคสคนไข้นี้")]
    public JarColor correctAnswerColor;

    [Header("Event เมื่อหย่อนลูกปิงปองลงโถ")]
    public UnityEvent onCorrectJar;
    public UnityEvent onWrongJar;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // เช็คว่าเป็นลูกปิงปองและยังไม่เคยตรวจผลมาก่อน
        if (!hasTriggered && other.CompareTag("PingPongBall"))
        {
            hasTriggered = true;

            if (thisJarColor == correctAnswerColor)
            {
                Debug.Log("ถูกต้อง! หย่อนลงโถสี: " + thisJarColor);
                onCorrectJar.Invoke();
            }
            else
            {
                Debug.Log("ผิด! หย่อนลงโถสี: " + thisJarColor + " (คำตอบที่ถูกคือ " + correctAnswerColor + ")");
                onWrongJar.Invoke();
            }
        }
    }
}