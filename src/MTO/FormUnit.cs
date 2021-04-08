﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MTO.Models;

namespace MTO
{
    public partial class FormUnit : Form
    {
        private bool isReadOnly = false;

        public FormUnit()
        {
            InitializeComponent();
            
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            int PK_Unit = Convert.ToInt32(dgv_units.SelectedRows[0].Cells[2].Value);
            Unit changingUnit = Program.db.Units.Find(PK_Unit);

            if (changingUnit != null)
            {
                FormUnitAdd form = new FormUnitAdd(changingUnit);
                form.ShowDialog();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            FormUnitAdd form = new FormUnitAdd();
            form.ShowDialog();
        }

        private void FormUnit_Load(object sender, EventArgs e)
        {
            dgv_units.AutoGenerateColumns = false;
            dgv_units.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            if (!Program.user.isAdmin())
            {
                btn_add.Enabled = false;
                isReadOnly = true;
            }
        }

        private void FormUnit_Activated(object sender, EventArgs e)
        {
            updateUnitTable();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                int PK_Unit = Convert.ToInt32(dgv_units.SelectedRows[0].Cells[2].Value);

                if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "Удаление",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Unit deletingUnit = Program.db.Units.Find(PK_Unit);

                    if (deletingUnit != null)
                    {
                        Program.db.Units.Remove(deletingUnit);
                        Program.db.SaveChanges();
                    }
                }

                //обновляем тоблицу
                updateUnitTable();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления, " + ex.ToString());
            }
        }

        private void updateUnitTable()
        {
            List<Unit> units = Program.db.Units.ToList();
            dgv_units.DataSource = units;

            dgv_units.Columns[2].DataPropertyName = "PK_Unit";
            dgv_units.Columns[1].DataPropertyName = "Name";
            dgv_units.Columns[0].DataPropertyName = "Cipher";
        }

        private void dgv_units_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_units.SelectedRows != null && !isReadOnly)
            {
                btn_delete.Enabled = true;
                btn_change.Enabled = true;
            }
            else
            {
                btn_delete.Enabled = false;
                btn_change.Enabled = false;
            }
        }
    }
}
