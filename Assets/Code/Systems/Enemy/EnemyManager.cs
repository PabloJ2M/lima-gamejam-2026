using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour {
    [SerializeField] private EnemyBase[] enemies;

    [Header("Enemy Result Events")]
    [SerializeField] private UnityEvent onEnemyPassed;
    [SerializeField] private UnityEvent onEnemyAttacked;

    private EnemyBase _currentEnemy;
    public Paranoia PendingParanoia { get; set; }

    private void Awake() {
        foreach(EnemyBase enemy in enemies) {
            enemy.gameObject.SetActive(false);
            enemy.OnEnemyResolved += OnEnemyResolved;
        }
    }

    public void ActivateEnemy(bool maskCorrect) {
        _currentEnemy = FindEnemyForParanoia(PendingParanoia);

        if(_currentEnemy == null) {
            Debug.LogError($"No enemy for paranoia {PendingParanoia}");
            return;
        }

        _currentEnemy.Activate(maskCorrect);
    }

    private EnemyBase FindEnemyForParanoia(Paranoia paranoia) {
        foreach(EnemyBase enemy in enemies) {
            if(enemy.EnemyType == paranoia)
                return enemy;
        }

        return null;
    }

    private void OnEnemyResolved(EnemyBase enemy, EnemyResult result) {
        _currentEnemy = null;

        if(result == EnemyResult.Success)
            onEnemyPassed?.Invoke();
        else
            onEnemyAttacked?.Invoke();
    }
}

public enum EnemyResult {
    Success, // pasó de frente
    Failed // atacó
}