using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
//데미지를 입을 수 있는 타입들은 IDamageable을 상속하고 OnDamage 메서드를 반드시 구현
