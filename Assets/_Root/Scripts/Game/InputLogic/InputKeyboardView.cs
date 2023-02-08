using JoostenProductions;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Game.InputLogic
{
    internal class InputKeyboardView : BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 10;
        private const string _horizontal = "Horizontal";

        protected override void Move()
        {
            float axisOffset = Input.GetAxis(_horizontal);
            float moveValue = _inputMultiplier * Time.deltaTime * axisOffset;

            float abs = Mathf.Abs(moveValue);
            float sign = Mathf.Sign(moveValue);

            if (sign > 0)
                OnRightMove(abs);
            else if (sign < 0)
                OnLeftMove(abs);
        }
    }
}
