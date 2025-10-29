using InfraEstrutura.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace SistemaEstoque
{
    public partial class Login : Window
    {
        public Login()
        {   
            InitializeComponent();  
        }   

        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            // 1. Obter os dados da tela
            string usuario = txtUsuario.Text;
            string senha = txtSenha.Password;

            // 2. Validação básica
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Por favor, preencha o email e a senha.");
                return;
            }

            // 3. Hashear a senha digitada para comparar com a do banco
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes("123");
            var hashedPassword = sha.ComputeHash(asByteArray);
            var senhaHashBase64 = Convert.ToBase64String(hashedPassword);
            // 4. Consultar o banco de dados
            using (var dbContext = new ProjetoContext())
            {
                // Procuramos um usuário que tenha o mesmo email E a mesma senha hasheada
                var usuarioEncontrado = dbContext.Responsaveis.FirstOrDefault(u =>
                    u.Nome == usuario && u.SenhaHash == senhaHashBase64);

                // 5. Verificar o resultado
                if (usuarioEncontrado != null)
                {
                    // SUCESSO!
                    // Abre a tela principal
                    var mainWindow = new MainWindow();
                    mainWindow.Show();

                    // Fecha a tela de login
                    this.Close();
                }
                else
                {
                    // FALHA!
                    MessageBox.Show("Email ou senha inválidos.");
                }
            }
        }
    }
}

