using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasySave.Model;
using EasySave.View;


namespace EasySave.Controllers
{
    class MenuControl
    {
        private MenuModel model;
        private ViewMenu menu;
        readonly BackupControllers controllerbackup;
       
        public MenuControl(MenuModel mdl, ViewMenu view)
        {
            menu = view;
            model = mdl;
            controllerbackup = new BackupControllers();
        }

        public void Start()
        {
            Affichage();
        }

        public void Affichage()
        {
            if(model.language == "fr")
            {
                AffichageFr();
            }
            else if (model.language == "en")
            {
                AffichageEn();
            }
        }
        

        public void AffichageFr()
        {

            switch (model.GetMenuView())
            {
                case 0: // Affichage accueil
                    controllerbackup.ListBackup();
                        menu.Print
                        (
                        "Veuillez entrer un chiffre correspondant aux propositions: \n \n \n" +
                        "[1] - créer une sauvegarde\n" +
                        "[2] - éxecuter une sauvegarde\n" +
                        "[3] - Montrer les détails d'une sauvegarde\n" +
                        "[4] - Supprimer une sauvegarde\n" +
                        "[5] - Modifier une sauvegarde\n" +
                        "[6] - Changer la langue\n" +
                        "[7] - Fermer l'application\n"
                        );


                    int choice = Convert.ToInt32(menu.Ask());
                    if (choice == 1 || choice == 2 || choice == 3 || choice == 4 || choice == 5 || choice == 6 || choice == 7)
                    {
                        ChangeViewMenuInput(choice);
                    }

                    break;

                case 1: //View of mode 1, creating of a new save
                    Backup backup = new Backup();
                    
                    menu.Print("Entrez un nom pour la sauvegarde"); //stringmenu 1.1
                    var nomSave = menu.Ask();
                    backup.Name = nomSave;


                    menu.Print("Entrez le chemin de la ressource a sauvegarder"); //stringmenu 1.2                 
                    var sourcePath = menu.Ask();
                    backup.DirectorySource = sourcePath;

                    menu.Print("Entrez le chemin de l'emplacement de la sauvegarde"); //stringmenu 1.3
                    var savePath = menu.Ask();
                    backup.DirectoryTarget = savePath;

                    menu.Print
                        (
                            "Entrez un chiffre pour choisir type d'enregistrement:\n" +
                            "1 - complet (on resauvegarde tout l'élément)\n" +
                            "2 - différentiel (sauvegarde seulement les changements lorsqu'il y en a)"
                        ); //stringmenu 1.4
                    int choixType = Convert.ToInt32(menu.Ask());


                    if (choixType == 1)
                    {
                        backup.BackupType = BackupType.Complet;
                        controllerbackup.AddBackup(backup); //aremplacer par le change du SaveWorkModel
                    }
                    if (choixType == 2)
                    {
                        backup.BackupType = BackupType.Differentielle;
                        controllerbackup.AddBackup(backup); //aremplacer par le change du SaveWorkModel
                    }

                    ChangeViewMenuInput(0);
                    Affichage();
                    break;

                case 2: // display of mode 2, executing a save
                    
                    menu.Print("Affichage des sauvegardes disponibles"); //stringmenu 2.1
                    menu.Print(GetAllBackup());
                    menu.Print("\n quelle sauvegarde voulez vous executer?"); //Stringmenu 2.2
                    int Executeindex = Convert.ToInt32(menu.Ask());
                    ListBackup(Executeindex).DirectoryCopy(ListBackup(Executeindex).DirectorySource, ListBackup(Executeindex).DirectoryTarget);
                    ChangeViewMenuInput(0);
                    Affichage();

                    break;
                case 3: //Display Backup Informations
                        menu.Print("Affichage des sauvegardes disponibles"); //stringmenu Displaysave

                    menu.Print(GetAllBackup());
                    menu.Print("\n de quelle sauvegarde voulez vous les informations?"); //stringmenu 3.1
                    int Displayindex = Convert.ToInt32(menu.Ask());
                    ListBackup(Displayindex);
                    menu.Print("Nom :" + ListBackup(Displayindex).Name + "\n" + "Répertoire Source : " + ListBackup(Displayindex).DirectorySource + "\n" + "Répertoire cible : " + ListBackup(Displayindex).DirectoryTarget + "\n" + "Type : " + ListBackup(Displayindex).BackupType);

                    menu.Ask();
                    ChangeViewMenuInput(0);
                    Affichage();

                    break;

                case 4:

                    menu.Print("Affichage des sauvegardes disponibles"); 
                    menu.Print(GetAllBackup());          
                    menu.Print("\n quelle sauvegarde voulez vous supprimer?"); 
                    var deleteindex = Convert.ToInt32(menu.Ask());
                    controllerbackup.DeleteBackup(deleteindex);
                    ChangeViewMenuInput(0);
                    Affichage();

                    break;

                case 5://Update
                        menu.Print("Affichage des sauvegardes disponibles"); //stringmenu DisplaySave

                    menu.Print(GetAllBackup());
                    menu.Print("\n quelle sauvegarde voulez vous modifier?"); //stringmenu 5.1
                    
                    int updateIndex = Convert.ToInt32(menu.Ask());

                        menu.Print("Que voulez vous modifier dans la sauvegarde ?"); //stringmenu 5.2
                        menu.Print("1 - Nom de la sauvegarde \n2 - Chemin de la ressource a sauvegarder\n3 - chemin de l'emplacement de la sauvegarde\n4 - Type de sauvegarde\n"); //stringmenu 5.3


                    int whatToModifyChoice = Convert.ToInt32(menu.Ask());
                    ListBackup(updateIndex);
                    if (whatToModifyChoice == 1)
                    {
                        menu.Print("Entrez le nouveau nom \n");
                        ListBackup(updateIndex).Name = menu.Ask();
                        controllerbackup.UpdateBackup(updateIndex, ListBackup(updateIndex));
                    }
                    if (whatToModifyChoice == 2)
                    {
                        menu.Print("Entrez le nouveau répertoire source \n");
                        ListBackup(updateIndex).DirectorySource = menu.Ask();
                        controllerbackup.UpdateBackup(updateIndex, ListBackup(updateIndex));
                    }
                    if (whatToModifyChoice == 3)
                    {
                        menu.Print("Entrez le nouveau répertoire cible \n");
                        ListBackup(updateIndex).DirectoryTarget = menu.Ask();
                        controllerbackup.UpdateBackup(updateIndex, ListBackup(updateIndex));
                    }
                    if (whatToModifyChoice == 4)
                    {
                        menu.Print("Entrez le nouveau type de sauvegarde \n");
                        ListBackup(updateIndex).BackupType = (BackupType)Convert.ToInt32(menu.Ask());
                        controllerbackup.UpdateBackup(updateIndex, ListBackup(updateIndex));
                    }
                    
                    ChangeViewMenuInput(0);
                    Affichage();

                    break;

                case 6:
                        menu.Print("Quel langage voulez vous afficher?");//stringmenu 6.1
                    int languageCounter = 1;
                    foreach (String language in model.GetLanguageList())
                    {
                        menu.Print(Convert.ToString(languageCounter) + " " + language);
                        languageCounter++;
                    }
                    int languageChoice = Convert.ToInt32(menu.Ask());
                    model.SetLanguage(model.GetLanguageList()[languageChoice - 1]);//select of the right language into the list in the model class
                    ChangeViewMenuInput(0);
                    Affichage();
                    break;

                case 7:
                    menu.Print("Etes vous sur de vouloir quitter l'application?\n\n1 - oui\n2 - non\n"); //stringmenu 8.1

                    int ExitChoice = Convert.ToInt32(menu.Ask());

                    if (ExitChoice == 1)
                    {
                        return;
                    }
                    else
                    {
                        ChangeViewMenuInput(0);
                        Affichage();
                    }

                    break;
            }
        }

        public void AffichageEn()
        {
            Backup backup = new Backup();
            switch (model.GetMenuView())
            {
                case 0: // Affichage accueil
                        menu.Print
                        (
                        "Please, Enter a number corresponding to the menu: \n \n \n" +
                        "[1] - create a save\n" +
                        "[2] - execute a save\n" +
                        "[3] - show a save details\n" +
                        "[4] - delete a save\n" +
                        "[5] - modify a save\n" +
                        "[6] - change language settings\n" +                     
                        "[7] - close the app\n"
                        );
                    int choice = Convert.ToInt32(menu.Ask());
                    if (choice == 1 || choice == 2 || choice == 3 || choice == 4 || choice == 5 || choice == 6 || choice == 7)
                    {
                        ChangeViewMenuInput(choice);
                    }

                    break;

                case 1: //View of mode 1, creating of a new save
                    
                    menu.Print("Type a name for your save");
                    var nomSave = menu.Ask();
                    backup.Name = nomSave;

                    menu.Print("Type the path of the element you want to save");
                    var sourcePath = menu.Ask();
                    backup.DirectorySource = sourcePath;

                    menu.Print("Type the path to save in");
                    var savePath = menu.Ask();
                    backup.DirectoryTarget = savePath;

                    menu.Print
                        (
                            "Type the number to choose a type of saving:\n" +
                            "1 - complete (saving of all of the element)\n" +
                            "2 - diffential (saving of changes only if they exists)"
                        );                    
                    int choixType = Convert.ToInt32(menu.Ask());

                    if (choixType == 1)
                    {
                        backup.BackupType = BackupType.Complet;
                        controllerbackup.AddBackup(backup);
                    }
                    if (choixType == 2)
                    {
                        backup.BackupType = BackupType.Differentielle;
                        controllerbackup.AddBackup(backup);
                    }
                    ChangeViewMenuInput(0);
                    Affichage();
                    break;

                   
                case 2: // display of mode 2, executing a save
 
                    menu.Print("Display of available saves");
                    menu.Print(GetAllBackup());
                    menu.Print("\n\n\n wich save dou you want to execute?");
                    int Exeindex = Convert.ToInt32(menu.Ask());
                    ListBackup(Exeindex).DirectoryCopy(ListBackup(Exeindex).DirectorySource, ListBackup(Exeindex).DirectoryTarget);
                    ChangeViewMenuInput(0);
                    Affichage();

                    break;
                case 3:
                    menu.Print("Display of available saves");
                    menu.Print(GetAllBackup());
                    menu.Print("\n from wich save would you want informations?");
                    int Showindex = Convert.ToInt32(menu.Ask());
                    ListBackup(Showindex);
                    menu.Print("Nom :" + ListBackup(Showindex).Name + "\n" + "Source Directory : " + ListBackup(Showindex).DirectorySource + "\n" + "Target Directory : " + ListBackup(Showindex).DirectoryTarget + "\n" + "Type : " + ListBackup(Showindex).BackupType);
                    menu.Ask();
                    ChangeViewMenuInput(0);
                    Affichage();

                    break;

                case 4:
                    menu.Print("Display of available saves");
                    menu.Print(GetAllBackup());
                    menu.Print("\n wich save would you want to delete?");
                    var deleteindex = Convert.ToInt32(menu.Ask());
                    controllerbackup.DeleteBackup(deleteindex);
                    ChangeViewMenuInput(0);
                    Affichage();

                    break;

                case 5:
                    menu.Print("Display of available saves");
                    menu.Print(GetAllBackup());
                    menu.Print("\n wich save would you want to delete?");
                    int updateIndex = Convert.ToInt32(menu.Ask());

                    menu.Print("What would you want to modify in this save?");
                    menu.Print("1 - Name of the save\n2 - path of the element to save\n3 - path to save in\n4 - Type of the save\n");
                    int whatToModifyChoice = Convert.ToInt32(menu.Ask());
                    ListBackup(updateIndex);
                    if (whatToModifyChoice == 1)
                    {
                        menu.Print("Enter the new name \n");
                        ListBackup(updateIndex).Name = menu.Ask();
                        controllerbackup.UpdateBackup(updateIndex, ListBackup(updateIndex));
                    }
                    if (whatToModifyChoice == 2)
                    {
                        menu.Print("Enter the new source directory \n");
                        ListBackup(updateIndex).DirectorySource = menu.Ask();
                        controllerbackup.UpdateBackup(updateIndex, ListBackup(updateIndex));
                    }
                    if (whatToModifyChoice == 3)
                    {
                        menu.Print("Enter the new target directory \n");
                        ListBackup(updateIndex).DirectoryTarget = menu.Ask();
                        controllerbackup.UpdateBackup(updateIndex, ListBackup(updateIndex));
                    }
                    if (whatToModifyChoice == 4)
                    {
                        menu.Print("Enter the new Backup type \n");
                        ListBackup(updateIndex).BackupType = (BackupType)Convert.ToInt32(menu.Ask());
                        controllerbackup.UpdateBackup(updateIndex, ListBackup(updateIndex));
                    }

                    ChangeViewMenuInput(0);
                    Affichage();


                    break;

                case 6:
                    menu.Print("wich language would you want to display?");
                    int languageCounter = 1;
                    foreach (String language in model.GetLanguageList()) //foreach languages available, we will display.
                    {
                        menu.Print(Convert.ToString(languageCounter) + " " + language);
                        languageCounter++;
                    }
                    int languageChoice = Convert.ToInt32(menu.Ask());
                    model.SetLanguage(model.GetLanguageList()[languageChoice - 1]);//select of the right language into the list in the model class
                    ChangeViewMenuInput(0);
                    Affichage();
                    break;

                //case 7:
                //    menu.Print("wich language would you want to display?");
                //    int languageCounter = 1;
                //    foreach (String language in model.GetLanguageList()) //foreach languages available, we will display.
                //    {
                //        menu.Print(Convert.ToString(languageCounter) + " " + language);
                //        languageCounter++;
                //    }
                //    int languageChoice = Convert.ToInt32(Console.ReadLine());
                //    model.SetLanguage(model.GetLanguageList()[languageChoice - 1]);//select of the right language into the list in the model class
                //    ChangeViewMenuInput(0);
                //    Affichage();
                //    break;

                case 8:
                    if (model.language == "en")
                    {
                        menu.Print("Are you sure that you want to exit from the app?\n\n1 - yes\n2 - no\n");
                    }
                    int ExitChoice = Convert.ToInt32(menu.Ask());

                    if (ExitChoice == 1)
                    {
                        return;
                    }
                    else
                    {
                        ChangeViewMenuInput(0);
                        Affichage();
                    }

                    break;
            }
        }

        public string GetAllBackup()
        {
            var enableBackup = controllerbackup.DisplayAllBackup(this.controllerbackup.BackupList);

            return enableBackup;
        }

       


            public Backup ListBackup(int index)
        {
            
            Backup backup = controllerbackup.BackupList[index - 1];
            return backup;
        }

        public void ChangeViewMenuInput(int a)
        {
            menu.Clear();
            model.SetMenuView(a);
            Affichage();
        }
    }
}
