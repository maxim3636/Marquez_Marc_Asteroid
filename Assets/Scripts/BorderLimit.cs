using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderLimit : MonoBehaviour
{
    public enum BorderType { Top, Bottom, Left, Right }

    public BorderType borderType;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Teleporta el jugador a la part interna del borde
            TeleportPlayerInsideBorder(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Destrueix les bales quan surten dels límits
            Destroy(other.gameObject);
        }
    }

    private void TeleportPlayerInsideBorder(GameObject player)
    {
        Vector3 newPosition = player.transform.position;

        switch (borderType)
        {
            case BorderType.Top:
                newPosition.y = transform.position.y - 8.8f; // Ajusta a la part interior de sota
                break;
            case BorderType.Bottom:
                newPosition.y = transform.position.y + 8.7f; // Ajusta a la part interior de dalt
                break;
            case BorderType.Left:
                newPosition.x = transform.position.x + 15.5f; // Ajusta a la part interior de la dreta
                break;
            case BorderType.Right:
                newPosition.x = transform.position.x - 15.5f; // Ajusta a la part interior de l'esquerra
                break;
        }

        player.transform.position = newPosition;
    }
}
