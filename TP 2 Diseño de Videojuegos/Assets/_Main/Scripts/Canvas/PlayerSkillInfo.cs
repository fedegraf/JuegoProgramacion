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

        private SkillController _skillController;

        private void Awake()
        {
            _skillController = GetComponent<SkillController>();
            _skillController?.Suscribe(this);
        }

        public void OnNotify(string message, params object[] args)
        {
            if (message != "SKILL_UPDATED") return;

            UpdateBar();
        }

        private void UpdateBar()
        {
            skillBar.fillAmount = _skillController.CurrentCoolDown / _skillController.MaxCoolDown;
            if (skillBar.fillAmount == 1)
                skillBar.color = readyColor;
            else
                skillBar.color = loadingColor;
        }
    }

}