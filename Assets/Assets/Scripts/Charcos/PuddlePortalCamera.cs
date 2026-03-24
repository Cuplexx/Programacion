using UnityEngine;

/// <summary>
/// PuddlePortalCamera
/// ─────────────────────────────────────────────────────────────────────────────
/// Coloca este script en la cámara de la sala destino.
/// Cuando el jugador está dentro del trigger del charco, la cámara hace
/// LookAt hacia la posición del jugador. Al salir, se queda congelada
/// en la última rotación conocida.
///
/// SETUP:
///   1. Crea una RenderTexture → asígnala al Output Texture de esta cámara.
///   2. El material del Quad del charco usa esa RenderTexture.
///   3. El script PuddleTrigger del charco llama a SetTarget / ClearTarget.
/// ─────────────────────────────────────────────────────────────────────────────
/// </summary>
[RequireComponent(typeof(Camera))]
public class PuddlePortalCamera : MonoBehaviour
{
    [Header("Offset de sala")]
    [Tooltip("Desplazamiento de Yaw entre la sala origen y la destino. " +
             "Deja en 0 si ambas salas están alineadas.")]
    public float roomYawOffset = 0f;

    [Header("Suavizado")]
    [Tooltip("Velocidad de interpolación de la rotación. 0 = instantáneo.")]
    [Range(0f, 20f)]
    public float smoothSpeed = 0f;

    // ─── estado interno ──────────────────────────────────────────────────────
    private Transform _target;        // Transform del jugador (null si fuera)
    private bool      _hasTarget;

    // ─── API pública (llamada desde PuddleTrigger) ───────────────────────────

    public void SetTarget(Transform playerTransform)
    {
        _target    = playerTransform;
        _hasTarget = true;
    }

    public void ClearTarget()
    {
        // Congelamos: simplemente dejamos de actualizar.
        // La rotación actual queda guardada automáticamente en transform.rotation.
        _target    = null;
        _hasTarget = false;
    }

    // ─── actualización ───────────────────────────────────────────────────────

    private void LateUpdate()
    {
        if (!_hasTarget || _target == null) return;

        Vector3 targetPos = _target.position;
        Quaternion targetRot = LookAtWithYawOffset(targetPos);

        if (smoothSpeed <= 0f)
        {
            transform.rotation = targetRot;
        }
        else
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                Time.deltaTime * smoothSpeed
            );
        }
    }

    // ─── cálculo de rotación ─────────────────────────────────────────────────

    private Quaternion LookAtWithYawOffset(Vector3 targetWorldPos)
    {
        Quaternion look = Quaternion.LookRotation(targetWorldPos - transform.position);

        if (!Mathf.Approximately(roomYawOffset, 0f))
        {
            look = Quaternion.Euler(
                look.eulerAngles.x,
                look.eulerAngles.y + roomYawOffset,
                0f
            );
        }

        return look;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, transform.forward * 2f);

        if (_hasTarget && _target != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, _target.position);
            Gizmos.DrawWireSphere(_target.position, 0.1f);
        }
    }
#endif
}
