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

    // Variáveis para suavização da transição
    private Transform previousTarget;
    private float transitionProgress = 0f;
    private float transitionDuration = 1f; // Duração da transição em segundos
    private bool isTransitioning = false;

    private Vector3 transitionStartPosition;
    private Quaternion transitionStartRotation;

    public Transform Target => target;

    private void Update()
    {
        // Verifica as condições para trocar o alvo
        Transform desiredTarget = GetDesiredTarget();

        if (desiredTarget != target && desiredTarget != null)
        {
            StartTransition(desiredTarget);
        }

        // Atualiza a transição se estiver em andamento
        if (isTransitioning)
        {
            UpdateTransition();
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Se não estiver em transição, segue o alvo normalmente
        if (!isTransitioning)
        {
            FollowTarget();
        }
    }

    private Transform GetDesiredTarget()
    {
        if (isSeeking && !isHiding && targetSeek != null)
        {
            return targetSeek;
        }
        else if (!isSeeking && isHiding && targetHide != null)
        {
            return targetHide;
        }
        return target;
    }

    private void StartTransition(Transform newTarget)
    {
        previousTarget = target;
        target = newTarget;

        transitionStartPosition = transform.position;
        transitionStartRotation = transform.rotation;

        transitionProgress = 0f;
        isTransitioning = true;
    }

    private void UpdateTransition()
    {
        transitionProgress += Time.deltaTime / transitionDuration;

        if (transitionProgress >= 1f)
        {
            // Transição concluída
            transitionProgress = 1f;
            isTransitioning = false;
        }

        // Interpolação suave da posição e rotação
        Vector3 targetPosition = target.position + offset;
        Quaternion targetRotation = lookAtTarget ?
            Quaternion.LookRotation(target.position - transform.position) :
            transform.rotation;

        // Interpolação suave usando SmoothStep para easing
        float smoothProgress = Mathf.SmoothStep(0f, 1f, transitionProgress);

        transform.position = Vector3.Lerp(transitionStartPosition, targetPosition, smoothProgress);

        if (lookAtTarget)
        {
            transform.rotation = Quaternion.Slerp(transitionStartRotation, targetRotation, smoothProgress);
        }
    }

    private void FollowTarget()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;

        if (lookAtTarget)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        StartTransition(newTarget);
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

    // Método para configurar a duração da transição
    public void SetTransitionDuration(float duration)
    {
        transitionDuration = Mathf.Max(0.1f, duration);
    }

    // Método para forçar o término da transição
    public void CompleteTransition()
    {
        if (isTransitioning)
        {
            transitionProgress = 1f;
            isTransitioning = false;
            FollowTarget(); // Garante que está na posição final exata
        }
    }
}