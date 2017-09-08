using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class AntBase : NetworkBehaviour
{
    private Transform _Base;

    public float MovePerSec = 1f;

    public Transform Source;
    public Transform Target;

    protected SpriteRenderer _Sprite;
    protected Rigidbody2D _Body;
    protected CircleCollider2D _Collider;

    private void Awake()
    {
        _Base = GameObject.Find("building.base").transform;

        transform.SetParent(GameObject.Find("Ants").transform);

        _Sprite = GetComponent<SpriteRenderer>();
        _Sprite.sprite = Resources.Load("Sprites/ant_worker", typeof(Sprite)) as Sprite;

        _Body = GetComponent<Rigidbody2D>();
        _Body.mass = 0;
        _Body.gravityScale = 0;

        _Collider = GetComponent<CircleCollider2D>();
        _Collider.radius = 0.1f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Target || collision.gameObject == Source)
        {
            _Sprite.enabled = true;
            _Collider.isTrigger = false;
        }
    }

    private void Start()
    {
        if (Source == null)
        {
            transform.position = _Base.localPosition.Uncertain();
        }
        else
        {
            transform.position = Source.transform.localPosition.Uncertain();
        }
    }

    private void Update()
    {
        if (Target == null)
        {
            Move(_Base.localPosition);
        }
        else
        {
            Move(Target.localPosition);
        }
    }

    protected void Move(Vector3 target)
    {
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, target, MovePerSec * Time.deltaTime);
    }

    protected virtual void Entering(GameObject go)
    {
        _Sprite.enabled = false;
        GetComponent<CircleCollider2D>().isTrigger = true;
        //WaitStart = Time.time;
    }
}
