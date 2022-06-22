using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills
{
    public class ExpandForce : MonoBehaviour, IObservable
    {
        [SerializeField] private float force;
        [SerializeField] private float forceRedius;
        [SerializeField] private float maxCoolDown;
        [SerializeField] private int damage;
        [SerializeField] private SphereCollider sphreCollider;
        [SerializeField] private string cantUseMessage;
        [SerializeField] private string useMessage;
        private bool _canUseSkill => CurrentCoolDown >= maxCoolDown;
        private List<IObserver> _subscribers = new List<IObserver>();
        private List<IEnemy> _enemiesIn = new List<IEnemy>();

        public bool CanUseSkill => _canUseSkill;
        public float CurrentCoolDown { get; private set; }
        public float MaxCoolDown => maxCoolDown;
        public List<IObserver> Subscribers => _subscribers;


        private void Awake()
        {
            sphreCollider.isTrigger = true;
            sphreCollider.radius = forceRedius;
            ResetSkillCoolDown();
            CurrentCoolDown = maxCoolDown;
        }

        private void Update()
        {
            if (!CanUseSkill)
            {
                CurrentCoolDown += Time.deltaTime;
                NotifyAll("SKILL_UPDATED");
            }
        }

        private void UseForce(Rigidbody body)
        {
            body.AddForce(-body.transform.forward * force, ForceMode.VelocityChange);
            ResetSkillCoolDown();
        }

        private void DamageBody(Rigidbody body)
        {
            body.GetComponent<IDamagable>().TakeDamage(damage);
        }

        private void ResetSkillCoolDown()
        {
            CurrentCoolDown = 0;
        }

        public void UseSkill()
        {
            if (!CanUseSkill || _enemiesIn.Count < 1)
            {
                NotifyAll("MESSAGE", cantUseMessage);
                return;
            }


            for (int i = 0; i < _enemiesIn.Count; i++)
            {
                if (_enemiesIn[i] == null)
                {
                    _enemiesIn.RemoveAt(i);
                }

                var body = _enemiesIn[i].Enemy.GetComponent<Rigidbody>();               
                UseForce(body);
                DamageBody(body);               
            }

            NotifyAll("MESSAGE", useMessage);
        }

        public void Suscribe(IObserver observer)
        {
            if (_subscribers.Contains(observer)) return;
            _subscribers.Add(observer);
        }

        public void Unsuscribe(IObserver observer)
        {
            if (!_subscribers.Contains(observer)) return;
            _subscribers.Remove(observer);
        }

        public void NotifyAll(string message, params object[] args)
        {
            if (_subscribers.Count < 1) return;

            foreach (var suscriber in _subscribers)
            {
                suscriber.OnNotify(message, args);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<IEnemy>();

            if (enemy == null) return;

            _enemiesIn.Add(enemy);
        }

        private void OnTriggerExit(Collider other)
        {
            var enemy = other.GetComponent<IEnemy>();

            if (enemy == null) return;

            _enemiesIn.Remove(enemy);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, forceRedius);
        }
    }

    
}