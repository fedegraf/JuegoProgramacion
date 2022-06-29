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
        [SerializeField] private Color warningColor;

        private SkillController _skillController;

        private bool _isWarning;

        private void Awake()
        {
            _skillController = GetComponent<SkillController>();
            _skillController?.Suscribe(this);
        }

        public void OnNotify(string message, params object[] args)
        {
            if (message == "SKILL_UPDATED")
            {
                UpdateBar();
            }

            else if (message == "SKILL_TRYTOUSE")
            {
                StartCoroutine(ChangeBarColor());
            }
        }

        private IEnumerator ChangeBarColor()
        {
            _isWarning = true;

            var lastColor = skillBar.color;

            skillBar.color = warningColor;

            yield return new WaitForSeconds(.20f);

            skillBar.color = lastColor;

            yield return new WaitForSeconds(.20f);

            skillBar.color = warningColor;

            yield return new WaitForSeconds(.20f);

            skillBar.color = lastColor;
            _isWarning = false;


            yield return null;
        }

        private void UpdateBar()
        {
            skillBar.fillAmount = _skillController.CurrentCoolDown / _skillController.MaxCoolDown;
            if (_isWarning) return;

            if (skillBar.fillAmount == 1)
                skillBar.color = readyColor;
            else
                skillBar.color = loadingColor;
        }
    }

}