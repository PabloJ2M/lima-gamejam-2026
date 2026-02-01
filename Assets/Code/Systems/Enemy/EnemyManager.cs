using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour {
    [SerializeField] private EnemyBase[] enemies;

    private EnemyBase _currentEnemy;
    
    public Paranoia CurrentParanoia { get; set; }

    // [SerializeField] private UnityEvent _onSuccess;

    private void Awake() {
        foreach(EnemyBase enemy in enemies) {
            enemy.gameObject.SetActive(false);
            enemy.OnEnemyFinished += OnEnemyFinished;
        }
    }

    public void ActiveEnemy(bool success) {
        _currentEnemy = GetEnemyByParanoia(CurrentParanoia);

        if(_currentEnemy == null) {
            Debug.LogError($"No enemy for paranoia {CurrentParanoia}");
            return;
        }

        _currentEnemy.correctMask = success;
        _currentEnemy.Activate();
    }

    private EnemyBase GetEnemyByParanoia(Paranoia paranoia) {
        foreach(EnemyBase enemy in enemies) {
            if(enemy.TypeEnemy == paranoia)
                return enemy;
        }

        return null;
    }

    private void OnEnemyFinished(EnemyBase enemy) {
        _currentEnemy = null;
    }
}