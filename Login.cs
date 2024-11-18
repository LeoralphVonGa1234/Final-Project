
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fianl_Project_bank_account
{
    public partial class Login : Form
    {

        // In-memory storage for users (email as key, password as value)
        private Dictionary<string, string> users = new Dictionary<string, string>();

        // Secret key for signing JWT tokens
        private const string SecretKey = "ThisIsASecretKeyForJWTGeneration";


        public Login()
        {
            InitializeComponent();
            InitializePlaceholders();
        }
        // Initialize placeholders for email and password textboxes
        // Initialize placeholders for email and password textboxes
        private void InitializePlaceholders()
        {
            SetPlaceholderText(txtEmail, "Email");
            SetPlaceholderText(txtPassword, "Password");
        }

        private void SetPlaceholderText(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = System.Drawing.Color.Gray;

            textBox.Enter += (sender, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = System.Drawing.Color.Black;
                }
            };

            textBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = System.Drawing.Color.Gray;
                }
            };
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (users.ContainsKey(email) && users[email] == password)
            {
                string token = GenerateJwtToken(email); // Generate JWT token for the user
                MessageBox.Show("Login successful! Your JWT Token: " + token, "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open Bank Account Management Form after login
                Bank bank = new Bank();
                bank.Show();
                this.Hide();  // Hide the login form
            }
            else
            {
                MessageBox.Show("Invalid email or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (users.ContainsKey(email))
            {
                MessageBox.Show("This email is already registered. Please try a different one.", "Sign Up Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                users.Add(email, password);
                MessageBox.Show("Sign up successful! You can now log in.", "Sign Up", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private string GenerateJwtToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "your-app",
                audience: "your-app",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
       


 
                

    


  

