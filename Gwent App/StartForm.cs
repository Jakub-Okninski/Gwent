﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gwent_App
{
    public partial class StartForm : Form
    {
     
   
        public StartForm()
        {
            InitializeComponent();
    
          
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            LoginForm loginForm = new LoginForm(this);;
            loginForm.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegisterForm RegisterForm = new RegisterForm(this); ;
            RegisterForm.Show();
            this.Hide();

        }
    }
}
