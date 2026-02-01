using System;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour {
    [SerializeField] private Paranoia typeEnemy;
    public Paranoia TypeEnemy => typeEnemy;
    
    public bool correctMask = false;

    public event Action<EnemyBase> OnEnemyFinished;

    public void Activate() {
        gameObject.SetActive(true);
        OnAppear();
    }

    protected void Finish() {
        gameObject.SetActive(false);
        OnEnemyFinished?.Invoke(this);
    }

    protected abstract void OnAppear();
    protected abstract void OnMaskSuccess();
    protected abstract void OnMaskFailed();
}