using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPW
{
    internal class RadioButtonGroupManager<T>
    {

        public event EventHandler<T> SelectionChanged;

        Dictionary<RadioButton, T> radioButtonToValue = new Dictionary<RadioButton, T>();

        public RadioButtonGroupManager()
        {
        }

        public void Add(RadioButton radioButton, T value)
        {
            if (radioButton == null)
                throw new ArgumentNullException(nameof(radioButton));
            if (radioButtonToValue.ContainsKey(radioButton))
                throw new ArgumentException("RadioButton already added to the group.", nameof(radioButton));
            radioButtonToValue[radioButton] = value;
            radioButton.CheckedChanged += RadioButton_CheckedChanged;
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Checked)
            {
                SelectionChanged?.Invoke(this, radioButtonToValue[rb]);
            }
        }

        public void checkRadio(RadioButton r)
        {
            r.Checked=true;
        }
        public void checkRadio(T value)
        {
            RadioButton rb = null;
            foreach (var kvp in radioButtonToValue)
            {
                if (EqualityComparer<T>.Default.Equals(kvp.Value, value))
                {
                    rb = kvp.Key;
                    break;
                }
            }
            if (rb != null)
                checkRadio(rb);
        }
    }
}
