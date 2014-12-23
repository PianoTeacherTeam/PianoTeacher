using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PDFW;
using PDFW.Classes;
using PDFW.SL;
using PDFW.SL.Classes;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Media;
using System.Timers;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace PianoTeacher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow_ViewModel viewModel = new MainWindow_ViewModel();
        public MainWindow()
        {
            this.DataContext = this.viewModel;
            
            this.Resources.Add("viewModel", this.viewModel);

            this.Resources.Add("notaIsPressedToButtonColorValueConverter", new NotaIsPressedToButtonColorValueConverter());
            this.Resources.Add("notaIsPressedToButtonForegroundValueConverter", new NotaIsPressedToButtonForegroundValueConverter());
            this.Resources.Add("playingNotaToNotaIconValueConverter", new PlayingNotaToNotaIconValueConverter());
            this.Resources.Add("playingNotaToMarginValueConverter", new PlayingNotaToMarginValueConverter());

            this.Resources.Add("playStateToButtonColorValueConverter", new PlayStateToButtonColorValueConverter());
            this.Resources.Add("playStateToButtonForegroundValueConverter", new PlayStateToButtonForegroundValueConverter());
            this.Resources.Add("isNextToBorderBrushValueConverter", new IsNextToBorderBrushValueConverter());


            
            InitializeComponent();
        }

        private void btnPlaySong_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.PlayCurrentSong(Utils.HandleCompleted);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var nota = ((FrameworkElement)sender).Tag as NotaModel;
            this.viewModel.PlayNota(nota, Utils.HandleCompleted);
        }

        private void btnPlayNotaButton_Click(object sender, RoutedEventArgs e)
        {
            var nota = ((FrameworkElement)sender).Tag as PlayNotaModel;
            this.viewModel.Play(nota, true, Utils.HandleCompleted);
        }

    }   



    public class MainWindow_ViewModel : BaseViewModel
    {
        public ObservableCollection<NotaModel> NotaList1 { get { return GetValue<ObservableCollection<NotaModel>>("NotaList1"); } set { SetValue("NotaList1", value); } }
        public ObservableCollection<NotaModel> NotaList2 { get { return GetValue<ObservableCollection<NotaModel>>("NotaList2"); } set { SetValue("NotaList2", value); } }

        public string CurrentNota { get { return GetValue<string>("CurrentNota"); } set { SetValue("CurrentNota", value); } }
        //public PlayNotaModel CurrentPlayNota { get { return GetValue<PlayNotaModel>("CurrentPlayNota"); } set { SetValue("CurrentPlayNota", value); } }

        public ObservableCollection<SongModel> Songs { get { return GetValue<ObservableCollection<SongModel>>("Songs"); } set { SetValue("Songs", value); } }
        public SongModel CurrentSong { get { return GetValue<SongModel>("CurrentSong"); } set { SetValue("CurrentSong", value); } }

        public IEnumerable<PlayNotaModel> CurrentSongNotaList { get { return GetValue<IEnumerable<PlayNotaModel>>("CurrentSongNotaList"); } set { SetValue("CurrentSongNotaList", value); } }

        public bool IsPlaying { get { return GetValue<bool>("IsPlaying"); } set { SetValue("IsPlaying", value); } }

        public MainWindow_ViewModel()
        {
            this.NotaList1 = new ObservableCollection<NotaModel>();
            this.NotaList2 = new ObservableCollection<NotaModel>();

            InitCompact();

            this.Songs = new ObservableCollection<SongModel>();

            InitSongs();

            

            this.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == "CurrentNota")
                {
                    UpdateIsPressed();
                }
                else if (a.PropertyName == "CurrentSong")
                {
                    LoadCurrentSondNotaList();
                }
            };

            LoadCurrentSondNotaList();
        }

        private void LoadCurrentSondNotaList()
        {
            IEnumerable<PlayNotaModel> notaList = new PlayNotaModel[] { };
            if (CurrentSong != null)
                notaList = ParseNotes(CurrentSong.NotaList);
            LoadNotaList(notaList);
        }

        private void InitSongs()
        {
            this.Songs.Add(new SongModel() { 
                Code = "DahaDunAnnemizin", Description = "Daha Dün Annemizin", 
                NotaList = "DO-DO-SOL-SOL-LA-LA-SOL:2-FA-FA-Mİ-Mİ-RE-RE-DO:2" });

            this.Songs.Add(new SongModel() { 
                Code = "Postaci", Description = "Bak Postacı Geliyor",
                NotaList = "SOL-FA-SOL-LA-SOL-FA-Mİ:2-RE-FA-Mİ-RE-Mİ:2-" +
                           "SOL-FA-SOL-LA-SOL-FA-Mİ:2-RE-FA-Mİ-RE-DO:2"
            });


            //this.Songs.Add(new SongModel() { 
            //    Code = "Criminal", Description = "Kriminal",
            //    NotaList = "fa-mi-fa-mi-fa-mi-fa-sol-fa-mi-mi-re-mi-re-mi-re-mi-fa-mi-re-do-re-do-re-do-re-mi-fa-mi-re-do-re-do-re-do-do-do-do"
            //});


            this.Songs.Add(new SongModel() { 
                Code = "BirSarkisin", Description = "Bir Şarkısın Sen",
                NotaList = "sol:2 - la:2 - sol:0,5 - fa:0,5 - mi:2 - si:0,5 - do:0,5 - la:0,5 - sol:0,5 - fa:0,5 - sol:0,5 - la:2-" +
                            "sol:2 - la:2 - sol:0,5 - fa:0,5 - mi:2- si:0,5 - do:0,5 - la:0,5 - sol:0,5 - fa:0,5 - sol:0,5 - mi:2-" +
                            "si:2 - do:2 - la:0,5 - sol:0,5 - la:2 - sol:0,5 - la:0,5 - fa:0,5 - sol:0,5 - fa:0,5 - sol:0,5 - mi:2-" +
                            "si:2 - do:2 - la:0,5 - sol:0,5 - la:2 - sol:0,5 - la:0,5 - fa:0,5 - sol:0,5 - fa:0,5 - sol:0,5 - mi:2"
            });


            this.Songs.Add(new SongModel() { 
                Code = "SuperBaba", Description = "Süper Baba",
                NotaList = "re:2-fa:2-mi:0,5-fa:0,5-mi:2-mi:0,5-mi:0,5-mi:0,5-mi-fa-sol:0,5-fa:0,5-re-re-re-re-mi-fa-mi-re-fa-" +
                           "re-fa-mi-fa-mi-mi-mi-mi-mi-fa-sol-fa-re-re-re-re-mi-fa-mi-re-re"
            });


            this.CurrentSong = this.Songs.FirstOrDefault();
        }       


        public void InitCompact()
        {
            this.NotaList1.Clear();
            this.AddNotaList(this.NotaList1,
                "DO", "RE", "Mİ", "FA", "SOL", "LA", "Sİ");


            this.AddNotaList(this.NotaList2,

                "Mİ2", "FA2", "",
                "SOL2", "LA2", "Sİ2");
        }

        public void InitFull()
        {
            this.NotaList1.Clear();
            this.AddNotaList(this.NotaList1,
                "DO", "RE", "Mİ", "FA", "SOL",
                "DO", "RE", "Mİ", "FA", "SOL", "LA", "Sİ",
                "DO", "RE", "Mİ", "FA", "SOL", "LA", "Sİ", "DO");


            this.AddNotaList(this.NotaList2,
                "DO", "RE", "",
                "Mİ", "FA", "SOL", "",
                "LA", "Sİ", "",
                "DO", "RE", "Mİ", "",
                "FA", "SOL", "",
                "LA", "Sİ", "DO");
        }


        public void AddNotaList(ObservableCollection<NotaModel> notaList, params string[] notaCodes)
        {
            if (notaCodes == null)
                return;

            foreach (var notaCode in notaCodes)
            {
                notaList.Add(new NotaModel() { Code = notaCode });
            }
        }


        public decimal ParseDecimal(string str, decimal defaultValue)
        {
            var val = str.NVLDecimal(defaultValue);
            return val;
        }

        public PlayNotaModel[] ParseNotes(string notaString)
        {
            var result = new List<PlayNotaModel>();

            var notaList = notaString.NVLStr().Trim().SplitString('-', '(', ')');

            int i=0;
            foreach (var strNota in notaList)
            {
                i += 1;

                var parts = strNota.SplitString(':', '(', ')').Select(p => p.NVLStr().Trim()).ToArray();
                var notaCode = (parts.Length <= 0 ? "" : (parts[0])).NVLStr();
                var notaDuration = (parts.Length <= 1 ? 1 : ParseDecimal(parts[1], 1));

                var nota = new PlayNotaModel() { Code = notaCode.ToUpper(), Duration = notaDuration };
                nota.ID = i;

                result.Add(nota);
            }

            return result.ToArray();
        }


        private void UpdateIsPressed()
        {
            var allNotaList = this.NotaList1.Union(this.NotaList2).Where(p => !string.IsNullOrWhiteSpace(p.Code));
            foreach (var nota in allNotaList)
            {
                nota.IsPressed = (this.CurrentNota == nota.Code);
            }
        }

        public void PlaySong(string notaString, onCompletedEventHandler onCompleted)
        {
            var notaList = ParseNotes(notaString);            
            PlaySong(notaList, onCompleted);
        }

        public void LoadNotaList(IEnumerable<PlayNotaModel> notaList)
        {
            this.CurrentSongNotaList = notaList;
            UpdateNext(null);
        }

        public void PlaySong(PlayNotaModel[] notaList, onCompletedEventHandler onCompleted)
        {
            this.IsPlaying = true;
            try
            {
                LoadNotaList(notaList);
                //this.CurrentSongNotaList = notaList;

                var queue = new AsyncOperationQueue();
                queue.BreakOnError = true;

                foreach (var nota in notaList)
                {
                    queue.Enqueue<PlayNotaModel>("Play Nota: " + nota.Code.NVLStr(), nota, (pNota, onCompletedQueueItem) =>
                    {
                        var otherPlayingNota = notaList.Where(p => p.PlayState==PlayStateEnum.Playing).ToArray();
                        if (otherPlayingNota != null)
                            otherPlayingNota.ForEach(p => p.PlayState = PlayStateEnum.Played);

                        Play(pNota, true, onCompletedQueueItem);
                    });
                }

                queue.onCompleted += () =>
                {
                    this.IsPlaying = false;

                    if (queue.HasErrors)
                    {
                        queue.ExceptionList.ThrowIfRequired();
                        onCompleted.CallOnCompleted(queue.ExceptionList);
                    }
                    else
                        onCompleted.CallOnCompleted(true, null);
                };

                queue.Execute();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void PlayCurrentSong(onCompletedEventHandler onCompleted)
        {
            var song = this.CurrentSong;
            if (song == null)
                throw new Exception("Select Song!");

            var notaList = song.NotaList;
            this.PlaySong(notaList, onCompleted);
        }

        public void PlayNota(NotaModel nota, onCompletedEventHandler onCompleted)
        {
            PlayNotaModel playNotaModel = null;
            bool updateNext = false;

            var nextNota = this.GetNextNota();
            if (nextNota == null || nextNota.Code != nota.Code)
                playNotaModel = new PlayNotaModel() { Code = nota.Code, Duration = 1 };
            else
            {
                playNotaModel = nextNota;
                updateNext = true;
            }

            this.Play(playNotaModel, updateNext, onCompleted);
        }

        public Player player = new Player();
        public void Play(PlayNotaModel newNota, bool updateNext, onCompletedEventHandler onCompleted)
        {
            this.CurrentNota = newNota.Code;
            newNota.PlayState = PlayStateEnum.Playing;

            if (updateNext)
                UpdateNext(newNota);

            this.player.Play(newNota, App.CurrentApp.Dispatcher, (result, expList) =>
                {
                    if (result)
                        newNota.PlayState = PlayStateEnum.Played;

                    onCompleted.CallOnCompleted(result, expList);
                });
        }

        private void UpdateNext(PlayNotaModel newNota=null)
        {
            var nextNota = CalculateNextNota(newNota);

            this.CurrentSongNotaList.ForEach(p => p.IsNext = false);
            if (nextNota != null)
                nextNota.IsNext = true;
        }

        private PlayNotaModel CalculateNextNota(PlayNotaModel newNota)
        {
            int index=-1;
            if (newNota!=null)
                index = this.CurrentSongNotaList.IndexOfIEnumerable(newNota);

            PlayNotaModel next = this.CurrentSongNotaList.NthOrNullClass(index+1);
            return next;
        }

        public PlayNotaModel GetNextNota()
        {
            var nota = this.CurrentSongNotaList.Where(p => p.IsNext).FirstOrDefault();
            return nota;
        }
    }
     

}
