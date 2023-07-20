using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Workspace.Scripts
{
    public class MainMenuInfoBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI amountTxt;

        private int _currentAmount = 0;

        public void UpdateAmountTxt(int newValue)
        {
            DOVirtual.Float(_currentAmount, newValue, .25f, (x) =>
            {
                _currentAmount = (int) x;
                amountTxt.SetText(FormatNumber(_currentAmount));
            });
        }

        private string FormatNumber(int value)
        {
            switch (value)
            {
                case < 1000:
                    return value.ToString();
                case >= 1000 and < 1000000:
                    return (value / 1000).ToString() + "K";
                case >= 1000000:
                    return (value / 1000000).ToString() + "M";
            }
        }
    
    }
}
