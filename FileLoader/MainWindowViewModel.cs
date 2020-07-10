using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace FileLoader
{
    public class MainWindowViewModel:ViewModelBase
    {
        private ObservableCollection<string> log = new ObservableCollection<string>();
        public ObservableCollection<string> Log
        {
            get { return log; }
            set
            {
                log = value;
                RaisePropertyChanged("Log");
            }
        }

        private string logStr;
        public string LogStr
        {
            get { return logStr; }
            set
            {
                logStr = value;
                RaisePropertyChanged("LogStr");
            }
        }
        private string filename="";
        public string FileName
        {
            get { return filename; }
            set
            {
                filename = value;
                RaisePropertyChanged("FileName");
            }
        }

        private string cstr="mongodb://localhost:27017";
        public string Constr
        {
            get { return cstr; }
            set
            {
                cstr = value;
                RaisePropertyChanged("Constr");
            }
        }
        public MainWindowViewModel()
        {
            Model.Log.CollectionChanged += Log_CollectionChanged;
        }

        private void Log_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Log = Model.Log;
            foreach(var line in e.NewItems)
            {
                LogStr += string.Format("{0}\r\n", line);
            }
        }

        public ICommand OpenFileCommand
        {
            get { return new RelayCommand(ExecuteOpenFile); }
        }

        private void ExecuteOpenFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Текстовые файлы | *.txt;*.csv;";
            dlg.Multiselect = false;
            if(dlg.ShowDialog()==true)
            {
                FileName = dlg.FileName;
            }
        }


        public ICommand WriteToDBCommand
        {
            get { return new RelayCommand(ExecuteWriteToDB,(!string.IsNullOrEmpty(FileName) && !string.IsNullOrEmpty(Constr))); }
        }

        private async void ExecuteWriteToDB()
        {
            //await Model.WriteToDatabase(Constr, await Task.Run(()=>Model.LoadData(FileName)));
            await Model.WriteGeoJsonToDatabase(Constr, await Task.Run(() => Model.LoadData(Model.LoadFileData(FileName))));
        }

        public ICommand CloseCommand
        {
            get { return new RelayCommand(ExecuteClose); }
        }

        private void ExecuteClose()
        {
            Environment.Exit(0);
        }
    }
}
