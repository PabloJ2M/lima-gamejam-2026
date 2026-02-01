using System;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour {
    [SerializeField] private Paranoia enemyType;
    public Paranoia EnemyType => enemyType;

    protected bool isMaskCorrect;

    public event Action<EnemyBase, EnemyResult> OnEnemyResolved;

    public void Activate(bool maskCorrect) {
        isMaskCorrect = maskCorrect;
        gameObject.SetActive(true);
        OnAppear();
    }

    protected void Finish(EnemyResult result) {
        gameObject.SetActive(false);
        OnEnemyResolved?.Invoke(this, result);
    }

    protected abstract void OnAppear();
}