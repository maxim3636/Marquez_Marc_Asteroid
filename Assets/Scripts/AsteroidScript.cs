using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; // Prefabs dels asteroides de diferents mides
    public GameObject prefabExplosion;
    private bool hasExploded = false;
    void Start()
    {
        // Invoca la funció de destrucció amb un retard de 25 segons
        Invoke("DestroyAsteroid", 25f);
    }

    // Funció per destruir l'asteroide després de 25 segons
    void DestroyAsteroid()
    {
        Destroy(gameObject);
    }

    // Funció per gestionar les col·lisions amb les bales
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasExploded && collision.gameObject.CompareTag("Bullet"))
        {
            hasExploded = true; // Marca que ya ha explotado
            Destroy(collision.gameObject); // Destruye la bala

            // Comprova el tag i el tipus de l'asteroide
            if (gameObject.CompareTag("LargeAsteroid"))
            {
                SpawnAsteroids(2, 1); // Crea dos mitjans
            }
            else if (gameObject.CompareTag("MediumAsteroid"))
            {
                SpawnAsteroids(2, 0); // Crea dos petits
            }
            else if (gameObject.CompareTag("SmallAsteroid"))
            {
                Destroy(gameObject); // Destruye l'asteroide petit
            }
            
            // Instancia una sola explosión
            GameObject explosion = Instantiate(prefabExplosion, transform.position, Quaternion.identity);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration); // Destruye la explosión después de su duración
        }
    }

    // Funció per generar asteroides basats en la quantitat i mides donades
    void SpawnAsteroids(int numAsteroids, int sizeIndex)
    {
        for (int i = 0; i < numAsteroids; i++)
        {
            // Codi similar al de l'AsteroidManager per generar asteroides amb mides específiques
            // Utilitza la variable 'sizeIndex' per determinar la mida de l'asteroide a instanciar
            // ... (crida Instantiate, defineix posició, direcció, etc.)
            Vector2 spawnPosition = transform.position; // Utilitza la mateixa posició que l'asteroide original
            GameObject asteroid = Instantiate(asteroidPrefabs[sizeIndex], spawnPosition, Quaternion.identity);
            Vector2 randomDirection = GetRandomDirection(spawnPosition);
            asteroid.GetComponent<Rigidbody2D>().velocity = randomDirection;
        }
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
