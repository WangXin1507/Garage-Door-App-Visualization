using UnityEngine;

public class TurnToFaceCamera : MonoBehaviour
{
    [SerializeField] private Transform faceTarget;

    void Awake()
    {
        if (!faceTarget) faceTarget = Camera.main.transform;
    }

    void Update()
    {
        if (!faceTarget) return;

        Vector3 dir = faceTarget.position - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(-dir);
    }
}
