using UnityEngine;

/// <summary>
/// PuddleTrigger
/// ─────────────────────────────────────────────────────────────────────────────
/// Coloca este script en el GameObject del charco (el que tiene el Collider
/// en modo Trigger). Cuando el jugador entra, manda su Transform a la cámara
/// del portal. Cuando sale, le dice que deje de seguirle.
///
/// SETUP:
///   1. El Collider del charco debe tener "Is Trigger" activado.
///   2. Arrastra la PuddlePortalCamera al campo portalCamera.
///   3. El jugador debe tener el tag "Player".
/// ─────────────────────────────────────────────────────────────────────────────
/// </summary>
public class PuddleTrigger : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("La cámara que renderiza la sala destino")]
    public PuddlePortalCamera portalCamera;

    [Tooltip("Tag del jugador")]
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;
        portalCamera.SetTarget(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;
        portalCamera.ClearTarget();
    }
}
