using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CafeKiosk.Data;

namespace CafeKiosk
{
    public partial class UCJuiceOption : UserControl
    {
        public UCJuice juiceForm = null;
        public int quantityJuice = 1;
        private int _orderLineId;
        OrderLineOption _orderLineOption = new OrderLineOption();

        public UCJuiceOption()
        {
            InitializeComponent();
            lblQuantity.Text = quantityJuice.ToString();
        }

        public UCJuiceOption(int orderLineId) : this()
        {
            _orderLineId = orderLineId;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            OnReturnJuiceMenu(true);
        }

        List<int> optionNum = new List<int>();

        public void Cart()
        {
            foreach (int number in optionNum)
            {
                _orderLineOption.OrderLineID = _orderLineId;
                _orderLineOption.OptionID = number;
                Dao.OrderLineOption.Insert(_orderLineOption);
            }
        }

        private void btnToCart_Click(object sender, EventArgs e)
        {
            juiceForm.optQuantityJuice = quantityJuice;
            juiceForm._orderLine.Quantity = juiceForm.optQuantityJuice;
            Dao.OrderLine.Update(juiceForm._orderLine);

            Cart();
            OnReturnJuiceMenu(true);
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (quantityJuice == 0)
            {
                quantityJuice = 1;
            }
            else
            {
                quantityJuice -= 1;
            }
            lblQuantity.Text = quantityJuice.ToString();

        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            quantityJuice += 1;
            lblQuantity.Text = quantityJuice.ToString();
        }

        private void btnTall_CheckedChanged(object sender, EventArgs e)
        {
            optionNum.Add(8);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            optionNum.Add(9);
        }

        //ice
        bool isChecked = false;
        private void btnIce_CheckedChanged(object sender, EventArgs e)
        {
            isChecked = btnIce.Checked;
            if (btnIce.Checked == true)
            {
                optionNum.Add(10);
            }
        }
        private void btnIce_Click(object sender, EventArgs e)
        {
            if (btnIce.Checked && !isChecked)
            {
                btnIce.Checked = false;
            }
            else
            {
                btnIce.Checked = true;
                isChecked = false;
            }
        }
        #region ReturnJuiceMenu event things for C# 3.0
        public event EventHandler<ReturnJuiceMenuEventArgs> ReturnJuiceMenu;

        protected virtual void OnReturnJuiceMenu(ReturnJuiceMenuEventArgs e)
        {
            if (ReturnJuiceMenu != null)
                ReturnJuiceMenu(this, e);
        }

        private ReturnJuiceMenuEventArgs OnReturnJuiceMenu(bool returnJuice)
        {
            ReturnJuiceMenuEventArgs args = new ReturnJuiceMenuEventArgs(returnJuice);
            OnReturnJuiceMenu(args);

            return args;
        }

        private ReturnJuiceMenuEventArgs OnReturnJuiceMenuForOut()
        {
            ReturnJuiceMenuEventArgs args = new ReturnJuiceMenuEventArgs();
            OnReturnJuiceMenu(args);

            return args;
        }

        public class ReturnJuiceMenuEventArgs : EventArgs
        {
            public bool ReturnJuice { get; set; }

            public ReturnJuiceMenuEventArgs()
            {
            }

            public ReturnJuiceMenuEventArgs(bool returnJuice)
            {
                ReturnJuice = returnJuice;
            }
        }
        #endregion
    }
}
