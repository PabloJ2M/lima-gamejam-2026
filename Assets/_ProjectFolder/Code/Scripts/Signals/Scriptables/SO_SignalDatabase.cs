namespace UnityEngine.Gameplay
{
    [CreateAssetMenu(fileName = "signal pattern", menuName = "system/signals/database", order = 0)]
    public class SO_SignalDatabase : ScriptableObject
    {
        [SerializeField] private SO_SignalPattern[] _patterns;

        public SO_SignalPattern GetRandomPattern() => _patterns[Random.Range(0, _patterns.Length)];
    }
}