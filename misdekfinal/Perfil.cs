﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace misdekfinal
{
    public partial class Perfil : Form
    {
        public string Nombre
        {
            get { return labelNombre.Text; }
            set { labelNombre.Text = value; }
        }

       
        public string Escuela
        {
            get { return labelEscuela.Text; }
            set { labelEscuela.Text = value; }
        }
        public string Telefono
        {
            get { return labelTelefono.Text; }
            set { labelTelefono.Text = value; }
        }



        public Perfil()
        {
            InitializeComponent();
        }

        private void Perfil_Load(object sender, EventArgs e)
        {
            labelNombre.Text = Nombre;

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string correo = "ehernandez71@ucol.mx";

            string mailtoUrl = $"mailto:{correo}";

            Process.Start(new ProcessStartInfo(mailtoUrl));

            e.Link.Visited = true;
        }
    }
}
