using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    public class PlayerSkillInfo : MonoBehaviour, IObserver
    {
        [SerializeField] private Image skillBar;
        [SerializeField] private Color loadingColor;
        [SerializeField] private Color readyColor;

        private ExpandForce _expandForce;

        private void Awake()
        {
            _expandForce = GetComponent<ExpandForce>();
            _expandForce.Suscribe(this);
        }

        public void OnNotify(string message, params object[] args)
        {
            if (message != "SKILL_UPDATED") return;

            UpdateBar();
        }

        private void UpdateBar()
        {
            skillBar.fillAmount = _expandForce.CurrentCoolDown / _expandForce.MaxCoolDown;
            if (skillBar.fillAmount == 1)
                skillBar.color = readyColor;
            else
                skillBar.color = loadingColor;
        }
    }

}