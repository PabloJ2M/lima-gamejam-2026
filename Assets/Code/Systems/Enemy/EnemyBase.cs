using System;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour {
    [SerializeField] private Paranoia typeEnemy;
    public event Action<EnemyBase> OnEnemyFinished;

    public void Activate() {
        gameObject.SetActive(true);
        OnAppear();
    }

    public void ResolveMask(bool success) {
        if(success)
            OnMaskSuccess();
        else
            OnMaskFailed();
    }

    protected void Finish() {
        gameObject.SetActive(false);
        OnEnemyFinished?.Invoke(this);
    }

    protected abstract void OnAppear();
    protected abstract void OnMaskSuccess();
    protected abstract void OnMaskFailed();
}