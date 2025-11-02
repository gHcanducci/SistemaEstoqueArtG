using InfraEstrutura.Data;
using Repositório.Classes;
using Repositorio.Classes;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SistemaEstoque
{
    public partial class GerenciarEntradasSaidasWindow : Window
    {
        private readonly ProjetoContext _contexto;
        private readonly ProdutoRepositorio _produtoRepositorio;
        private readonly EntradaProdutoRepositorio _entradaRepositorio;
        private readonly SaidaProdutoRepositorio _saidaRepositorio;

        public GerenciarEntradasSaidasWindow()
        {
            InitializeComponent();

            try
            {
                // Inicializa contexto e repositórios (assume construtores que aceitam contexto)
                _contexto = new ProjetoContext();
                _produtoRepositorio = new ProdutoRepositorio(_contexto);
                _entradaRepositorio = new EntradaProdutoRepositorio(_contexto);
                _saidaRepositorio = new SaidaProdutoRepositorio(_contexto);

                CarregarProdutos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inicializar a janela: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void CarregarProdutos()
        {
            try
            {
                var produtos = _produtoRepositorio.ObterTodos()
                    .Select(p => new { p.Id, p.Nome })
                    .OrderBy(p => p.Nome)
                    .ToList();

                cbProduto.ItemsSource = produtos;
                cbProduto.DisplayMemberPath = "Nome";
                cbProduto.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar produtos: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Consultar_Click(object sender, RoutedEventArgs e)
        {
            if (cbProduto.SelectedValue == null)
            {
                MessageBox.Show("Selecione um produto.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cbTipo.SelectedItem == null)
            {
                MessageBox.Show("Selecione o tipo de movimentação (Entradas ou Saídas).", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int produtoId;
            try
            {
                produtoId = Convert.ToInt32(cbProduto.SelectedValue);
            }
            catch
            {
                MessageBox.Show("Produto selecionado inválido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string tipo = ((ComboBoxItem)cbTipo.SelectedItem).Content.ToString();

            try
            {
                if (tipo == "Entradas")
                {
                    var entradas = _entradaRepositorio.ObterPorProduto(produtoId)
                        .Select(e => new
                        {
                            Data = e.DataEntrada.ToString("yyyy-MM-dd HH:mm"), // formato legível
                            Quantidade = e.Quantidade,
                            Responsavel = e.Responsavel != null ? e.Responsavel.Nome : "—"
                        })
                        .OrderByDescending(e => e.Data)
                        .ToList();

                    dgMovimentacoes.ItemsSource = entradas;
                }
                else // Saídas
                {
                    var saidas = _saidaRepositorio.ObterPorProduto(produtoId)
                        .Select(s => new
                        {
                            Data = s.DataSaida.ToString("yyyy-MM-dd HH:mm"),
                            Quantidade = s.Quantidade,
                            TipoSaida = string.IsNullOrWhiteSpace(s.TipoSaida) ? "—" : s.TipoSaida, // ✅ NOVO CAMPO
                            Responsavel = s.Responsavel != null ? s.Responsavel.Nome : "—"
                        })
                        .OrderByDescending(s => s.Data)
                        .ToList();

                    dgMovimentacoes.ItemsSource = saidas;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao consultar movimentações: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}