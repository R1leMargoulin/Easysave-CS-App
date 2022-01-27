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
        private Menu menu;
        public MenuControl(MenuModel mdl, Menu view)
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
            if(model.GetLanguage() == "fr")
            {
                AffichageFr();
            }
            else if (model.GetLanguage() == "en")
            {
                AffichageEn();
            }
        }

        public void AffichageFr()
        {
            switch (model.GetMenuView())
            {
                case 0: // Affichage accueil
                    
                        menu.Print
                        (
                        "Veuillez entrer un chiffre correspondant aux propositions: \n \n \n" +
                        "1 - créer une sauvegarde\n" +
                        "2 - éxecuter une sauvegarde\n" +
                        "3 - Montrer les détails d'une sauvegarde\n" +
                        "4 - Supprimer une sauvegarde\n" +
                        "5 - Modifier une sauvegarde\n" +
                        "6 - Changer la langue\n" +
                        "7 - Fermer l'application\n"
                        );


                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == 1 || choice == 2 || choice == 3 || choice == 4 || choice == 5 || choice == 6 || choice == 7)
                    {
                        ChangeViewMenuInput(choice);
                    }

                    break;

                case 1: //View of mode 1, creating of a new save

                    menu.Print("Entrez un nom pour la sauvegarde"); //stringmenu 1.1
                    String nomSave = Console.ReadLine();

                    menu.Print("Entrez le chemin de la ressource a sauvegarder"); //stringmenu 1.2                 
                    String sourcePath = Console.ReadLine();

                    menu.Print("Entrez le chemin de l'emplacement de la sauvegarde"); //stringmenu 1.3
                    String savePath = Console.ReadLine();

                    menu.Print
                        (
                            "Entrez un chiffre pour choisir type d'enregistrement:\n" +
                            "1 - complet (on resauvegarde tout l'élément)\n" +
                            "2 - différentiel (sauvegarde seulement les changements lorsqu'il y en a)"
                        ); //stringmenu 1.4
                    int choixType = Convert.ToInt32(Console.ReadLine());


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

                        menu.Print("Affichage des sauvegardes disponibles"); //stringmenu 2.1
                        menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        menu.Print("\n_n_n quelle sauvegarde voulez vous executer?"); //Stringmenu 2.2
                    

                    int ExecSaveChoice = Convert.ToInt32(Console.ReadLine());

                    //aremplacer ce vide par l'ajout de l'appel de la méthode du controlleur pour executer la sauvegarde

                    ChangeViewMenuInput(0);
                    Affichage();

                    break;
                case 3:
                        menu.Print("Affichage des sauvegardes disponibles"); //stringmenu Displaysave
                        menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        menu.Print("\n de quelle sauvegarde voulez vous les informations?"); //stringmenu 3.1

                    int InfoSaveChoice = Convert.ToInt32(Console.ReadLine());

                    menu.Print("informatiooooooooooooooooooons");//aremplacer par l'ajout de l'appel de la méthode du controlleur pour afficher les infos de la sauvegarde

                    Console.ReadLine();
                    ChangeViewMenuInput(0);
                    Affichage();

                    break;

                case 4:
                        menu.Print("Affichage des sauvegardes disponibles"); //stringmenu Displaysave
                        menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        menu.Print("\n quelle sauvegarde voulez vous supprimer?"); //stringmenu 4.1

                    int DeleteSaveChoice = Convert.ToInt32(Console.ReadLine());

                    //aremplacer par l'ajout de l'appel de la méthode du controlleur pour afficher les infos de la sauvegarde

                    ChangeViewMenuInput(0);
                    Affichage();

                    break;

                case 5:
                        menu.Print("Affichage des sauvegardes disponibles"); //stringmenu DisplaySave
                        menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        menu.Print("\n quelle sauvegarde voulez vous modifier?"); //stringmenu 5.1
                    
                    int modifySaveChoice = Convert.ToInt32(Console.ReadLine());

                        menu.Print("Que voulez vous modifier dans la sauvegarde ?"); //stringmenu 5.2
                        menu.Print("1 - Nom de la sauvegarde \n2 - Chemin de la ressource a sauvegarder\n3 - chemin de l'emplacement de la sauvegarde\n4 - Type de sauvegarde\n"); //stringmenu 5.3


                    int whatToModifyChoice = Convert.ToInt32(Console.ReadLine());
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
                    int languageChoice = Convert.ToInt32(Console.ReadLine());
                    model.SetLanguage(model.GetLanguageList()[languageChoice - 1]);//select of the right language into the list in the model class
                    ChangeViewMenuInput(0);
                    Affichage();
                    break;
                case 7:
                    menu.Print("Etes vous sur de vouloir quitter l'application?\n\n1 - oui\n2 - non\n"); //stringmenu 7.1

                    int ExitChoice = Convert.ToInt32(Console.ReadLine());

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
                        "1 - create a save\n" +
                        "2 - execute a save\n" +
                        "3 - show a save details\n" +
                        "4 - delete a save\n" +
                        "5 - modify a save\n" +
                        "6 - change language settings\n" +
                        "7 - close the app\n"
                        );
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == 1 || choice == 2 || choice == 3 || choice == 4 || choice == 5 || choice == 6 || choice == 7)
                    {
                        ChangeViewMenuInput(choice);
                    }

                    break;

                case 1: //View of mode 1, creating of a new save
                    
                    menu.Print("Type a name for your save");
                    String nomSave = Console.ReadLine();

                    menu.Print("Type the path of the element you want to save");
                    String sourcePath = Console.ReadLine();

                    menu.Print("Type the path to save in");
                    String savePath = Console.ReadLine();

                    menu.Print
                        (
                            "Type the number to choose a type of saving:\n" +
                            "1 - complete (saving of all of the element)\n" +
                            "2 - diffential (saving of changes only if they exists)"
                        );                    
                    int choixType = Convert.ToInt32(Console.ReadLine());

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
                    int ExecSaveChoice = Convert.ToInt32(Console.ReadLine());

                    //aremplacer ce vide par l'ajout de l'appel de la méthode du controlleur pour executer la sauvegarde

                    ChangeViewMenuInput(0);
                    Affichage();

                    break;
                case 3:
                    menu.Print("Display of available saves");
                    menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                    menu.Print("\n from wich save would you want informations?");
                    
                    int InfoSaveChoice = Convert.ToInt32(Console.ReadLine());

                    menu.Print("informatiooooooooooooooooooons");//aremplacer par l'ajout de l'appel de la méthode du controlleur pour afficher les infos de la sauvegarde

                    Console.ReadLine();
                    ChangeViewMenuInput(0);
                    Affichage();

                    break;

                case 4:
                    menu.Print("Display of available saves");
                    menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                    menu.Print("\n wich save would you want to delete?");
                    
                    int DeleteSaveChoice = Convert.ToInt32(Console.ReadLine());

                    //aremplacer par l'ajout de l'appel de la méthode du controlleur pour afficher les infos de la sauvegarde

                    ChangeViewMenuInput(0);
                    Affichage();

                    break;

                case 5:
                    menu.Print("Display of available saves");
                    menu.Print("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                    menu.Print("\n wich save would you want to delete?");
                    int modifySaveChoice = Convert.ToInt32(Console.ReadLine());

                    menu.Print("What would you want to modify in this save?");
                    menu.Print("1 - Name of the save\n2 - path of the element to save\n3 - path to save in\n4 - Type of the save\n");
                    int whatToModifyChoice = Convert.ToInt32(Console.ReadLine());
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
                    int languageChoice = Convert.ToInt32(Console.ReadLine());
                    model.SetLanguage(model.GetLanguageList()[languageChoice - 1]);//select of the right language into the list in the model class
                    ChangeViewMenuInput(0);
                    Affichage();
                    break;
                case 7:
                    if (model.GetLanguage() == "en")
                    {
                        menu.Print("Are you sure that you want to exit from the app?\n\n1 - yes\n2 - no\n");
                    }
                    int ExitChoice = Convert.ToInt32(Console.ReadLine());

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
            Console.Clear();
            model.SetMenuView(a);
            Affichage();
        }
    }
}
