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
    public partial class UCDessertOption : UserControl
    {

        public UCDessert dessertForm = null;
        public int quantityDessert = 1;
        private int _orderLineId;
        public UCDessertOption()
        {
            InitializeComponent();
            lblQuantity.Text = quantityDessert.ToString();
        }

        public UCDessertOption(int orderLineId) : this()
        {
            _orderLineId = orderLineId;
        }

        OrderLineOption _orderLineOption = new OrderLineOption();
        private void btnReturn_Click(object sender, EventArgs e)
        {
            OnReturnDessertMenu(true);
        }

        List<int> optionNum = new List<int>();
        //장바구니로
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

            dessertForm.optQuantityDessert = quantityDessert;
            dessertForm._orderLine.Quantity = dessertForm.optQuantityDessert;
            Dao.OrderLine.Update(dessertForm._orderLine);

            Cart();
            OnReturnDessertMenu(true);
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (quantityDessert == 0)
            {
                quantityDessert = 1;
            }
            else
            {
                quantityDessert -= 1;
            }
            lblQuantity.Text = quantityDessert.ToString();
        }
        private void btnPlus_Click(object sender, EventArgs e)
        {
            quantityDessert += 1;
            lblQuantity.Text = quantityDessert.ToString();

        }

        private void btnCreamLess_CheckedChanged(object sender, EventArgs e)
        {
            if (btnCreamLess.Checked == true)
            {
                optionNum.Add(11);
            }
        }

        private void btnCreamMore_CheckedChanged(object sender, EventArgs e)
        {
            if (btnCreamMore.Checked == true)
            {
                optionNum.Add(12);
            }
        }

        bool isCheckedI = false;
        private void btnAddIceC_CheckedChanged(object sender, EventArgs e)
        {

            isCheckedI = btnAddIceC.Checked;
            if (btnAddIceC.Checked == true)
            {
                optionNum.Add(13);
            }
        }

        private void btnAddIceC_Click(object sender, EventArgs e)
        {
            if (btnAddIceC.Checked && !isCheckedI)
            {
                btnAddIceC.Checked = false;
            }
            else
            {
                btnAddIceC.Checked = true;
                isCheckedI = false;
            }
        }

        bool isCheckedF = false;
        private void btnAddFork_CheckedChanged(object sender, EventArgs e)
        {

            isCheckedF = btnAddFork.Checked;
            if (btnAddFork.Checked == true)
            {
                optionNum.Add(14);
            }
        }

        private void btnAddFork_Click(object sender, EventArgs e)
        {
            if (btnAddFork.Checked && !isCheckedF)
            {
                btnAddFork.Checked = false;
            }
            else
            {
                btnAddFork.Checked = true;
                isCheckedF = false;
            }
        }

        #region ReturnDessertMenu event things for C# 3.0
        public event EventHandler<ReturnDessertMenuEventArgs> ReturnDessertMenu;

        protected virtual void OnReturnDessertMenu(ReturnDessertMenuEventArgs e)
        {
            if (ReturnDessertMenu != null)
                ReturnDessertMenu(this, e);
        }

        private ReturnDessertMenuEventArgs OnReturnDessertMenu(bool returnDessert)
        {
            ReturnDessertMenuEventArgs args = new ReturnDessertMenuEventArgs(returnDessert);
            OnReturnDessertMenu(args);

            return args;
        }

        private ReturnDessertMenuEventArgs OnReturnDessertMenuForOut()
        {
            ReturnDessertMenuEventArgs args = new ReturnDessertMenuEventArgs();
            OnReturnDessertMenu(args);

            return args;
        }

        public class ReturnDessertMenuEventArgs : EventArgs
        {
            public bool ReturnDessert { get; set; }

            public ReturnDessertMenuEventArgs()
            {
            }

            public ReturnDessertMenuEventArgs(bool returnDessert)
            {
                ReturnDessert = returnDessert;
            }
        }
        #endregion


    }
}
