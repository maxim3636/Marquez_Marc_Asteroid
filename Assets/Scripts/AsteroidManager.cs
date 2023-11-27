using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class AsteroidManager : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; // Prefabs dels asteroides de diferents mides
    public int currentWave = 1; // Ronda actual
    public GameManager gm;

    void Start()
    {
        InvokeRepeating("SpawnAsteroids", 5f, 10f); // Generar asteroides cada X
        InvokeRepeating("IncreaseWave", 10f, 10f);
    }
    void IncreaseWave()
    {
        currentWave++;
    }

    // Funció per generar asteroides basats en la ronda actual
    public void SpawnAsteroids()
    {
        if (gm.lives > 0)
        {
            float numAsteroids = currentWave * 1.15f; // Més asteroides per cada ronda
            int maxSizeIndex;

            // Restriccions de mides d'asteroides segons les rondes
            if (currentWave <= 2)
            {
                maxSizeIndex = 0; // Només petits
            }
            else if (currentWave <= 4)
            {
                maxSizeIndex = 1; // Petits i mitjans
            }
            else
            {
                maxSizeIndex = Mathf.Min(asteroidPrefabs.Length - 1, currentWave - 1); // Totes les mides disponibles
            }

            for (int i = 0; i < numAsteroids; i++)
            {
                Vector2 spawnPosition = GetRandomSpawnPosition();
                int sizeIndex = Random.Range(0, maxSizeIndex + 1);
                GameObject asteroid = Instantiate(asteroidPrefabs[sizeIndex], spawnPosition, Quaternion.identity);
                Vector2 randomDirection = GetRandomDirection(spawnPosition);
                asteroid.GetComponent<Rigidbody2D>().velocity = randomDirection;
            }
        }
    }
    
    
    // Funció per generar una posició aleatòria fora de l'escena dins d'un cercle
    private Vector2 GetRandomSpawnPosition()
    {
        float spawnRadius = 15f; // Radi suficientment gran per estar fora de l'escena
        Vector2 spawnPosition;

        do
        {
            spawnPosition = Random.insideUnitCircle * spawnRadius;
        } while (IsInsideScreen(spawnPosition));

        return spawnPosition;
    }

// Funció per comprovar si una posició està dins de la pantalla
    private bool IsInsideScreen(Vector2 position)
    {
        Camera mainCamera = Camera.main;
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }

    // Funció per obtenir una direcció aleatòria amb petites variacions
    private Vector2 GetRandomDirection(Vector2 spawnPosition)
    {
        Vector2 directionTowardsCenter = -spawnPosition.normalized; // Direcció cap al centre
        float angleVariation = 10f; // Ajusta la variació de l'angle segons la teva preferència

        // Aplica petites variacions a la direcció cap al centre
        Vector2 randomDirection = Quaternion.Euler(0, 0, Random.Range(-angleVariation, angleVariation)) * directionTowardsCenter;
        return randomDirection;
    }
}
