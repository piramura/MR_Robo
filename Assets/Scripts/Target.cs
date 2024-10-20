using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public void OnHit()
    {
        Debug.Log("Target Hit!");
        // ターゲットがヒットしたときの処理
        Destroy(gameObject);  // ターゲットを消すなど
    }
}
