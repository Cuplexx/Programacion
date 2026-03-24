using UnityEngine;

/// <summary>
/// PuddlePortalCamera
/// ─────────────────────────────────────────────────────────────────────────────
/// Coloca este script en la cámara de la sala destino (la que renderiza
/// a la RenderTexture del charco).
///
/// CONCEPTO:
///   El charco es un portal en el suelo. La cámara destino debe mostrar la sala
///   como si el jugador "cayera" a través del suelo y mirara desde abajo.
///
///   Mapeo de rotación:
///     · Yaw  (Y) del jugador  →  Yaw (Y) de la cámara destino + 180°
///           (giras a la derecha, la vista del charco también gira, pero opuesta
///            porque estás mirando "desde el otro lado")
///     · Pitch (X) del jugador →  Se refleja respecto al plano horizontal:
///           pitchDestino = -pitchJugador
///           Si miras al frente (0°)  → cámara destino mira al frente (0°)  *desde abajo*
///           Si miras abajo  (-70°)   → cámara destino mira arriba (+70°)
///           Si miras arriba (+30°)   → cámara destino mira abajo  (-30°)
///     · Roll  siempre 0.
///
/// SETUP:
///   1. Crea una RenderTexture y asígnala a la cámara destino (Output Texture).
///   2. Crea un material con la RenderTexture y aplícalo al Quad del charco.
///   3. Arrastra la Transform del jugador (o de su cámara FPS) a playerCamera.
///   4. El GameObject de este script NO se mueve; solo rota.
/// ─────────────────────────────────────────────────────────────────────────────
/// </summary>
[RequireComponent(typeof(Camera))]
public class PuddlePortalCamera : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Transform de la cámara FPS del jugador")]
    public Transform playerCamera;

    [Header("Offset de la sala destino")]
    [Tooltip("Rotación Y base de la sala destino respecto a la sala origen. " +
             "Deja en 0 si ambas salas están alineadas en el mismo eje.")]
    public float roomYawOffset = 0f;

    [Header("Opciones")]
    [Tooltip("Si la sala destino está 'boca abajo' respecto a la origen, activa esto.")]
    public bool flipPitch = true;

    // ─── internals ───────────────────────────────────────────────────────────
    private Camera _cam;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (playerCamera == null) return;
        UpdatePortalCamera();
    }

    private void UpdatePortalCamera()
    {
        // ── 1. Extraer ángulos Euler del jugador ──────────────────────────────
        //    Usamos eulerAngles para obtener pitch y yaw limpios.
        //    Unity devuelve X en [0, 360); lo convertimos a [-180, 180].
        Vector3 playerEuler = playerCamera.eulerAngles;

        float playerYaw = playerEuler.y;
        float playerPitch = NormalizeAngle(playerEuler.x); // negativo = mirar arriba en FPS

        // ── 2. Calcular rotación destino ──────────────────────────────────────
        //    Yaw: mismo eje de giro + 180° (estamos "debajo" del plano)
        //    Pitch: invertido (espejo respecto al horizonte)
        float destYaw = playerYaw + 180f + roomYawOffset;
        float destPitch = flipPitch ? -playerPitch : playerPitch;

        // ── 3. Aplicar ────────────────────────────────────────────────────────
        transform.rotation = Quaternion.Euler(destPitch, destYaw, 0f);
    }

    /// <summary>
    /// Convierte un ángulo [0, 360) a [-180, 180].
    /// Necesario porque Unity devuelve pitch como 350° en lugar de -10°.
    /// </summary>
    private static float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        return angle;
    }

#if UNITY_EDITOR
    // Gizmo opcional: dibuja la dirección de vista en Scene View
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.up * 0.5f);
    }
#endif
}