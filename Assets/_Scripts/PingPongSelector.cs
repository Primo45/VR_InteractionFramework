using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PingPongSelector : MonoBehaviour
{
    public enum PingPongColor
    {
        None,
        White,       // Normal
        LightGreen,  // At Risk
        DarkGreen,   // Stage 0 (Controlled)
        Yellow,      // Stage 1 (Mild)
        Orange,      // Stage 2 (Moderate / High)
        Red,         // Stage 3 (Critical)
        Black        // Complications
    }

    [Header("UI References")]
    [Tooltip("Text component for displaying color category title")]
    public TextMeshProUGUI titleText;

    [Tooltip("Text component for displaying detailed description")]
    public TextMeshProUGUI descriptionText;

    [Tooltip("Optional panel container for the description")]
    public GameObject descriptionPanel;

    [Header("Audio Settings")]
    public AudioSource clickAudioSource;
    public float soundDelay = 0.2f;

    [Header("Settings")]
    public PingPongColor correctColor = PingPongColor.LightGreen;
    public PingPongColor selectedColor = PingPongColor.None;

    private Dictionary<PingPongColor, (string title, string hexColor, string description)> colorData;

    private void Awake()
    {
        InitializeColorData();
    }

    private void Start()
    {
        ShowDefaultPrompt();
    }

    private void InitializeColorData()
    {
        colorData = new Dictionary<PingPongColor, (string, string, string)>()
        {
            {
                PingPongColor.White,
                (
                    "WHITE : Normal Group",
                    "#FFFFFF",
                    "<b>Criteria:</b> FBS < 100 mg/dL | Blood Pressure < 120/80 mmHg\n" +
                    "<b>Status:</b> Normal health parameters. No chronic disease risk.\n" +
                    "<b>Action:</b> Maintain healthy lifestyle, balanced nutrition, regular exercise, and annual check-ups."
                )
            },
            {
                PingPongColor.LightGreen,
                (
                    "LIGHT GREEN : At-Risk Group",
                    "#88FF88",
                    "<b>Criteria:</b> FBS 100 - 125 mg/dL | Blood Pressure 120-139 / 80-89 mmHg\n" +
                    "<b>Status:</b> Pre-diabetes or pre-hypertension state (high risk of developing chronic disease).\n" +
                    "<b>Action:</b> Intensive lifestyle modification (3E 3R: diet control, exercise, stress management)."
                )
            },
            {
                PingPongColor.DarkGreen,
                (
                    "DARK GREEN : Stage 0 - Well Controlled",
                    "#33DD55",
                    "<b>Criteria:</b> FBS < 126 mg/dL | Blood Pressure < 140/90 mmHg (with medication)\n" +
                    "<b>Status:</b> Diagnosed chronic patient with well-controlled levels and no organ complications.\n" +
                    "<b>Action:</b> Continue prescribed medical treatment, healthy habits, and routine check-ups."
                )
            },
            {
                PingPongColor.Yellow,
                (
                    "YELLOW : Stage 1 - Mild Risk",
                    "#FFEE33",
                    "<b>Criteria:</b> FBS 126 - 154 mg/dL | Blood Pressure 140-159 / 90-99 mmHg\n" +
                    "<b>Status:</b> Moderately controlled chronic illness. Early risk of developing complications.\n" +
                    "<b>Action:</b> Re-evaluate medication regimen, enforce dietary compliance, and follow up every 1-2 months."
                )
            },
            {
                PingPongColor.Orange,
                (
                    "ORANGE : Stage 2 - High Risk",
                    "#FFAA22",
                    "<b>Criteria:</b> FBS 155 - 182 mg/dL | Blood Pressure 160-179 / 100-109 mmHg\n" +
                    "<b>Status:</b> Uncontrolled chronic disease with high risk of target organ damage.\n" +
                    "<b>Action:</b> Strict medical intervention, medication adjustment, and close clinical monitoring."
                )
            },
            {
                PingPongColor.Red,
                (
                    "RED : Stage 3 - Severe / Critical",
                    "#FF4444",
                    "<b>Criteria:</b> FBS >= 183 mg/dL | Blood Pressure >= 180/110 mmHg\n" +
                    "<b>Status:</b> Dangerously high levels with imminent risk of acute cardiovascular events or stroke.\n" +
                    "<b>Action:</b> Immediate medical attention, urgent specialist care, and aggressive symptom control."
                )
            },
            {
                PingPongColor.Black,
                (
                    "BLACK : Complications Group",
                    "#B0B0B0",
                    "<b>Criteria:</b> Established Chronic Complications (e.g. Kidney Disease, Retinopathy, Stroke, Foot Ulcers)\n" +
                    "<b>Status:</b> Patients with established chronic organ damage or vascular complications.\n" +
                    "<b>Action:</b> Specialized multidisciplinary care, complication management, and disability prevention."
                )
            }
        };
    }

    public void ShowDefaultPrompt()
    {
        if (titleText != null)
        {
            titleText.text = "<color=#AAAAAA><b>[ SELECT A COLOR BALL ]</b></color>";
        }

        if (descriptionText != null)
        {
            descriptionText.text = "Click on any color ball above to view its category criteria, clinical status, and health advice.";
        }
    }

    public void SelectColor(string colorName)
    {
        if (System.Enum.TryParse(colorName, out PingPongColor color))
        {
            SelectColor(color);
        }
        else
        {
            Debug.LogWarning("PingPongSelector: Unknown color name: " + colorName);
        }
    }

    public void SelectColor(PingPongColor color)
    {
        selectedColor = color;

        if (clickAudioSource != null)
        {
            clickAudioSource.Play();
        }

        if (colorData == null)
        {
            InitializeColorData();
        }

        if (colorData.TryGetValue(color, out var data))
        {
            if (titleText != null)
            {
                titleText.text = $"<color={data.hexColor}><b>{data.title}</b></color>";
            }

            if (descriptionText != null)
            {
                descriptionText.text = data.description;
            }

            if (descriptionPanel != null)
            {
                descriptionPanel.SetActive(true);
            }
        }
    }

    // Button click helper methods for Unity UI Button Inspector
    public void PickWhite() => SelectColor(PingPongColor.White);
    public void PickLightGreen() => SelectColor(PingPongColor.LightGreen);
    public void PickDarkGreen() => SelectColor(PingPongColor.DarkGreen);
    public void PickYellow() => SelectColor(PingPongColor.Yellow);
    public void PickOrange() => SelectColor(PingPongColor.Orange);
    public void PickRed() => SelectColor(PingPongColor.Red);
    public void PickBlack() => SelectColor(PingPongColor.Black);

    public void ConfirmSelection()
    {
        bool isCorrect = (selectedColor == correctColor);
        StartCoroutine(PlaySoundAndCheckResult(isCorrect));
    }

    private IEnumerator PlaySoundAndCheckResult(bool isCorrect)
    {
        if (clickAudioSource != null)
        {
            clickAudioSource.Play();
            yield return new WaitForSeconds(soundDelay);
        }

        if (isCorrect)
        {
            Debug.Log("Correct selection! Navigating to ResultSuccess.");
            SceneManager.LoadScene("Scene_09_ResultSuccess");
        }
        else
        {
            Debug.Log("Incorrect selection! Navigating to ResultFail.");
            SceneManager.LoadScene("Scene_08_ResultFail");
        }
    }
}
