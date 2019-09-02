using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NoteBookTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private dynamic jsonObj;
        public string editpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Notas.json");
        public ObservableCollection<Notes> Mynotes;
        public MainPage()
        {
            this.InitializeComponent();
            
            string json = File.ReadAllText(editpath);
            jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

        }

        private void Titulo_TextChanged(object sender, TextChangedEventArgs e)
        {
            #region

            int indice = listNotas.SelectedIndex;
            jsonObj["notes"][indice]["title"] = titulo.Text;
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj);
            editor.TextDocument.SetText(Windows.UI.Text.TextSetOptions.None, output);
            File.WriteAllText(editpath, output);
            #endregion

        }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        public string editpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Notas.json");
        public ObservableCollection<Notes> Mynotes { get; set; }
        public ViewModel()
        {
            LoadUpdate();
            SetSelectIndex(0);
        }

        private void SetSelectIndex(int index)
        {
            SelectItem = Mynotes[index];
        }
        private void LoadUpdate()
        {
            using (StreamReader file = File.OpenText(editpath))
            {
                var json = file.ReadToEnd();
                baseNotes mainnotes = JsonConvert.DeserializeObject<baseNotes>(json);
                Mynotes = new ObservableCollection<Notes>();

                foreach (var item in mainnotes.notes)
                {
                    Mynotes.Add(new Notes { title = item.title });
                }

            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Notes _selectItem;

        public event PropertyChangedEventHandler PropertyChanged;

        public Notes SelectItem
        {
            get
            {
                return _selectItem;
            }
            set
            {
                _selectItem = value;
                OnPropertyChanged();
            }
        }
    }
}
