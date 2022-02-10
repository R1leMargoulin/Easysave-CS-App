using EasySave.Model;
using System;
using System.Collections.Generic;
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

namespace EasySave.View
{
    /// <summary>
    /// Interaction logic for CreateBackup.xaml
    /// </summary>
    public partial class CreateBackup : Window

    {
        private static CreateBackup home = null;
        public CreateBackup()
        {
            home = this;
            InitializeComponent();
          
        }

        public static CreateBackup GetPage()
        {
            if(home!= null)
                return home;
            return new CreateBackup();  
        }

        private void CreateBackupAdd( object sender, RoutedEventArgs e)
        {
            Backup backup = new Backup();
            backup.Name = home.Name.Text;
            backup.DirectorySource = home.Source.Text;
            backup.DirectoryTarget = home.Target.Text;
            backup.BackupType = BackupType.Complet;
            
            List<Backup> list = MainWindow.ListBackup();

            list.Add(backup);
            MainWindow.SaveBackup(list);
            MainWindow.GetPage().Refresh();
            Close();
            
            

        }
    }
}
