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
            try
            {
                switch (model.GetMenuView())
                {
                    case "0": // Affichage accueil
                              //menu.Clear();
                        menu.Print
                        (
                        "==========================================================\n" +
                        "\t Welcome in EasySave \n \n" +
                        "Veuillez entrer un chiffre correspondant aux propositions: \n \n" +
                        "[1] - créer une sauvegarde\n" +
                        "[2] - éxecuter une sauvegarde\n" +
                        "[3] - Montrer les détails d'une sauvegarde\n" +
                        "[4] - Supprimer une sauvegarde\n" +
                        "[5] - Modifier une sauvegarde\n" +
                        "[6] - Changer la langue\n" +
                        "[7] - Changer le format des logs\n" +
                        "[8] - Fermer l'application\n"+
                        "\n=========================================================="
                        );


                        //int choice = Convert.ToInt32(menu.Ask());
                        string choice = menu.Ask("Veuillez entrer un chiffre correspondant aux propositions:");
                        if (choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5" || choice == "6" || choice == "7" || choice == "8")
                        {
                            ChangeViewMenuInput(choice);
                        }
                        else
                        {
                            //DisplayErrorFr();
                            ChangeViewMenuInput("0",Erreur.InputErrorFR);
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
                            menu.Print(Convert.ToString(languageCounter) + " " + language);
                            languageCounter++;
                        }
                        int languageChoice = Convert.ToInt32(menu.Ask("Saisie"));
                        model.SetLanguage(model.GetLanguageList()[languageChoice - 1]);//select of the right language into the list in the model class
                        ChangeViewMenuInput("0");
                        Affichage();
                        break;

                    //case "7":
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


                    case "8":
                        menu.Print("Etes vous sur de vouloir quitter l'application?\n\n1 - oui\n2 - non\n"); //stringmenu 8.1

                        string ExitChoice = menu.Ask("Seletion");

                        if (ExitChoice == "1")
                        {
                            Environment.Exit(0);
                        }


                        if (ExitChoice == "2")
                        {
                            ChangeViewMenuInput("0");
                            //Affichage();
                        }
                        else
                        {
                            throw new Exception();
                        }

                        break;
                    default:
                        throw new Exception();

                }
                
            }
            catch (Exception)
            {
                //DisplayErrorFr();
                ChangeViewMenuInput("0",Erreur.InputErrorFR) ;
                //Affichage();

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
                        "=================================================\n" +
                        "\t Welcome in EasySave \n \n"+
                        "Please, Enter a number corresponding to the menu: \n \n" +
                        "[1] - create a save\n" +
                        "[2] - execute a save\n" +
                        "[3] - show a save details\n" +
                        "[4] - delete a save\n" +
                        "[5] - modify a save\n" +
                        "[6] - change language settings\n" +
                        "[7] - change lof format\n" +
                        "[8] - close the app\n"+
                        "\n================================================"

                        );

                        //int choice = Convert.ToInt32(menu.Ask());
                        string choice = menu.Ask("");
                        if (choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5" || choice == "6" || choice == "7" || choice == "8")
                        {
                            ChangeViewMenuInput(choice);
                        }
                        else
                        {
                            //DisplayErrorEN();
                            ChangeViewMenuInput("0", Erreur.InputErrorEN);

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
                        menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        menu.Print("\n wich save would you want to delete?");
                        int modifySaveChoice = Convert.ToInt32(menu.Ask(""));

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
                        foreach (String language in model.GetLanguageList()) //foreach languages available, we will display.
                        {
                            menu.Print(Convert.ToString(languageCounter) + " " + language);
                            languageCounter++;
                        }
                        int languageChoice = Convert.ToInt32(menu.Ask(""));
                        model.SetLanguage(model.GetLanguageList()[languageChoice - 1]);//select of the right language into the list in the model class
                        ChangeViewMenuInput("0");
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

                    case "8":
                        if (model.language == "en")
                        {
                            menu.Print("Are you sure that you want to exit from the app?\n\n[1] - yes\n[2] - no\n");
                        }
                        string ExitChoice = menu.Ask("");

                        if (ExitChoice == "1")
                        {
                            Environment.Exit(0);
                        }

                        if (ExitChoice == "2")
                        {
                            ChangeViewMenuInput("0");
                            //Affichage();
                        }
                        else
                        {
                            //throw new Exception();
                            ChangeViewMenuInput("0", Erreur.InputErrorEN);
                        }

                        break;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception)
            {
               
                ChangeViewMenuInput("0",Erreur.InputErrorEN);
                //Affichage();

            }
        }

        public void DisplayErrorFr()
        {
            //Change foreground color
            Console.ForegroundColor = ConsoleColor.Red;
            menu.Print("Erreur de saisie");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void DisplayErrorEN()
        {
            //Change foreground color
            Console.ForegroundColor = ConsoleColor.Red;
            menu.Print("Imput error");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void DisplaySuccessFr()
        {
            //Change foreground color
            Console.ForegroundColor = ConsoleColor.Green;
            menu.Print("Succès de l'operation");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void DisplaySuccessEN()
        {
            //Change foreground color
            Console.ForegroundColor = ConsoleColor.Green;
            menu.Print("Operation is a Succes");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void ChangeViewMenuInput(string a, Erreur inferreur = Erreur.NoErreur)
        {
            menu.Clear();

            if (inferreur == Erreur.InputErrorFR)
            {
               DisplayErrorFr();   
            }

            if (inferreur == Erreur.InputErrorEN)
            {
                DisplayErrorEN();   
            }
            if (inferreur == Erreur.SuccesFR)
            {
                DisplaySuccessFr();
            }

            if (inferreur == Erreur.SuccesEN)
            {
                DisplaySuccessEN();
            }
            model.SetMenuView(a);
                Affichage();
            
        }
    }
}
