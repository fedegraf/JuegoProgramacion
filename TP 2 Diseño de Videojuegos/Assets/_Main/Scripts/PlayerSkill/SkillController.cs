using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills
{
    public class SkillController : MonoBehaviour, IObservable
    {
        [SerializeField] private float maxCoolDown;
        [SerializeField] private GameObject expandForce;
        [SerializeField] private string cantUseMessage;
        [SerializeField] private string useMessage;
        private bool _canUseSkill => CurrentCoolDown >= maxCoolDown;
        private List<IObserver> _subscribers = new List<IObserver>();

        public bool CanUseSkill => _canUseSkill;
        public float CurrentCoolDown { get; private set; }
        public bool IsSphereExpanded { get; private set; }
        public float MaxCoolDown => maxCoolDown;
        public List<IObserver> Subscribers => _subscribers;


        private void Awake()
        {
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

        private void ResetUseSkillCoolDown()
        {
            CurrentCoolDown = 0;
        }

        public void UseSkill()
        {
            if (!CanUseSkill)
            {
                NotifyAll("MESSAGE", cantUseMessage);
                return;
            }

            expandForce.SetActive(true);
            expandForce.GetComponent<ExpandForceV2>().DoStartForce();
            ResetUseSkillCoolDown();
            NotifyAll("MESSAGE", useMessage);
        }


        //Observer
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
    }
}
