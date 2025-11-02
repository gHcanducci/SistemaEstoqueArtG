using InfraEstrutura.Data;
using InfraEstrutura.Entidades;
using Repositório.Classes;
using Repositorio.Classes;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SistemaEstoque
{
    public partial class ResponsaveisWindow : Window
    {
        private readonly ResponsavelRepositorio _responsavelRepositorio;
        private Responsavel? _responsavelSelecionado;

        public ResponsaveisWindow()
        {
            InitializeComponent();

            var contexto = new ProjetoContext();
            _responsavelRepositorio = new ResponsavelRepositorio(contexto);
        }

        private void CarregarDados(object sender, RoutedEventArgs e)
        {
            try
            {
                dgResponsaveis.ItemsSource = _responsavelRepositorio.ObterTodos().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar responsáveis: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SalvarResponsavel(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtSetor.Text))
                {
                    MessageBox.Show("Preencha todos os campos antes de salvar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var novo = new Responsavel
                {
                    Nome = txtNome.Text.Trim(),
                    Setor = txtSetor.Text.Trim()
                };

                _responsavelRepositorio.Adicionar(novo);
                _responsavelRepositorio.Salvar();

                MessageBox.Show("Responsável adicionado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                LimparCampos();
                CarregarDados(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar responsável: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditarResponsavel(object sender, RoutedEventArgs e)
        {
            if (_responsavelSelecionado == null)
            {
                MessageBox.Show("Selecione um responsável na lista para editar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _responsavelSelecionado.Nome = txtNome.Text.Trim();
                _responsavelSelecionado.Setor = txtSetor.Text.Trim();

                _responsavelRepositorio.Atualizar(_responsavelSelecionado);
                _responsavelRepositorio.Salvar();

                MessageBox.Show("Responsável atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                LimparCampos();
                CarregarDados(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao editar responsável: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExcluirResponsavel(object sender, RoutedEventArgs e)
        {
            if (_responsavelSelecionado == null)
            {
                MessageBox.Show("Selecione um responsável para excluir.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Verifica se o responsável está vinculado a entradas ou saídas
                bool possuiVinculos = _responsavelRepositorio.ExisteVinculos(_responsavelSelecionado.Id);

                if (possuiVinculos)
                {
                    MessageBox.Show(
                        "Não é possível excluir este responsável, pois ele está vinculado a entradas ou saídas de produtos.",
                        "Aviso",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                if (MessageBox.Show($"Deseja realmente excluir o responsável '{_responsavelSelecionado.Nome}'?",
                                    "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _responsavelRepositorio.Remover(_responsavelSelecionado);
                    _responsavelRepositorio.Salvar();

                    MessageBox.Show("Responsável excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                    LimparCampos();
                    CarregarDados(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir responsável: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgResponsaveis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _responsavelSelecionado = dgResponsaveis.SelectedItem as Responsavel;

            if (_responsavelSelecionado != null)
            {
                txtNome.Text = _responsavelSelecionado.Nome;
                txtSetor.Text = _responsavelSelecionado.Setor;
            }
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtSetor.Text = "";
            dgResponsaveis.SelectedItem = null;
            _responsavelSelecionado = null;
        }
    }
}