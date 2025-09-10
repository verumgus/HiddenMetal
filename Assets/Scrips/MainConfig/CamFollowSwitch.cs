using UnityEngine;

public class CamFollowSwitch : MonoBehaviour
{
    public Transform target;
    public Transform targetSeek;
    public Transform targetHide;

    // Variáveis booleanas para controle
    public bool isSeeking = false;
    public bool isHiding = false;

    public Vector3 offset = new(0, 6, -9);
    public float smoothSpeed = 5f;
    public bool lookAtTarget = true;

    public Transform Target => target;

    private void Update()
    {
        // Verifica as condições para trocar o alvo
        if (isSeeking && !isHiding && targetSeek != null)
        {
            SetTarget(targetSeek);
        }
        else if (!isSeeking && isHiding && targetHide != null)
        {
            SetTarget(targetHide);
        }
        // Você pode adicionar outras condições aqui se necessário
    }

    private void LateUpdate()
    {
        if (target == null) { return; }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;

        if (lookAtTarget)
        {
            transform.LookAt(target);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetOffset(Vector3 newOffset)
    {
        offset = newOffset;
    }

    // Métodos para modificar as booleanas de outros scripts
    public void SetSeeking(bool seeking)
    {
        isSeeking = seeking;
    }

    public void SetHiding(bool hiding)
    {
        isHiding = hiding;
    }
}