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
        private SphereCollider _sphreCollider;
        private bool _canUseSkill => CurrentCoolDown == maxCoolDown;
        private bool _useSkillTrigger;
        private List<IObserver> _subscribers = new List<IObserver>();


        public bool CanUseSkill => _canUseSkill;
        public float CurrentCoolDown { get; private set; }
        public float MaxCoolDown => maxCoolDown;
        public List<IObserver> Subscribers => _subscribers;


        private void Awake()
        {
            _sphreCollider = GetComponent<SphereCollider>();
            _sphreCollider.radius = forceRedius;
            ResetSkillCoolDown();
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
            body.AddForce(-body.transform.forward * force, ForceMode.Impulse);
            Debug.Log("Used Force");
            _useSkillTrigger = false;
        }

        private void ResetSkillCoolDown()
        {
            CurrentCoolDown = 0;
        }

        public void UseSkill()
        {
            if (CanUseSkill) return;

            _useSkillTrigger = true;
            ResetSkillCoolDown();
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
            var enemy = other.gameObject.GetComponent<IEnemy>();
            var enemyBody = other.gameObject.GetComponent<Rigidbody>();

            if (enemy != null && enemyBody != null)
            {
                if(_useSkillTrigger)
                    UseForce(enemyBody);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, forceRedius);
        }
    }

    
}