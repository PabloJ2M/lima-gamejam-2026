using System.Collections;
using UnityEngine;

public class ObservadoEnemy : EnemyBase {
    protected override void OnAppear() {
        StartCoroutine(AppearRoutine());
    }

    private IEnumerator AppearRoutine() {
        // entra y observa
        yield return new WaitForSeconds(2f);
        // se queda esperando la máscara
    }

    protected override void OnMaskSuccess() {
        StartCoroutine(LeaveRoutine());
    }

    protected override void OnMaskFailed() {
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator LeaveRoutine() {
        // mira un momento y se va
        yield return new WaitForSeconds(1.5f);
        Finish();
    }

    private IEnumerator AttackRoutine() {
        // ataque
        yield return new WaitForSeconds(3f);
        Finish();
    }
}