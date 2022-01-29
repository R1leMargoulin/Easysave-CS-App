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

        public void Affichage() //choix de la méthode en fonction de la langue 
        {
            if(model.language == "fr")
            {
                AffichageFr();
            }
            if (model.language == "en")
            {
                AffichageEn();
            }
            else
            {
                DisplayAppError();
        }
        }


        public void AffichageFr()
        {
            try
            {
                switch (model.GetMenuView())
                {
                    case "0": // Affichage accueil
                        controllerbackup.ListBackup();
                        menu.Print
                        (
                         templatetop +
                        " [1] - Créer une sauvegarde\n" +
                        " [2] - Executer une sauvegarde\n" +
                        " [3] - Montrer les détails d'une sauvegarde\n" +
                        " [4] - Supprimer une sauvegarde\n" +
                        " [5] - Modifier une sauvegarde\n" +
                        " [6] - Changer la langue\n" +
                        " [7] - Fermer l'application\n" +
                        templatebot
                        );


                        string choice = menu.Ask("Veuillez entrer un chiffre correspondant aux propositions:");
                        if (choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5" || choice == "6" || choice == "7")
                        {
                            ChangeViewMenuInput(choice);
                        }
                        else
                        {
                            //DisplayErrorFr();
                            ChangeViewMenuInput("0", PopUpMessage.InputError);
                        }

                        break;


                    case "1": //View of mode 1, creating of a new save
                        Backup backup = new Backup();

                        menu.Print(templatetop + "\n [*] - Création de la sauvegarde \n" + templatebot); //stringmenu 1.1
                        var nomSave = menu.Ask("Entrez un nom pour la sauvegarde : ");
                        backup.Name = nomSave;


                        //menu.Print("Entrez le chemin de la ressource a sauvegarder"); //stringmenu 1.2                 
                        var sourcePath = menu.Ask("Entrez le chemin de la ressource a sauvegarder : ");
                        backup.DirectorySource = sourcePath;

                        //menu.Print("Entrez le chemin de l'emplacement de la sauvegarde"); //stringmenu 1.3
                        var savePath = menu.Ask("Entrez le chemin de l'emplacement de la sauvegarde : ");
                        backup.DirectoryTarget = savePath;

                        menu.Print
                            (
                                " [1] - complet (on resauvegarde tout l'élément)\n" +
                                " [2] - différentiel (sauvegarde seulement les changements lorsqu'il y en a)"
                            ); //stringmenu 1.4
                        string choixType = (menu.Ask("Quel est votre choix : "));


                        if (choixType == "1")
                        {
                            backup.BackupType = BackupType.Complet;
                            controllerbackup.AddBackup(backup); //aremplacer par le change du SaveWorkModel
                            ChangeViewMenuInput("0", PopUpMessage.Success);
                            Affichage();
                        }
                        if (choixType == "2")
                        {
                            backup.BackupType = BackupType.Differentielle;
                            controllerbackup.AddBackup(backup); //aremplacer par le change du SaveWorkModel
                            ChangeViewMenuInput("0", PopUpMessage.Success);
                            Affichage();
                        }
                        else
                        {
                            ChangeViewMenuInput("0", PopUpMessage.InputError);
                        }
                        break;

                    case "2": // display of mode 2, executing a save

                        menu.Print(templatetop + "\nAffichage des sauvegardes disponibles : \n\n" + GetAllBackup() + templatebot);

                        menu.Print("Quelle sauvegarde voulez vous executer?"); //Stringmenu 2.2
                        int Executeindex = Convert.ToInt32(menu.Ask("Quel est votre choix : "));
                        ListBackup(Executeindex).BackupExecute(ListBackup(Executeindex).DirectorySource, ListBackup(Executeindex).DirectoryTarget);
                        ChangeViewMenuInput("0");
                        Affichage();

                        break;
                    case "3": //Display Backup Informations
                        menu.Print(templatetop + "\nAffichage des sauvegardes disponibles : \n\n" + GetAllBackup() + templatebot); //stringmenu Displaysave

                        //menu.Print(GetAllBackup());
                        menu.Print("De quelle sauvegarde voulez vous les informations?"); //stringmenu 3.1
                        int Displayindex = Convert.ToInt32(menu.Ask(""));
                        ListBackup(Displayindex);
                        menu.Print("Nom : " + ListBackup(Displayindex).Name + "\n" + "Répertoire Source : " + ListBackup(Displayindex).DirectorySource + "\n" + "Répertoire cible : " + ListBackup(Displayindex).DirectoryTarget + "\n" + "Type : " + ListBackup(Displayindex).BackupType);

                        menu.Ask("");
                        ChangeViewMenuInput("0", PopUpMessage.Success);
                        Affichage();

                        break;

                    case "4":
                        menu.Print(templatetop + "\nAffichage des sauvegardes disponibles : \n\n" + GetAllBackup() + templatebot);
                        //menu.Print(GetAllBackup());          
                        menu.Print("\nQuelle sauvegarde voulez vous supprimer?");
                        var deleteindex = Convert.ToInt32(menu.Ask("Quelle est votre choix : "));
                        controllerbackup.DeleteBackup(deleteindex);
                        ChangeViewMenuInput("0", PopUpMessage.Success);
                        Affichage();

                        break;

                    case "5"://Update
                        menu.Print(templatetop + "\nAffichage des sauvegardes disponibles : \n\n" + GetAllBackup() + templatebot);//stringmenu DisplaySave

                        //  menu.Print(GetAllBackup());
                        menu.Print("Quelle sauvegarde voulez vous modifier?"); //stringmenu 5.1

                        int updateIndex = Convert.ToInt32(menu.Ask("Quel est votre choix : "));

                        menu.Print("Que voulez vous modifier dans la sauvegarde ?"); //stringmenu 5.2
                        menu.Print("\n [1] - Nom de la sauvegarde \n [2] - Chemin de la ressource a sauvegarder\n [3] - chemin de l'emplacement de la sauvegarde\n [4] - Type de sauvegarde\n"); //stringmenu 5.3


                        int whatToModifyChoice = Convert.ToInt32(menu.Ask("Quel est votre choix : "));
                        ListBackup(updateIndex);
                        if (whatToModifyChoice == 1)
                        {
                            menu.Print("Entrez le nouveau nom \n");
                            ListBackup(updateIndex).Name = menu.Ask("");
                            controllerbackup.UpdateBackup(updateIndex, ListBackup(updateIndex));
                        }
                        if (whatToModifyChoice == 2)
                        {
                            menu.Print("Entrez le nouveau répertoire source : \n");
                            ListBackup(updateIndex).DirectorySource = menu.Ask("");
                            controllerbackup.UpdateBackup(updateIndex, ListBackup(updateIndex));
                        }
                        if (whatToModifyChoice == 3)
                        {
                            menu.Print("Entrez le nouveau répertoire cible : \n");
                            ListBackup(updateIndex).DirectoryTarget = menu.Ask("");
                            controllerbackup.UpdateBackup(updateIndex, ListBackup(updateIndex));
                        }
                        if (whatToModifyChoice == 4)
                        {
                            menu.Print("Entrez le nouveau type de sauvegarde : \n");
                            ListBackup(updateIndex).BackupType = (BackupType)Convert.ToInt32(menu.Ask(""));
                            controllerbackup.UpdateBackup(updateIndex, ListBackup(updateIndex));
                        }

                        ChangeViewMenuInput("0", PopUpMessage.Success);
                        Affichage();

                        break;

                        break;

                    case "6":
                        menu.Print(templatetop + "\nQuel langage voulez vous afficher? \n\n" + DisplayLanguageList() + "\n" + templatebot);
                        //menu.Print(templatetop + "\nQuel langage voulez vous afficher?\n");//stringmenu 6.1
                        //int languageCounter = 1;
                        //foreach (String language in model.GetLanguageList())
                        //{
                        //    menu.Print(" [" + Convert.ToString(languageCounter) + "] - " + language);
                        //    languageCounter++;
                        //}
                        //menu.Print("\n" + templatebot);
                        int languageChoice = Convert.ToInt32(menu.Ask("Quel est votre choix : "));
                        model.SetLanguage(model.GetLanguageList()[languageChoice - 1]);//select of the right language into the list in the model class
                        ChangeViewMenuInput("0");
                        Affichage();
                        break;

                    case "7":
                        menu.Print(templatetop + "Etes vous sur de vouloir quitter l'application?\n\n [1] - oui\n [2] - non\n" + templatebot);
                        SelectLangue("Quel est votre choix : ");
                        break;
                    default:
                        throw new Exception();

                }
                
            }
            catch (Exception)
            {
                ChangeViewMenuInput("0", PopUpMessage.InputError);
            }
            }
        
        public void AffichageEn()
        {
            try
            {
                Backup backup = new Backup();
                switch (model.GetMenuView())
                {
                    case "0": // Affichage accueil
                        controllerbackup.ListBackup();
                        menu.Print
                        (
                        templatetop +
                        " [1] - Create a save\n" +
                        " [2] - Execute a save\n" +
                        " [3] - Show a save detail\n" +
                        " [4] - Delete a save\n" +
                        " [5] - Modify a save\n" +
                        " [6] - Change language settings\n" +
                        " [7] - Close the application\n" +
                        templatebot
                        );

                        //int choice = Convert.ToInt32(menu.Ask());
                        string choice = menu.Ask("Please, Enter a number corresponding to the menu : ");
                        if (choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5" || choice == "6" || choice == "7")
                        {
                            ChangeViewMenuInput(choice);
                        }
                        else
                        {
                            ChangeViewMenuInput("0", PopUpMessage.InputError);

                        }

                        break;

                    case "1": //View of mode 1, creating of a new save

                        var nomSave = menu.Ask("Type a name for your save : ");
                        backup.Name = nomSave;

                        var sourcePath = menu.Ask("Type the path of the element you want to save : ");
                        backup.DirectorySource = sourcePath;

                        var savePath = menu.Ask("Type the path to save in : ");
                        backup.DirectoryTarget = savePath;

                        menu.Print
                            (
                                "Type the number to choose a type of saving:\n" +
                                " [1] - complete (saving of all of the element)\n" +
                                " [2] - diffential (saving of changes only if they exists)"
                            );
                        int choixType = Convert.ToInt32(menu.Ask(""));

                        if (choixType == 1)
                        {
                            backup.BackupType = BackupType.Complet;
                            controllerbackup.AddBackup(backup);
                            ChangeViewMenuInput("0");
                            Affichage();
                        }
                        if (choixType == 2)
                        {
                            backup.BackupType = BackupType.Differentielle;
                            controllerbackup.AddBackup(backup);
                            ChangeViewMenuInput("0");
                            Affichage();
                        }
                        else
                        {
                            ChangeViewMenuInput("0", PopUpMessage.InputError);
                        }
;
                        break;

                   
                    case "2": // display of mode 2, executing a save

                        menu.Print(templatetop + "\nDisplay of available saves : \n\n" + GetAllBackup() + "\n" + templatebot);

                        menu.Print("Wich save dou you want to execute?");
                        int Exeindex = Convert.ToInt32(menu.Ask("What is your choise : "));
                        ListBackup(Exeindex).BackupExecute(ListBackup(Exeindex).DirectorySource, ListBackup(Exeindex).DirectoryTarget);
                        ChangeViewMenuInput("0");
                        Affichage();

                        break;
                    case "3":
                        menu.Print(templatetop + "\nDisplay of available saves : \n\n" + GetAllBackup() + "\n" + templatebot);

                        menu.Print("From wich save would you want informations?");
                        int Showindex = Convert.ToInt32(menu.Ask("What is your choise : "));
                        ListBackup(Showindex);
                        menu.Print("Name : " + ListBackup(Showindex).Name + "\n" + "Source Directory : " + ListBackup(Showindex).DirectorySource + "\n" + "Target Directory : " + ListBackup(Showindex).DirectoryTarget + "\n" + "Save type : " + ListBackup(Showindex).BackupType);
                        menu.Ask("");
                        ChangeViewMenuInput("0", PopUpMessage.Success);
                        Affichage();

                        break;

                    case "4":
                        menu.Print(templatetop + "\nDisplay of available saves : \n\n" + GetAllBackup() + "\n" + templatebot);

                        menu.Print("\nWich save would you want to delete?");
                        var deleteindex = Convert.ToInt32(menu.Ask("What is your choise : "));
                        controllerbackup.DeleteBackup(deleteindex);
                        ChangeViewMenuInput("0", PopUpMessage.Success);
                        Affichage();

                        break;

                    case "5":
                        menu.Print(templatetop + "\nDisplay of available saves : \n\n" + GetAllBackup() + "\n" + templatebot);

                        menu.Print("Wich save would you want to modify?");
                        int updateIndex = Convert.ToInt32(menu.Ask("What is your choise : "));

                        menu.Print("What would you want to modify in this save?");
                        menu.Print("\n [1] - Name of the save\n [2] - Source path of the element to save\n [3] - Destination path to save in\n [4] - Type of the save\n");
                        int whatToModifyChoice = Convert.ToInt32(menu.Ask(""));
                        ListBackup(updateIndex);
                        if (whatToModifyChoice == 1)
                        {
                            //menu.Print("Enter the new name \n");
                            ListBackup(updateIndex).Name = menu.Ask("Enter the new name : ");
                            controllerbackup.UpdateBackup(updateIndex, ListBackup(updateIndex));
                        }
                        if (whatToModifyChoice == 2)
                        {
                            //menu.Print("Enter the new source directory \n");
                            ListBackup(updateIndex).DirectorySource = menu.Ask("Enter the new source directory : ");
                            controllerbackup.UpdateBackup(updateIndex, ListBackup(updateIndex));
                        }
                        if (whatToModifyChoice == 3)
                        {
                            //menu.Print("Enter the new target directory \n");
                            ListBackup(updateIndex).DirectoryTarget = menu.Ask("Enter the new target directory : ");
                            controllerbackup.UpdateBackup(updateIndex, ListBackup(updateIndex));
                        }
                        if (whatToModifyChoice == 4)
                        {
                            //menu.Print("Enter the new Backup type \n");
                            ListBackup(updateIndex).BackupType = (BackupType)Convert.ToInt32(menu.Ask("Enter the new Backup type : "));
                            controllerbackup.UpdateBackup(updateIndex, ListBackup(updateIndex));
                        }

                        ChangeViewMenuInput("0", PopUpMessage.Success);
                        Affichage();


                        break;

                    case "6":
                        menu.Print(templatetop + "\nWich language would you want to display? \n\n" + DisplayLanguageList() + "\n" + templatebot);
                        //int languageCounter = 1;
                        //foreach (String language in model.GetLanguageList()) //foreach languages available, we will display.
                        //{
                        //    menu.Print(Convert.ToString(languageCounter) + " " + language);
                        //    languageCounter++;
                        //}
                        int languageChoice = Convert.ToInt32(menu.Ask("What is your choise : "));
                        model.SetLanguage(model.GetLanguageList()[languageChoice - 1]);//select of the right language into the list in the model class
                        ChangeViewMenuInput("0");
                        Affichage();
                        break;


                    case "7":
                        menu.Print(templatetop + "Are you sure that you want to exit from the app?\n\n [1] - yes\n [2] - no\n" + templatebot);
                        SelectLangue("What is your choise");

                        break;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception)
                        {
                ChangeViewMenuInput("0", PopUpMessage.InputError);
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
        //Display function for an imput error in french
        public void DisplayErrorFR()
                        {
            Console.ForegroundColor = ConsoleColor.Red;//Change foreground color
            menu.Print("Erreur de saisie");
            Console.ForegroundColor = ConsoleColor.Gray;
                        }

        //Display function for an imput error in english
        public void DisplayErrorEN()
        {
            Console.ForegroundColor = ConsoleColor.Red;//Change foreground color
            menu.Print("Imput error");
            Console.ForegroundColor = ConsoleColor.Gray;
            }

        //Display function for a success message in french
        public void DisplaySuccessFR()
            {
            Console.ForegroundColor = ConsoleColor.Green;//Change foreground color
            menu.Print("Succès de l'operation");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        //Display function for a success message in english
        public void DisplaySuccessEN()
        {
            Console.ForegroundColor = ConsoleColor.Green; //Change foreground color
            menu.Print("Operation is a Success");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public string DisplayLanguageList()
        {
            int languageCounter = 1;
            string affiche = "";
            foreach (String language in model.GetLanguageList())
            {
                affiche += " [" + Convert.ToString(languageCounter) + "] - " + language + "\n";
                languageCounter++;


            }
            return affiche;

        }
        //Display function for a success message in english
        public void DisplayAppError()
        {
            Console.ForegroundColor = ConsoleColor.Red; //Change foreground color
            menu.Print("FATAL ERROR");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        //Function that change the menu and display some error if they occur
        public void ChangeViewMenuInput(string a, PopUpMessage inferreur = PopUpMessage.NoErreur)
        {
            menu.Clear();

            if (inferreur == PopUpMessage.InputError && model.language == "fr")
            {
                DisplayErrorFR();
            }

            if (inferreur == PopUpMessage.InputError && model.language == "en")
            {
                DisplayErrorEN();   
            }
            if (inferreur == PopUpMessage.Success && model.language == "fr")
            {
                DisplaySuccessFR();
            }

            if (inferreur == PopUpMessage.Success && model.language == "en")
            {
                DisplaySuccessEN();
            }

                model.SetMenuView(a);
                Affichage();
        }

        public string templatetop = "===========================================================\n\n\t Welcome in EasySave\n\n";
        public string templatebot = "\n===========================================================";

        private void SelectLangue(string whattodisplay)
        {
            string ExitChoice = menu.Ask(whattodisplay);
            
            try
            {
                if (ExitChoice == "1")
                {
                    menu.Clear();
                    Environment.Exit(0);
                }
                if (ExitChoice == "2")
                {
                    ChangeViewMenuInput("0");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                ChangeViewMenuInput("7", PopUpMessage.InputError);
            }
        }
    }
}
