﻿using SkladtradeEntity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skladtrade
{
    public partial class FormSearch : Form
    {
        public FormSearch()
        {
            InitializeComponent();
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {
            LoadCategory();
            LoadManufacturer();
        }

        private void LoadManufacturer()
        {
            try
            {
                this.comboBoxManufacturer.DisplayMember = "Name";
                this.comboBoxManufacturer.ValueMember = "ID";
                this.comboBoxManufacturer.DataSource = Manufacturer.GetAll();
            }
            catch (Exception ex)
            {
                this.comboBoxManufacturer.DataSource = null;
                this.comboBoxManufacturer.Items.Add(ex.Message);
            }
        }

        private void LoadCategory()
        {
            try
            {
                this.comboBoxCategory.DisplayMember = "Name";
                this.comboBoxCategory.ValueMember = "ID";
                this.comboBoxCategory.DataSource = Category.GetAll();
            }
            catch (Exception ex)
            {
                this.comboBoxManufacturer.DataSource = null;
                this.comboBoxManufacturer.Items.Add(ex.Message);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            List<Product> theProducts = new List<Product>();
            foreach (var order in Order.GetAll().Where(x => x.OrderStatus.ID == 5 || x.OrderStatus.ID == 8))
            {
                foreach (var item in order.OrderProducts)
                {
                    for (int i = 0; i < item.Count; i++)
                    {
                        theProducts.Add(item.Product);
                    }

                }
            }



            if (this.radioButtonSearchCategory.Checked)
            {
                Category theCategory = this.comboBoxCategory.SelectedItem as Category;
                

                foreach (var item in theProducts.Where(x => x.Category.ID == theCategory.ID))
                {
                    this.listBoxSearchResult.Items.Add(item.created_at.ToString() + " " + item.Name);
                }

            }

            if (this.radioButtonSearchManufacturer.Checked)
            {
                Manufacturer theManufacturer = this.comboBoxManufacturer.SelectedItem as Manufacturer;

                foreach (var item in theProducts.Where(x => x.Manufacturer.ID == theManufacturer.ID))
                {
                    this.listBoxSearchResult.Items.Add(item.created_at.ToString() + " " + item.Name);
                }

            }

            if (this.radioButtonSearchPrice.Checked)
            {
                foreach (var item in theProducts.Where(x => x.Price < 250))
                {
                    this.listBoxSearchResult.Items.Add(item.created_at.ToString() + " " + item.Name);
                }
            }

            if (this.radioButton1.Checked)
            {
                foreach (var item in theProducts.Where(x => x.Price > 250 && x.Price < 500))
                {
                    this.listBoxSearchResult.Items.Add(item.created_at.ToString() + " " + item.Name);
                }
            }

            if (this.radioButtonSearchPrice.Checked)
            {
                foreach (var item in theProducts.Where(x => x.Price > 500))
                {
                    this.listBoxSearchResult.Items.Add(item.created_at.ToString() + " " + item.Name);
                }
            }
        }
    }
}
