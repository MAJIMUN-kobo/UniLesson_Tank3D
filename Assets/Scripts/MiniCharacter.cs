using UnityEngine;
using UnityEngine.InputSystem;

public class MiniCharacter : MonoBehaviour
{
    [Header("** Shooter Settings **")]
    public GameObject bulletPrefab;     // 弾丸のプレハブ
    public GameObject shotPoint;        // 撃ちだし座標
    public float shotForce = 10f;       // 発射の力

    [Header("** Camera Settings **")]
    public GameObject cameraJoint;      // カメラ軸(CameraJoint)のオブジェクト

    private Vector2 _inputMoveValue;    // Moveの入力値
    private Vector2 _inputLookValue;    // Lookの入力値
    private float _inputAttackValue;    // Attackの入力値
    private Vector3 angles;             // キャラクターの向き(角度)

    void Start()
    {
        
    }

    void Update()
    {
        Move();     // 移動メソッドを呼び出す
        Look();     // 回転メソッドを呼び出す
    }

    // === 移動メソッド ===
    /*
     * 引数　：なし
     * 戻り値：なし
     */ 
    public void Move()
    {
        Vector3 velocity = Vector3.zero;    // 速度の変数
        velocity.z = _inputMoveValue.y;     // 入力(上下)で前後移動
        velocity.x = _inputMoveValue.x;     // 入力(左右)で左右移動

        transform.Translate(velocity * Time.deltaTime);
    }

    // === 回転(向き)メソッド ===
    /*
     * 引数　：なし
     * 戻り値：なし
     */
    public void Look()
    {
        angles.x += _inputLookValue.y;  // y 入力で x 軸回転
        angles.y += _inputLookValue.x;  // x 入力で y 軸回転

        // x 軸の角度に制限を設ける
        // 👇範囲を設ける数学関数
        // [Mathf.Clamp(対象値, 最小値, 最大値)]
        angles.x = Mathf.Clamp(angles.x, -90, 90);

        // TpsPlayer(自分) の y 軸だけ回転
        transform.eulerAngles = new Vector3(0, angles.y, 0); // 角度を代入
        // CameraJoint の x 軸を回転
        cameraJoint.transform.eulerAngles = new Vector3(angles.x * -1, angles.y, 0); // 角度を代入
    }

    // === InputAction [Move]のイベント処理
    void OnMove(InputValue value)
    {
        _inputMoveValue = value.Get<Vector2>();     // 入力値の保持
    }

    // === InputAction [Look]のイベント処理
    void OnLook(InputValue value)
    {
        _inputLookValue = value.Get<Vector2>();     // 入力値の保持
    }

    // === InputAction [Attack]のイベント処理
    void OnAttack(InputValue value)
    {
        _inputAttackValue = value.Get<float>();     // 入力値の保持
    }
}
