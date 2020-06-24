using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _birdWasLaunched;          // 用于判断是否有启动小鸟（拖拽小鸟）
    private float _timeSittingAround;       // 小鸟掉在地上的时间

    [SerializeField] private float _lauchPower = 500;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);

        // 当小鸟掉在地上时（速度近乎0）
        if (_birdWasLaunched && 
            GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _timeSittingAround += Time.deltaTime;
        }

        // 小鸟超出屏幕边界以外的一定距离，重置场景（回归原位）
        // 小鸟的失去速度（静止，也可以是掉在地上）的3秒后（未拖拽的不算），重置场景
        if (transform.position.y > 8 ||
            transform.position.y < -8 ||
            transform.position.x > 20 ||
            transform.position.x < -15 ||
            _timeSittingAround > 3)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    private void OnMouseDown()
    {
        // 拖拽改变小鸟颜色功能
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnMouseUp()
    {
        // 松手恢复小鸟颜色（白色，即无）
        GetComponent<SpriteRenderer>().color = Color.white;

        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _lauchPower);
        // 施加重力影响，默认开始g=0，即小鸟不会自由落体
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _birdWasLaunched = true;

        GetComponent<LineRenderer>().enabled = false;
    }

    private void OnMouseDrag()
    {
        // 拖拽功能，先转换到世界坐标，然后实时更改坐标位置
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y, 0);
    }
}
