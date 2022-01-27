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
                        "[7] - Changer le format des logs\n" +
                        "[8] - Fermer l'application\n"
                        );


                    int choice = Convert.ToInt32(menu.Ask());
                    if (choice == 1 || choice == 2 || choice == 3 || choice == 4 || choice == 5 || choice == 6 || choice == 7 || choice == 8)
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
                        
                    }

                    ChangeViewMenuInput(0);
                    Affichage();
                    break;

                case 2: // display of mode 2, executing a save
                    
                    menu.Print("Affichage des sauvegardes disponibles"); //stringmenu 2.1
                    
                    var enableBackup = controllerbackup.DisplayAllBackup(this.controllerbackup.BackupList);
                    
                        menu.Print(enableBackup);
                    
                 
                        menu.Print("\n quelle sauvegarde voulez vous executer?"); //Stringmenu 2.2
                    

                    int ExecSaveindex = Convert.ToInt32(menu.Ask());
                    Backup backup1 = controllerbackup.BackupList[ExecSaveindex - 1];
                    backup1.DirectoryCopy(backup1.DirectorySource, backup1.DirectoryTarget);



                    //aremplacer ce vide par l'ajout de l'appel de la méthode du controlleur pour executer la sauvegarde

                    ChangeViewMenuInput(0);
                    Affichage();

                    break;
                case 3:
                        menu.Print("Affichage des sauvegardes disponibles"); //stringmenu Displaysave

                    var enableBackups = controllerbackup.DisplayAllBackup(this.controllerbackup.BackupList);

                    menu.Print(enableBackups);
                    menu.Print("\n de quelle sauvegarde voulez vous les informations?"); //stringmenu 3.1

                    int InfoSaveChoice = Convert.ToInt32(menu.Ask());

                    menu.Print("informatiooooooooooooooooooons");//aremplacer par l'ajout de l'appel de la méthode du controlleur pour afficher les infos de la sauvegarde

                    menu.Ask();
                    ChangeViewMenuInput(0);
                    Affichage();

                    break;

                case 4:
                        menu.Print("Affichage des sauvegardes disponibles"); //stringmenu Displaysave
                    var enableBackupss = controllerbackup.DisplayAllBackup(this.controllerbackup.BackupList);

                    menu.Print(enableBackupss);
                   

                   
                    menu.Print("\n quelle sauvegarde voulez vous supprimer?"); //stringmenu 4.1

                    var deleteindex = Convert.ToInt32(menu.Ask());
                    controllerbackup.DeleteBackup(deleteindex);

                    //aremplacer par l'ajout de l'appel de la méthode du controlleur pour afficher les infos de la sauvegarde

                    ChangeViewMenuInput(0);
                    Affichage();

                    break;

                case 5://Update
                        menu.Print("Affichage des sauvegardes disponibles"); //stringmenu DisplaySave
                    var enableBackupsss = controllerbackup.DisplayAllBackup(this.controllerbackup.BackupList);

                    menu.Print(enableBackupsss);

                    menu.Print("\n quelle sauvegarde voulez vous modifier?"); //stringmenu 5.1
                    
                    int updateIndex = Convert.ToInt32(menu.Ask());

                        menu.Print("Que voulez vous modifier dans la sauvegarde ?"); //stringmenu 5.2
                        menu.Print("1 - Nom de la sauvegarde \n2 - Chemin de la ressource a sauvegarder\n3 - chemin de l'emplacement de la sauvegarde\n4 - Type de sauvegarde\n"); //stringmenu 5.3


                    int whatToModifyChoice = Convert.ToInt32(menu.Ask());

                    if(whatToModifyChoice == 1)
                    {
                        Backup backup2 = controllerbackup.BackupList[updateIndex - 1];
                        backup2.Name = menu.Ask();
                        controllerbackup.UpdateBackup(updateIndex, backup2);
                    }
                    if (whatToModifyChoice == 2)
                    {
                        Backup backup2 = controllerbackup.BackupList[updateIndex - 1];
                        backup2.DirectorySource = menu.Ask();
                        controllerbackup.UpdateBackup(updateIndex, backup2);
                    }
                    if (whatToModifyChoice == 3)
                    {
                        Backup backup2 = controllerbackup.BackupList[updateIndex - 1];
                        backup2.DirectoryTarget = menu.Ask();
                        controllerbackup.UpdateBackup(updateIndex, backup2);
                    }
                    //if (whatToModifyChoice == 4)
                    //{
                    //    Backup backup2 = controllerbackup.BackupList[updateIndex - 1];
                    //    backup2.BackupType = Convert.ToInt32(menu.Ask());
                    //    controllerbackup.UpdateBackup(updateIndex, backup2);
                    //}
                    //aremplacer par l'ajout de l'appel de la méthode du controlleur pour modifier la sauvegarde selon ce qu'on va choisir de modifier

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

                //case 7:
                //    menu.Print("Quel format de log voulez vous selectionner?");//stringmenu 7.1
                //    int languageCounter = 1;
                //    foreach (String language in model.GetLanguageList())
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
                        "[7] - change lof format\n" +
                        "[8] - close the app\n"
                        );
                    int choice = Convert.ToInt32(menu.Ask());
                    if (choice == 1 || choice == 2 || choice == 3 || choice == 4 || choice == 5 || choice == 6 || choice == 7 || choice == 8)
                    {
                        ChangeViewMenuInput(choice);
                    }

                    break;

                case 1: //View of mode 1, creating of a new save
                    
                    menu.Print("Type a name for your save");
                    String nomSave = menu.Ask();

                    menu.Print("Type the path of the element you want to save");
                    String sourcePath = menu.Ask();

                    menu.Print("Type the path to save in");
                    String savePath = menu.Ask();

                    menu.Print
                        (
                            "Type the number to choose a type of saving:\n" +
                            "1 - complete (saving of all of the element)\n" +
                            "2 - diffential (saving of changes only if they exists)"
                        );                    
                    int choixType = Convert.ToInt32(menu.Ask());

                    if (choixType == 1)
                    {
                        int b = 2; //aremplacer par le change du SaveWorkModel
                    }
                    if (choixType == 2)
                    {
                        int b = 2; //aremplacer par le change du SaveWorkModel
                    }
                    ChangeViewMenuInput(0);
                    Affichage();
                    break;

                case 2: // display of mode 2, executing a save
 
                    menu.Print("Display of available saves");
                    menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                    menu.Print("\n\n\n wich save dou you want to execute?");
                    int ExecSaveChoice = Convert.ToInt32(menu.Ask());

                    //aremplacer ce vide par l'ajout de l'appel de la méthode du controlleur pour executer la sauvegarde

                    ChangeViewMenuInput(0);
                    Affichage();

                    break;
                case 3:
                    menu.Print("Display of available saves");
                    menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                    menu.Print("\n from wich save would you want informations?");
                    
                    int InfoSaveChoice = Convert.ToInt32(menu.Ask());

                    menu.Print("informatiooooooooooooooooooons");//aremplacer par l'ajout de l'appel de la méthode du controlleur pour afficher les infos de la sauvegarde

                    menu.Ask();
                    ChangeViewMenuInput(0);
                    Affichage();

                    break;

                case 4:
                    menu.Print("Display of available saves");
                    menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                    menu.Print("\n wich save would you want to delete?");
                    
                    int DeleteSaveChoice = Convert.ToInt32(menu.Ask());

                    //aremplacer par l'ajout de l'appel de la méthode du controlleur pour afficher les infos de la sauvegarde

                    ChangeViewMenuInput(0);
                    Affichage();

                    break;

                case 5:
                    menu.Print("Display of available saves");
                    menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                    menu.Print("\n wich save would you want to delete?");
                    int modifySaveChoice = Convert.ToInt32(menu.Ask());

                    menu.Print("What would you want to modify in this save?");
                    menu.Print("1 - Name of the save\n2 - path of the element to save\n3 - path to save in\n4 - Type of the save\n");
                    int whatToModifyChoice = Convert.ToInt32(menu.Ask());
                    //aremplacer par l'ajout de l'appel de la méthode du controlleur pour modifier la sauvegarde selon ce qu'on va choisir de modifier

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


        public void ChangeViewMenuInput(int a)
        {
            menu.Clear();
            model.SetMenuView(a);
            Affichage();
        }
    }
}
