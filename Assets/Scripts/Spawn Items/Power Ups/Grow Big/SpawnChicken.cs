public class SpawnChicken : TimedSpawn
{
    // Chicken -> GrowBigger
    private void Awake()
    {
        minRespawnSeconds = 3f;
        maxRespawnSeconds = 10f;
        effectSeconds = float.MaxValue;
    }
}
