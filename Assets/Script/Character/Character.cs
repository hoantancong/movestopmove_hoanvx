using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    [SerializeField] protected GameObject handWeapon;
    public float speed = 5f;
    public float attackRadius = 6f;
    public float attackDelayTime = 1f;
    public bool isFired;
    public Animator animator;
    protected GameObject currentBullet;
    public bool isAlive;
    private void Awake()
    {
        
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        isAlive = true;
    }
    public void Fire(Vector3 target)
    {
        if (!isFired)
        {
            animator?.SetBool("IsRun", false);
            animator?.SetTrigger("Fire");
            handWeapon.SetActive(false);
            GameObject bullet = ObjectPool.Instance.SpawnFromPool(Constants.TAG_WEAPON, TF.position+TF.forward*0.5f, Quaternion.identity);
            bullet.GetComponent<RotationBullet>().OnInit(this, target);
            bullet.SetActive(true);
            currentBullet = bullet;
            isFired = true;
            StartCoroutine(CanFireAvaiable());

        }
    }
    IEnumerator CanFireAvaiable()
    {
        yield return new WaitForSeconds(attackDelayTime);
        isFired = false;
        currentBullet = null;
        handWeapon.SetActive(true);
    }
    public virtual Transform FireTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);
        foreach (var hitCollider in hitColliders)
        {
            Character character = hitCollider.GetComponent<Character>();
            if (character != null && character.isAlive && character!=this)
            {
                TF.LookAt(character.TF.position);
                return character.TF;
            }
        }
        return null;
    }
    //when weapon is ready for using
    public void Idle()
    {
        animator?.SetBool("IsRun", false);
    }
    public void Move(Vector3 direction)
    {
        TF.Translate(direction * Time.deltaTime * speed, Space.World);
        TF.forward = direction;
        animator?.SetBool("IsRun", true);
    }
    public virtual void GetHit()
    {
        if (!isAlive) return;
        isAlive = false;
        StopAllCoroutines();
        handWeapon.SetActive(false);
        animator.SetBool("IsRun", false);
        animator.SetBool("IsDie", true);
    }



}
