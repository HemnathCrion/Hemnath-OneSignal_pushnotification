using System;
using UnityEngine;
using OneSignalSDK;
using OneSignalSDK.Notifications;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class PushNotification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_statusText;
    [SerializeField] private TextMeshProUGUI m_popupText;

    [SerializeField] private GameObject m_poupPanel;
    [SerializeField] private GameObject m_homePanel;
    [SerializeField] private Button m_popupButton;
    [SerializeField] private Button m_backButton;
    

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private async void Start()
    {
        m_poupPanel.SetActive(false);
        m_statusText.text = "Starting...";
        OneSignal.Debug.LogLevel = OneSignalSDK.Debug.Models.LogLevel.Verbose;

        // Initialize OneSignal
        OneSignal.Initialize("3eb4f1e9-37a9-460b-9e02-923205fb3ae1");
        m_statusText.text = "Ready...";

        // Ask for push permission (iOS only, Android auto-grants)
        await OneSignal.Notifications.RequestPermissionAsync(true);
        m_statusText.text = "Permission granted";

        OneSignal.Notifications.Clicked += OnNotificationClicked;
    }


    private void OnDisable()
    {
        m_poupPanel.SetActive(false);
        m_statusText.enabled = false;
        m_homePanel.SetActive(false);
    }

    private void OnNotificationClicked(object sender, NotificationClickEventArgs e)
    {
        if (e.Notification.AdditionalData.ContainsKey("MainScene"))
        {
            string sceneName = e.Notification.AdditionalData["MainScene"].ToString();
            Debug.Log("Opening scene: " + sceneName);
            m_statusText.text = "Opening scene: " + sceneName;

            if (Application.CanStreamedLevelBeLoaded(sceneName) && sceneName == "Log")
            {
                m_poupPanel.SetActive(true);
                m_popupText.text = "OK Click to open your log list and review the latest system records.";

                m_popupButton.onClick.AddListener(() =>
                {
                    OnDisable();
                    SceneManager.LoadScene(sceneName);
                });
                m_backButton.onClick.AddListener(() =>
                {
                    SceneManager.LoadScene("MainScene");
                });
            }
            else
            {
                m_poupPanel.SetActive(true);
                m_popupText.text = "OK Click to open your work order list and check the latest updates.";

                m_popupButton.onClick.AddListener(() =>
                {
                    OnDisable();
                    SceneManager.LoadScene(sceneName);
                });
                m_backButton.onClick.AddListener(() =>
                {
                    SceneManager.LoadScene("MainScene");
                });
            }
        }
    }
}