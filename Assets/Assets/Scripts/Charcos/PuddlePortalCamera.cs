using UnityEngine;

/// <summary>
/// PuddlePortalCamera
/// ─────────────────────────────────────────────────────────────────────────────
/// La rotación de la cámara destino se calcula a partir del offset XZ del
/// jugador respecto al CENTRO DEL CHARCO, nunca desde la posición 3D de la
/// cámara. Esto evita el flip de 180° al cruzar el centro.
///
/// LÓGICA:
///   · Centro del charco (offset XZ = 0,0)  →  pitch  90° (mirar recto arriba)
///   · Jugador se desplaza en XZ            →  el pitch baja y el yaw apunta
///                                             hacia donde está el jugador
///   · La inclinación es proporcional a la distancia / radio del trigger
///
///   Fórmula:
///     yaw   = atan2(offsetX, offsetZ)          ← dirección del offset en XZ
///     t     = clamp01(distancia / radio)        ← 0=centro, 1=borde
///     pitch = lerp(90°, pitchAtEdge, t)         ← 90° en centro, baja al borde
///
/// SETUP:
///   1. Crea una RenderTexture → Output Texture de esta cámara.
///   2. Material del Quad del charco usa esa RenderTexture.
///   3. PuddleTrigger llama a SetTarget / ClearTarget.
///   4. Asigna el Transform del centro del charco a puddleCenter
///      (puede ser el mismo GameObject del Quad).
/// ─────────────────────────────────────────────────────────────────────────────
/// </summary>
[RequireComponent(typeof(Camera))]
public class PuddlePortalCamera : MonoBehaviour
{
    [Header("Referencia al charco")]
    [Tooltip("Transform del centro del charco (el Quad o un GameObject vacío en su centro)")]
    public Transform puddleCenter;

    [Tooltip("Radio del trigger del charco. Úsalo para normalizar la distancia.")]
    public float puddleRadius = 1f;

    [Header("Ángulos")]
    [Tooltip("Pitch cuando el jugador está en el borde del trigger (distancia = radio).")]
    [Range(-90f, 90f)]
    public float pitchAtEdge = 0f;

    [Header("Offset de sala")]
    [Tooltip("Offset de Yaw si las dos salas no están alineadas en Y.")]
    public float roomYawOffset = 0f;

    [Header("Suavizado")]
    [Tooltip("Velocidad de Slerp. 0 = instantáneo.")]
    [Range(0f, 20f)]
    public float smoothSpeed = 0f;

    // ─── estado interno ──────────────────────────────────────────────────────
    private Transform _target;
    private bool _hasTarget;

    // ─── API pública ─────────────────────────────────────────────────────────

    public void SetTarget(Transform playerTransform)
    {
        _target = playerTransform;
        _hasTarget = true;
    }

    public void ClearTarget()
    {
        _target = null;
        _hasTarget = false;
        // La rotación queda congelada en el último valor calculado.
    }

    // ─── update ──────────────────────────────────────────────────────────────

    private void LateUpdate()
    {
        if (!_hasTarget || _target == null || puddleCenter == null) return;

        Quaternion targetRot = CalculateRotation();

        if (smoothSpeed <= 0f)
            transform.rotation = targetRot;
        else
            transform.rotation = Quaternion.Slerp(
                transform.rotation, targetRot, Time.deltaTime * smoothSpeed);
    }

    // ─── cálculo ─────────────────────────────────────────────────────────────

    private Quaternion CalculateRotation()
    {
        // Offset del jugador en XZ respecto al centro del charco
        Vector3 playerPos = _target.position;
        Vector3 centerPos = puddleCenter.position;

        float offsetX = playerPos.x - centerPos.x;
        float offsetZ = playerPos.z - centerPos.z;
        float distance = Mathf.Sqrt(offsetX * offsetX + offsetZ * offsetZ);

        // ── Yaw: dirección del offset en el plano XZ ──────────────────────────
        // atan2(x, z) da el ángulo respecto al eje Z (norte), igual que Unity.
        float yaw = Mathf.Atan2(offsetX, offsetZ) * Mathf.Rad2Deg + roomYawOffset;

        // ── Pitch: 90° en el centro, baja hacia pitchAtEdge al alejarse ───────
        float t = (puddleRadius > 0f)
                      ? Mathf.Clamp01(distance / puddleRadius)
                      : 0f;
        float pitch = Mathf.Lerp(90f, pitchAtEdge, t);

        // Pitch positivo en Unity = mirar hacia abajo desde el punto de vista
        // de la cámara, pero aquí lo usamos como ángulo absoluto de rotación X.
        // 90° = mirar recto arriba  →  en Euler X de Unity eso es -90°
        // Por eso negamos: Euler(-pitch, yaw, 0)
        return Quaternion.Euler(-pitch, yaw, 0f);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // Dirección de vista de la cámara
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, transform.forward * 2f);

        // Línea al target si existe
        if (_hasTarget && _target != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, _target.position);
            Gizmos.DrawWireSphere(_target.position, 0.08f);
        }

        // Radio del charco
        if (puddleCenter != null)
        {
            Gizmos.color = new Color(1f, 1f, 0f, 0.4f);
            Gizmos.DrawWireSphere(puddleCenter.position, puddleRadius);
        }
    }
#endif
}