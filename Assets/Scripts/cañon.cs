using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // El prefab de la bala
    public Transform firePoint; // El punto desde donde se dispara la bala
    public float fireRate = 1.0f; // Intervalo entre disparos
    public float bulletSpeed = 10.0f; // Velocidad inicial de la bala
    public float raycastRange = 50.0f; // Rango del raycast
    public LayerMask targetLayer; // Capas a las que reaccionará el raycast

    private float nextFireTime = 0.0f; // Tiempo para el próximo disparo

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            if (Shoot()) // Disparar una bala si el raycast no golpea un obstáculo
            {
                nextFireTime = Time.time + fireRate; // Configurar el tiempo del próximo disparo
            }
        }
    }

    bool Shoot()
    {
        // Realizar el raycast
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, raycastRange, targetLayer))
        {
            // Si golpea un objetivo, podemos hacer algo aquí
            Debug.Log($"Hit {hit.collider.gameObject.name} at distance {hit.distance}");

            // Opcionalmente, puedes destruir el objetivo o realizar otra acción
            // Destroy(hit.collider.gameObject);

            return false; // No disparamos la bala porque golpeó algo
        }

        // Si no golpea nada, disparamos la bala
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed; // Mover la bala hacia adelante
        }

        return true; // Disparo exitoso
    }
}





