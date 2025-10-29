using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SistemaEstoque
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Se quiser, pode carregar o relatório semanal e pré-visualização do estoque aqui
            CarregarRelatorioSemanal();
            CarregarPreviewEstoque();
        }

        // Botão: Gerenciar Responsáveis
        private void AbrirResponsaveis_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ResponsaveisWindow janelaResponsaveis = new ResponsaveisWindow();
                {
                    Owner = this; // MainWindow como dona da janela
                };
                janelaResponsaveis.ShowDialog(); // Modal
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Erro ao abrir a janela de responsáveis:\n{ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Botão: Visualizar Estoque
        private void AbrirEstoque_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EstoqueWindow janelaEstoque = new EstoqueWindow();
                {
                    Owner = this;
                };
                janelaEstoque.ShowDialog(); // Modal
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Erro ao abrir a janela de estoque:\n{ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Exemplo de método para carregar o relatório semanal (pode ser do banco de dados)
        private void CarregarRelatorioSemanal()
        {
            // Aqui você pode substituir por uma consulta real no banco
            var listaExemplo = new[]
            {
                new { Nome = "Produto A", QuantidadeAtual = 50, QuantidadeNecessaria = 100, Status = "Comprar" },
                new { Nome = "Produto B", QuantidadeAtual = 200, QuantidadeNecessaria = 150, Status = "Estável" }
            };

            dgRelatorioSemanal.ItemsSource = listaExemplo;
            // COM BANCO DE DADOS ALGO COMO:
            //dgRelatorioSemanal.ItemsSource = conexao.ExecutarConsulta("SELECT * FROM Produtos");

        }

        // Exemplo de método para carregar pré-visualização do estoque
        private void CarregarPreviewEstoque()
        {
            var previewExemplo = new[]
            {
                new { Nome = "Produto A", Quantidade = 50 },
                new { Nome = "Produto B", Quantidade = 200 }
            };

            lvPreviewEstoque.ItemsSource = previewExemplo;
            // COM BANCO DE DADOS ALGO COMO:
            //Fará um SELECT Nome, Quantidade FROM Produtos
        }
    }
}