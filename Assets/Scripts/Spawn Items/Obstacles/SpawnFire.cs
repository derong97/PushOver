public class SpawnFire : TimedSpawn
{
    private void Awake()
    {
        minRespawnSeconds = 4f;
        maxRespawnSeconds = 10f;
        effectSeconds = 7f;
    }
}