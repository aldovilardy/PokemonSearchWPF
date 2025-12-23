using PokeApiNet;
using PokeApiNet.Models;
using PokemonSearchWPF.Dtos;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace PokemonSearchWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Timer to delay search execution
        /// </summary>
        private DispatcherTimer searchTimer;

        /// <summary>
        /// PokeApi client instance
        /// </summary>
        private PokeApiClient pokeApi;

        public MainWindow()
        {
            InitializeComponent();
            titleBar.MouseLeftButtonDown += (sender, e) => DragMove();           

            Task.Run(async () => { pokeApi = new(); });
                        
            searchTimer = new() { Interval = TimeSpan.FromSeconds(1) };
            searchTimer.Tick += OnSearchTimerTick;
        }

        /// <summary>
        /// Handles the Tick event of the searchTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnSearchTimerTick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            Search();
        }

        /// <summary>
        /// Handles the Click event of the ButtomExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtomExit(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        /// <summary>
        /// Handles the Click event of the ButtonMinimize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonMinimizeClick(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        /// <summary>
        /// Handles the MouseEnter event of the ButtonMinimize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonMinimizeMouseEnter(object sender, MouseEventArgs e) => ButtonMinimize.Background = new SolidColorBrush(Colors.Yellow);

        /// <summary>
        /// Handles the MouseLeave event of the ButtonExitMouseEnter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonExitMouseEnter(object sender, MouseEventArgs e) => ButtonExit.Background = new SolidColorBrush(Colors.Red);

        /// <summary>
        /// Searches for Pokémon based on the text in the search box.
        /// </summary>
        private async void Search()
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
                return;

            var result = await pokeApi.SearchNamedResourceAsync<Pokemon>(txtSearch.Text.ToLower());

            if (result == null)
                return;

            var pokemonList = new List<PokemonDto>();

            foreach (var item in result.Results)
            {
                var pokemon = await pokeApi.GetResourceAsync<Pokemon>(item.Name);

                if (pokemon == null)
                    continue;

                pokemonList.Add(new()
                {
                    Id = pokemon.Id.ToString(),
                    Order = $"Order: {pokemon.Order}",
                    Name = pokemon.Name,
                    Image = pokemon.Sprites.FrontDefault ?? "/Assets/missingno.png",
                    Type = $"Type: {string.Join(", ", pokemon.Types.Select(t => t.Type.Name))}",
                    Weight = $"Weight: {pokemon.Weight}",
                    Height = $"Height: {pokemon.Height}"
                });
            }

            ListPokemon.ItemsSource = pokemonList;
        }

        /// <summary>
        /// Handles the TextChanged event of the TxtSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }
    }
}