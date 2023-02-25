using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // O objeto que a câmera deve seguir
    [SerializeField] private float smoothSpeed = 0.125f; // A suavidade com que a câmera deve se mover para a posição desejada
    private Vector3 offset; // A distância entre a câmera e o objeto seguido

    private void Start()
    {
        offset = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // Calcula a posição desejada para a câmera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Suaviza o movimento da câmera
        transform.position = smoothedPosition; // Move a câmera para a posição suavizada
    }
}
