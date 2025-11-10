using UnityEngine;
using Unity.Cinemachine;  // Cinemachine 3 전용

public class CameraSwitchTrigger : MonoBehaviour
{
    [Header("Cameras")]
    public CinemachineCamera playerCamera;
    public CinemachineCamera bossCamera;

    [Header("Priority Settings")]
    public int activePriority = 11;
    public int inactivePriority = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossCamera.Priority = activePriority;
            playerCamera.Priority = inactivePriority;
            Debug.Log("🎥 Boss Camera ON");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossCamera.Priority = inactivePriority;
            playerCamera.Priority = activePriority;
            Debug.Log("🎥 Player Camera ON");
        }
    }
}
