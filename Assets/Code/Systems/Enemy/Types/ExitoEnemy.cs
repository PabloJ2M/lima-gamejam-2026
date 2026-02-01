using System.Collections;
using PrimeTween;
using UnityEngine;

public class ExitoEnemy : EnemyBase {
    protected override void OnAppear() {
        StartCoroutine(AppearRoutine());
    }

    private IEnumerator AppearRoutine() {
        transform.position = new Vector3(-9f, 0f, 5f);
        Tween.Position(transform, new Vector3(0f, 0f, 5f), 1);
        yield return new WaitForSeconds(2f);

        if(isMaskCorrect)
            yield return StartCoroutine(LeaveRoutine());
        else
            yield return StartCoroutine(AttackRoutine());
    }

    private IEnumerator LeaveRoutine() {
        Tween.Position(transform, new Vector3(9f, 0f, 5f), 1);
        yield return new WaitForSeconds(1.5f);
        Finish(EnemyResult.Success);
    }

    private IEnumerator AttackRoutine() {
        Tween.Position(transform, new Vector3(0f, 0f, 0f), 1);
        yield return new WaitForSeconds(3f);
        Finish(EnemyResult.Failed);
    }
}