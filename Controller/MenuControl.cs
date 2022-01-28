using System;
using System.Collections.Generic;
using System.Text;
using EasySave.Model;
using EasySave.View;

namespace EasySave.Controller
{
    class MenuControl
    {
        private MenuModel model;
        private ViewMenu menu;
        public MenuControl(MenuModel mdl, ViewMenu view)
        {
            menu = view;
            model = mdl;
        }

        public void Start()
        {
            Affichage();
        }


        //Function that deside wich language to display
        public void Affichage()
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
                              //menu.Clear();
                        menu.Print
                        (
                         templatetop+
                        " [1] - créer une sauvegarde\n" +
                        " [2] - éxecuter une sauvegarde\n" +
                        " [3] - Montrer les détails d'une sauvegarde\n" +
                        " [4] - Supprimer une sauvegarde\n" +
                        " [5] - Modifier une sauvegarde\n" +
                        " [6] - Changer la langue\n" +
                        " [7] - Fermer l'application\n"+
                        templatebot
                        );


                        //int choice = Convert.ToInt32(menu.Ask());
                        string choice = menu.Ask("Veuillez entrer un chiffre correspondant aux propositions:");
                        if (choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5" || choice == "6" || choice == "7")
                        {
                            ChangeViewMenuInput(choice);
                        }
                        else
                        {
                            //DisplayErrorFr();
                            ChangeViewMenuInput("0",PopUpMessage.InputError);
                        }

                        break;


                    case "1": //View of mode 1, creating of a new save

                        menu.Print("Entrez un nom pour la sauvegarde"); //stringmenu 1.1
                        String nomSave = menu.Ask("Entrez un nom pour la sauvegarde");

                        menu.Print("Entrez le chemin de la ressource a sauvegarder"); //stringmenu 1.2                 
                        String sourcePath = menu.Ask("Entrez le chemin de la ressource a sauvegarder");

                        menu.Print("Entrez le chemin de l'emplacement de la sauvegarde"); //stringmenu 1.3
                        String savePath = menu.Ask("Entrez le chemin de l'emplacement de la sauvegarde");

                        menu.Print
                            (
                                "Entrez un chiffre pour choisir type d'enregistrement:\n" +
                                "1 - complet (on resauvegarde tout l'élément)\n" +
                                "2 - différentiel (sauvegarde seulement les changements lorsqu'il y en a)"
                            ); //stringmenu 1.4
                        string choixType = menu.Ask("Entrez un chiffre pour choisir type d'enregistrement:");


                        if (choixType == "1")
                        {
                            int b = 2; //aremplacer par le change du SaveWorkModel
                        }
                        if (choixType == "2")
                        {
                            int b = 2; //aremplacer par le change du SaveWorkModel
                        }

                        ChangeViewMenuInput("0");
                        Affichage();
                        break;
                        

                    case "2": // display of mode 2, executing a save

                        menu.Print("Affichage des sauvegardes disponibles"); //stringmenu 2.1
                        menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        menu.Print("\n_n_n quelle sauvegarde voulez vous executer?"); //Stringmenu 2.2


                        int ExecSaveChoice = Convert.ToInt32(menu.Ask("quelle sauvegarde voulez vous executer ? "));

                        //aremplacer ce vide par l'ajout de l'appel de la méthode du controlleur pour executer la sauvegarde

                        ChangeViewMenuInput("0");
                        Affichage();

                        break;
                    case "3":
                        menu.Print("Affichage des sauvegardes disponibles"); //stringmenu Displaysave
                        menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        menu.Print("\n de quelle sauvegarde voulez vous les informations?"); //stringmenu 3.1

                        int InfoSaveChoice = Convert.ToInt32(menu.Ask("quelle sauvegarde voulez vous les informations?"));

                        menu.Print("informatiooooooooooooooooooons");//aremplacer par l'ajout de l'appel de la méthode du controlleur pour afficher les infos de la sauvegarde

                        menu.Ask("test");
                        ChangeViewMenuInput("0");
                        Affichage();

                        break;

                    case "4":
                        menu.Print("Affichage des sauvegardes disponibles"); //stringmenu Displaysave
                        menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        menu.Print("\n quelle sauvegarde voulez vous supprimer?"); //stringmenu 4.1

                        int DeleteSaveChoice = Convert.ToInt32(menu.Ask("quelle sauvegarde voulez vous supprimer?"));

                        //aremplacer par l'ajout de l'appel de la méthode du controlleur pour afficher les infos de la sauvegarde

                        ChangeViewMenuInput("0");
                        Affichage();

                        break;

                    case "5":
                        menu.Print("Affichage des sauvegardes disponibles"); //stringmenu DisplaySave
                        menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        menu.Print("\n quelle sauvegarde voulez vous modifier?"); //stringmenu 5.1

                        int modifySaveChoice = Convert.ToInt32(menu.Ask("quelle sauvegarde voulez vous modifier"));

                        menu.Print("Que voulez vous modifier dans la sauvegarde ?"); //stringmenu 5.2
                        menu.Print("1 - Nom de la sauvegarde \n2 - Chemin de la ressource a sauvegarder\n3 - chemin de l'emplacement de la sauvegarde\n4 - Type de sauvegarde\n"); //stringmenu 5.3


                        int whatToModifyChoice = Convert.ToInt32(menu.Ask("Que voulez vous modifier dans la sauvegarde ?"));
                        //aremplacer par l'ajout de l'appel de la méthode du controlleur pour modifier la sauvegarde selon ce qu'on va choisir de modifier

                        ChangeViewMenuInput("0");
                        Affichage();

                        break;

                    case "6":
                        menu.Print("Quel langage voulez vous afficher?");//stringmenu 6.1
                        int languageCounter = 1;
                        foreach (String language in model.GetLanguageList())
                        {
                            menu.Print("["+Convert.ToString(languageCounter) + "] - " + language);
                            languageCounter++;
                        }
                        int languageChoice = Convert.ToInt32(menu.Ask(""));
                        model.SetLanguage(model.GetLanguageList()[languageChoice - 1]);//select of the right language into the list in the model class
                        ChangeViewMenuInput("0");
                        Affichage();
                        break;

                    case "7":
                        menu.Print(templatetop+"Etes vous sur de vouloir quitter l'application?\n\n [1] - oui\n [2] - non\n"+templatebot);
                        SelectLangue("Quel est votre choix");
                        break;
                    default:
                        throw new Exception();

                }
                
            }
            catch (Exception)
            {
                InputException("0");
            }
            }
        
        public void AffichageEn()
        {
            try
            {
                switch (model.GetMenuView())
                {
                    case "0": // Affichage accueil
                        menu.Print
                        (
                        templatetop +
                        " [1] - Create a save\n" +
                        " [2] - Execute a save\n" +
                        " [3] - Show a save detail\n" +
                        " [4] - Delete a save\n" +
                        " [5] - Modify a save\n" +
                        " [6] - Change language settings\n" +
                        " [7] - Close the application\n"+
                        templatebot
                        );

                        //int choice = Convert.ToInt32(menu.Ask());
                        string choice = menu.Ask("Please, Enter a number corresponding to the menu: ");
                        if (choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5" || choice == "6" || choice == "7")
                        {
                            ChangeViewMenuInput(choice);
                        }
                        else
                        {
                            //DisplayErrorEN();
                            ChangeViewMenuInput("0", PopUpMessage.InputError);

                        }

                        break;

                    case "1": //View of mode 1, creating of a new save

                        menu.Print("Type a name for your save");
                        String nomSave = menu.Ask("");

                        menu.Print("Type the path of the element you want to save");
                        String sourcePath = menu.Ask("");

                        menu.Print("Type the path to save in");
                        String savePath = menu.Ask("");

                        menu.Print
                            (
                                "Type the number to choose a type of saving:\n" +
                                "[1] - complete (saving of all of the element)\n" +
                                "[2] - diffential (saving of changes only if they exists)"
                            );
                        int choixType = Convert.ToInt32(menu.Ask(""));

                        if (choixType == 1)
                        {
                            int b = 2; //aremplacer par le change du SaveWorkModel
                        }
                        if (choixType == 2)
                        {
                            int b = 2; //aremplacer par le change du SaveWorkModel
                        }
                        ChangeViewMenuInput("0");
                        Affichage();
                        break;

                    case "2": // display of mode 2, executing a save

                        menu.Print("Display of available saves");
                        menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        menu.Print("\n\n\n wich save dou you want to execute?");
                        int ExecSaveChoice = Convert.ToInt32(menu.Ask(""));

                        //aremplacer ce vide par l'ajout de l'appel de la méthode du controlleur pour executer la sauvegarde

                        ChangeViewMenuInput("0");
                        Affichage();

                        break;
                    case "3":
                        menu.Print("Display of available saves");
                        menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        menu.Print("\n from wich save would you want informations?");

                        int InfoSaveChoice = Convert.ToInt32(menu.Ask(""));

                        menu.Print("informatiooooooooooooooooooons");//aremplacer par l'ajout de l'appel de la méthode du controlleur pour afficher les infos de la sauvegarde

                        menu.Ask("");
                        ChangeViewMenuInput("0");
                        Affichage();

                        break;

                    case "4":
                        menu.Print("Display of available saves");
                        menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        menu.Print("\n wich save would you want to delete?");

                        int DeleteSaveChoice = Convert.ToInt32(menu.Ask(""));

                        //aremplacer par l'ajout de l'appel de la méthode du controlleur pour afficher les infos de la sauvegarde

                        ChangeViewMenuInput("0");
                        Affichage();

                        break;

                    case "5":
                        menu.Print("Display of available saves");
                        menu.Print("1 - test1 \n2 - test2\n");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        
                        int modifySaveChoice = Convert.ToInt32(menu.Ask("Wich save would you want to delete"));

                        menu.Print("What would you want to modify in this save?");
                        menu.Print("1 - Name of the save\n2 - path of the element to save\n3 - path to save in\n4 - Type of the save\n");
                        int whatToModifyChoice = Convert.ToInt32(menu.Ask(""));
                        //aremplacer par l'ajout de l'appel de la méthode du controlleur pour modifier la sauvegarde selon ce qu'on va choisir de modifier

                        ChangeViewMenuInput("0");
                        Affichage();

                        break;

                    case "6":
                        menu.Print("wich language would you want to display?");
                        int languageCounter = 1;
                        foreach (string language in model.GetLanguageList()) //foreach languages available, we will display.
                        {
                            menu.Print("["+Convert.ToString(languageCounter) + "] - " + language);
                            languageCounter++;
                        }
                        int languageChoice = Convert.ToInt32(menu.Ask(""));
                        model.SetLanguage(model.GetLanguageList()[languageChoice - 1]);//select of the right language into the list in the model class
                        ChangeViewMenuInput("0");
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
            }
        }

        //Display function for an imput error in french
        public void DisplayErrorFr()
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
        public void DisplaySuccessFr()
        {
            Console.ForegroundColor = ConsoleColor.Green;//Change foreground color
            menu.Print("Succès de l'operation");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        //Display function for a success message in english
        public void DisplaySuccessEN()
        {
            Console.ForegroundColor = ConsoleColor.Green; //Change foreground color
            menu.Print("Operation is a Succes");
            Console.ForegroundColor = ConsoleColor.Gray;
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

            if (inferreur == PopUpMessage.InputError && model.language == "fr" )
            {
               DisplayErrorFr();
            }

            if (inferreur == PopUpMessage.InputError && model.language == "en")
            {
                DisplayErrorEN();   
            }
            if (inferreur == PopUpMessage.Success && model.language == "fr")
            {
                DisplaySuccessFr();
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
                InputException("7");
            }
        }


        private void InputException(string choiseview)
        {
                if (model.language == "fr")
                { 
                    ChangeViewMenuInput(choiseview, PopUpMessage.InputError); 
                }
                if (model.language == "en")
                {
                    ChangeViewMenuInput(choiseview, PopUpMessage.InputError);
                }
                else
                {
                DisplayAppError();
                }
            }
        
        }
}
