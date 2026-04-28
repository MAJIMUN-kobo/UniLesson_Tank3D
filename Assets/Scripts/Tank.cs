using UnityEngine;
using UnityEngine.Rendering;

public class Tank : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 90f;
    public float topTurnSpeed = 90f;
    public float cannonTurnSpeed = 90f;

    public Transform top;
    public Transform cannon;

    [Header("** Shot Settings **")]
    public Transform shotPoint;         // 撃ちだし座標
    public GameObject bulletPrefab;     // 弾丸のプレハブ

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Turn();
        TopTurn();
        CannonTurn();
        Shot();
    }

    void Movement()
    {
        float deltaMoveSpeed = moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, deltaMoveSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -deltaMoveSpeed);
        }
    }

    void Turn()
    {
        float deltaTurnSpeed = turnSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -deltaTurnSpeed, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, deltaTurnSpeed, 0);

        }
    }

    void TopTurn()
    {
        float deltaTopTurnSpeed = topTurnSpeed * Time.deltaTime;

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            top.transform.Rotate(0, -deltaTopTurnSpeed, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            top.transform.Rotate(0, deltaTopTurnSpeed, 0);
        }
    }

    void CannonTurn()
    {
        float deltaCannonTurnSpeed = cannonTurnSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            cannon.transform.Rotate(-deltaCannonTurnSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cannon.transform.Rotate(deltaCannonTurnSpeed, 0, 0);
        }
    }

    void Shot()
    {
        if (Input.GetMouseButton(0))
        {
            // 弾丸を生成する
            GameObject origin   = bulletPrefab;
            Vector3 position    = shotPoint.position;
            Quaternion rotation = shotPoint.rotation;

            GameObject bullet = Instantiate(origin, position, rotation);

            // 弾丸を飛ばす
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(shotPoint.forward * 50, ForceMode.Impulse);
            
            Destroy(bullet, 5f);
        }
    }

    void OnCollisionEnter( Collision collision )
    {
        // "Box"に触れると、タンクが消える（死亡）
        if (collision.transform.name.Equals("Box"))
        {
            Debug.Log($"[ {collision.transform.name} ]に触れました！ゲームオーバー...");
            Destroy(gameObject);
        }
    }
}
