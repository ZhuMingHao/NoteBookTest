using System.Collections.Generic;
using System.ComponentModel;

namespace NoteBookTest
{
    public class Notes : INotifyPropertyChanged
    {
        public int created { get; set; }


        //public string title { get; set; }

        private string Title;

        public string title
        {
            get { return Title; }
            set
            {
                Title = value;
                NotifyPropertyChanged("title");

            }
        }
        public string text { get; set; }
        public int id { get; set; }
        public int updated { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            }
        }


    }

    public class baseNotes
    {
        public List<Notes> notes { get; set; }

    }
}