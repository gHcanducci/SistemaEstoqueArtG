using InfraEstrutura.Data;
using InfraEstrutura.Entidades;
using System;
using System.Linq;
using System.Windows;

namespace SistemaEstoque
{
    public partial class RegistrarSaidaWindow : Window
    {
        private readonly ProjetoContext _contexto;

        public RegistrarSaidaWindow()
        {
            InitializeComponent();
            _contexto = new ProjetoContext();
            CarregarDados();
            dpDataSaida.SelectedDate = DateTime.Now; // Define data padrão como hoje
        }

        private void CarregarDados()
        {
            // Carrega produtos
            cbProduto.ItemsSource = _contexto.Produtos.ToList();
            cbProduto.DisplayMemberPath = "Nome";
            cbProduto.SelectedValuePath = "Id";

            // Carrega responsáveis
            cbResponsavel.ItemsSource = _contexto.Responsaveis.ToList();
            cbResponsavel.DisplayMemberPath = "Nome";
            cbResponsavel.SelectedValuePath = "Id";
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validação
                if (cbProduto.SelectedValue == null)
                {
                    MessageBox.Show("Selecione um produto.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(txtQuantidade.Text, out int quantidade) || quantidade <= 0)
                {
                    MessageBox.Show("Informe uma quantidade válida.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (quantidade > ((Produto)cbProduto.SelectedItem).Quantidade)
                {
                    MessageBox.Show("Quantidade maior que o estoque disponível!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int produtoId = (int)cbProduto.SelectedValue;
                int? responsavelId = cbResponsavel.SelectedValue as int?;

                var produto = _contexto.Produtos.Find(produtoId);
                if (produto == null)
                {
                    MessageBox.Show("Produto não encontrado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Atualiza estoque
                produto.Quantidade -= quantidade;

                // Atualiza reposição conforme o CheckBox
                produto.Reposicao = chkReposicao.IsChecked == true ? (byte)1 : (byte)0;

                // Cria registro de saída
                var saida = new SaidaProduto
                {
                    ProdutoId = produtoId,
                    Quantidade = quantidade,
                    DataSaida = dpDataSaida.SelectedDate ?? DateTime.Now,
                    TipoSaida = string.IsNullOrWhiteSpace(txtTipoSaida.Text) ? "Não informado" : txtTipoSaida.Text,
                    ResponsavelId = responsavelId
                };

                _contexto.SaidaProduto.Add(saida);
                _contexto.SaveChanges();

                MessageBox.Show("Saída registrada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao registrar saída:\n{ex.InnerException?.Message ?? ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}