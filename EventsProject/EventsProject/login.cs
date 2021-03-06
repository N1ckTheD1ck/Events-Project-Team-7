﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Security.Cryptography;

namespace EventsProject
{
	public partial class login : Form
	{
		public login()
		{
			InitializeComponent();
		}

		OleDbConnection con = new OleDbConnection(Properties.Settings.Default.EventsConnectionString);
		HashCode hash = new HashCode();
		static int id;
		int is_admin = 0;
		public bool authentication()
		{
			string sql = "SELECT * FROM UserTable WHERE username = '" + usernameTextBox.Text + "' AND [password] = '" + hash.encrypt(passwordTextBox.Text) + "'";
			OleDbCommand cmd = new OleDbCommand(sql, con);
			cmd.CommandType = CommandType.Text;
			bool ok = false;
			con.Open();

			OleDbDataReader dr = cmd.ExecuteReader();
			try
			{

				if (dr.Read())
				{
					MessageBox.Show("login succesfully!!!");
					is_admin = Convert.ToInt32(dr["is_admin"]);
					id = (int)dr["ID"];
					ok = true;
				}
				else
				{
					MessageBox.Show("wrong username or password");
					ok = false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			
			con.Close();
		
			return ok;
		}

		public int identity()
		{
			string sql = "SELECT * FROM UserTable WHERE username = '" + usernameTextBox.Text + "' AND [password] = '" + hash.encrypt(passwordTextBox.Text) + "'";
			OleDbCommand cmd = new OleDbCommand(sql, con);
			cmd.CommandType = CommandType.Text;

			OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
			
			con.Open();

			OleDbDataReader dr = cmd.ExecuteReader();
			try
			{

				if (dr.Read())
				{
					id = (int)dr["ID"];

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			con.Close();
			return id;
		}

		public void loginButton_Click(object sender, EventArgs e)
		{
			string username = usernameTextBox.Text;
			if (usernameTextBox.Text.Length != 0)
			{
				if (authentication())
				{
					identity();
					this.Hide();
					startForm start = new startForm(username);
					start.Show();
					start.activate();
					if (is_admin == 1)
					{
						start.admin();
					}
				}
			}
			else
			{
				MessageBox.Show("empty fields");
			}
		}

		private void registerButton_Click(object sender, EventArgs e)
		{
			this.Hide();
			register reg = new register();
			reg.Show();
		}

		private void forgotPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.Hide();
			forgotPass forgot = new forgotPass();
			forgot.Show();
		}

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void usernameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

		private void backButton_Click(object sender, EventArgs e)
		{
			startForm startF = new startForm();
			this.Hide();
			startF.Show();
		}

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
