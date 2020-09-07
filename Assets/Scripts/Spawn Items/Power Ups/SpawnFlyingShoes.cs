public class SpawnFlyingShoes : TimedSpawn
{
    // Flying Shoes -> Speed Up
    private void Awake()
    {
        minRespawnSeconds = 3f;
        maxRespawnSeconds = 20f;
        effectSeconds = float.MaxValue;
    }
}