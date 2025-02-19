using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SomethingHanlder : MonoBehaviour
{
    [SerializeField] private int score;
    public int Score { get { return score; } }

    [SerializeField] private int damage;
    public int Damage { get { return damage; } }

    [SerializeField] private bool onKnockback = false;
    public bool OnKnockback { get { return onKnockback; } }

    [SerializeField] private int knockbackDuration;
    public int KnockbackDuration { get { return knockbackDuration; } }

    Animator animator;

    ResourceController resourceController;
    BaseController baseController;

    [SerializeField] Sprite[] sprites = new Sprite[9];

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Init(int spriteindex, int score, int damage, bool onKnockback, int knockbackDuration)
    {
        this.score = score;
        this.damage = damage;
        this.onKnockback = onKnockback;
        this.knockbackDuration = knockbackDuration;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {

            animator.SetBool("IsInteracted", true);

            if (OnKnockback == true && collision.gameObject.TryGetComponent<BaseController>(out baseController))
            {
                baseController.ApplyKnockback(transform, 1, KnockbackDuration);
            }

            MiniGameManger.M_instance.AddScore(score);

            //애니메이션 이벤트로 애니메이션 실행후 파괴되게 해놨음.
        }
    }


}
