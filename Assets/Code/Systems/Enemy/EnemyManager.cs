using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour {
    [SerializeField] private EnemyBase[] enemies;

    private EnemyBase _currentEnemy;
    
    [SerializeField] private UnityEvent onEnemyFinished;
    
    private void Awake() {
        foreach(EnemyBase enemy in enemies) {
            enemy.gameObject.SetActive(false);
            enemy.OnEnemyFinished += OnEnemyFinished;
        }
    }

    public void SpawnEnemy(Paranoia paranoia) {
        _currentEnemy = GetEnemyForParanoia(paranoia);
        _currentEnemy.Activate();
    }

    public void ResolveMask(bool success) {
        _currentEnemy?.ResolveMask(success);
    }

    private void OnEnemyFinished(EnemyBase enemy) {
        _currentEnemy = null;
        onEnemyFinished.Invoke();
    }

    private EnemyBase GetEnemyForParanoia(Paranoia paranoia) {
        foreach(EnemyBase enemy in enemies) {
            if(enemy.name.Contains(paranoia.ToString()))
                return enemy;
        }

        return null;
    }
}