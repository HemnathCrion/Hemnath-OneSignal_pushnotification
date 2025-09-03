using UnityEngine;
using OneSignalSDK;
using OneSignalSDK.Debug.Models;
using TMPro;

public class PushNotification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText;

    async void Start()
    {
        // Enable verbose logging for debugging (remove in production)
        statusText.text = "Starting...";
        OneSignal.Debug.LogLevel = LogLevel.Verbose;
        // Initialize with your OneSignal App ID
        OneSignal.Initialize("3eb4f1e9-37a9-460b-9e02-923205fb3ae1");
        statusText.text = "Ready...";
        // Use this method to prompt for push notifications.
        // We recommend removing this method after testing and instead use In-App Messages to prompt for notification permission.
        await OneSignal.Notifications.RequestPermissionAsync(true);
        statusText.text = "Permission granted";
        Debug.Log(LogLevel.Verbose);
        }
}