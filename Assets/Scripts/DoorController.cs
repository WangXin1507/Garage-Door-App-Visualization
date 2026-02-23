using System.Collections;
using TMPro;
using UnityEngine;


public class DoorController : MonoBehaviour
{
    [SerializeField] Transform door;

    [SerializeField] Vector3 openOffset = new(0, 2.5f, 0);
    [SerializeField] float movementDuration = 3.0f;
    [SerializeField] TextMeshProUGUI statusText;


    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isDoorOpen = false;
    private Coroutine openDoorCoroutine;

    void Awake()
    {
        closedPosition = door.localPosition;
        openPosition = closedPosition + openOffset;
    }


    public void ToggleDoor()
    {
        if (openDoorCoroutine != null) StopCoroutine(openDoorCoroutine);

        isDoorOpen = !isDoorOpen;
        Vector3 targetPosition = isDoorOpen ? openPosition : closedPosition;
        string status = isDoorOpen ? "Status: OPENING..." : "Status: CLOSING";

        openDoorCoroutine = StartCoroutine(OperateMotor(targetPosition, status));
    }

    private IEnumerator OperateMotor(Vector3 targetPos, string status)
    {
        UpdateStatusText(status, Color.yellow);

        Vector3 startPos = door.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < movementDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / movementDuration;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            door.localPosition = Vector3.Lerp(startPos, targetPos, smoothT);
            yield return null;
        }
        door.localPosition = targetPos;

        string statusComplete = isDoorOpen ? "Status: OPEN" : "Status: CLOSED";
        Color statusColor = isDoorOpen ? Color.green : Color.red;
        UpdateStatusText(statusComplete, statusColor);
    }

    private void UpdateStatusText(string text, Color c)
    {
        statusText.text = text;
        statusText.color = c;
    }
}