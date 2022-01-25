using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class View
    {
        Model model;
        Controller control;
        public View(Model mdl, Controller ctrl)
        {
            model = mdl;
            control = ctrl;
        }
        public void Start()
        {
            affichage();
        }
        public void affichage()
        {
            switch (model.GetMenuView())
            {
                case 0: // Affichage accueil
                    if(model.GetLanguage() == "fr")
                    {
                        Console.WriteLine
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
                    }
                    if (model.GetLanguage() == "en")
                    {
                        Console.WriteLine
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
                    }
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == 1 || choice == 2 || choice == 3 || choice == 4 || choice == 5 || choice == 6 || choice == 7)
                    {
                        ChangeViewMenuInput(choice);
                    }

                    break;

                case 1: //View of mode 1, creating of a new save
                        if (model.GetLanguage() == "fr")
                        {
                            Console.WriteLine("Entrez un nom pour la sauvegarde");
                        }
                        if (model.GetLanguage() == "en")
                        {
                            Console.WriteLine("Type a name for your save");
                        }
                        String nomSave = Console.ReadLine();
                        if (model.GetLanguage() == "fr")
                        {
                            Console.WriteLine("Entrez le chemin de la ressource a sauvegarder");
                        }
                        if (model.GetLanguage() == "en")
                        {
                            Console.WriteLine("Type the path of the element you want to save");
                        }
                        String sourcePath = Console.ReadLine();

                        if (model.GetLanguage() == "fr")
                        {
                            Console.WriteLine("Entrez le chemin de l'emplacement de la sauvegarde");
                        }
                        if (model.GetLanguage() == "en")
                        {
                            Console.WriteLine("Type the path to save in");
                        }
                        String savePath = Console.ReadLine();
                        if (model.GetLanguage() == "fr")
                        {
                            Console.WriteLine
                                (
                                    "Entrez un chiffre pour choisir type d'enregistrement:\n" +
                                    "1 - complet (on resauvegarde tout l'élément)\n" +
                                    "2 - différentiel (sauvegarde seulement les changements lorsqu'il y en a)"
                                );
                        }
                        if (model.GetLanguage() == "en")
                        {
                            Console.WriteLine
                                (
                                    "Type the number to choose a type of saving:\n" +
                                    "1 - complete (saving of all of the element)\n" +
                                    "2 - diffential (saving of changes only if they exists)"
                                );
                        }
                        int choixType = Convert.ToInt32(Console.ReadLine());
                        if(choixType == 1)
                        {
                            int b = 2; //aremplacer par le change du SaveWorkModel
                        }
                        if (choixType == 2)
                        {
                            int b = 2; //aremplacer par le change du SaveWorkModel
                        }
                        ChangeViewMenuInput(0);
                        affichage();
                        break;

                case 2: // display of mode 2, executing a save
                    if (model.GetLanguage() == "fr")
                    {
                        Console.WriteLine("Affichage des sauvegardes disponibles");
                        Console.WriteLine("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        Console.WriteLine("\n_n_n quelle sauvegarde voulez vous executer?");
                    }
                    if (model.GetLanguage() == "en")
                    {
                        Console.WriteLine("Display of available saves");
                        Console.WriteLine("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        Console.WriteLine("\n\n\n wich save dou you want to execute?");
                    }

                    int ExecSaveChoice = Convert.ToInt32(Console.ReadLine());

                    //aremplacer ce vide par l'ajout de l'appel de la méthode du controlleur pour executer la sauvegarde

                    ChangeViewMenuInput(0);
                    affichage();

                    break;
                case 3:
                    if (model.GetLanguage() == "fr")
                    {
                        Console.WriteLine("Affichage des sauvegardes disponibles");
                        Console.WriteLine("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        Console.WriteLine("\n de quelle sauvegarde voulez vous les informations?");
                    }
                    if (model.GetLanguage() == "en")
                    {
                        Console.WriteLine("Display of available saves");
                        Console.WriteLine("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        Console.WriteLine("\n from wich save would you want informations?");
                    }
                    int InfoSaveChoice = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("informatiooooooooooooooooooons");//aremplacer par l'ajout de l'appel de la méthode du controlleur pour afficher les infos de la sauvegarde

                    Console.ReadLine();                    
                    ChangeViewMenuInput(0);
                    affichage();

                    break;

                case 4:
                    if (model.GetLanguage() == "fr")
                    {
                        Console.WriteLine("Affichage des sauvegardes disponibles");
                        Console.WriteLine("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        Console.WriteLine("\n quelle sauvegarde voulez vous supprimer?");
                    }
                    if (model.GetLanguage() == "en")
                    {
                        Console.WriteLine("Display of available saves");
                        Console.WriteLine("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        Console.WriteLine("\n wich save would you want to delete?");
                    }
                    int DeleteSaveChoice = Convert.ToInt32(Console.ReadLine());

                    //aremplacer par l'ajout de l'appel de la méthode du controlleur pour afficher les infos de la sauvegarde

                    ChangeViewMenuInput(0);
                    affichage();

                    break;

                case 5:
                    if (model.GetLanguage() == "fr")
                    {
                        Console.WriteLine("Affichage des sauvegardes disponibles");
                        Console.WriteLine("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        Console.WriteLine("\n quelle sauvegarde voulez vous modifier?");
                    }
                    if (model.GetLanguage() == "en")
                    {
                        Console.WriteLine("Display of available saves");
                        Console.WriteLine("1 - test1 \n2 - test2");//aremplacer par la lecture des saves dans un fichier ou je ne sais quoi
                        Console.WriteLine("\n wich save would you want to delete?");
                    }


                    int modifySaveChoice = Convert.ToInt32(Console.ReadLine());


                    if (model.GetLanguage() == "fr")
                    {
                        Console.WriteLine("Que voulez vous modifier dans la sauvegarde ?");
                        Console.WriteLine("1 - Nom de la sauvegarde \n2 - Chemin de la ressource a sauvegarder\n3 - chemin de l'emplacement de la sauvegarde\n4 - Type de sauvegarde\n");
                    }
                    if (model.GetLanguage() == "en")
                    {
                        Console.WriteLine("What would you want to modify in this save?");
                        Console.WriteLine("1 - Nom de la sauvegarde \n2 - Chemin de la ressource a sauvegarder\n3 - chemin de l'emplacement de la sauvegarde\n4 - Type de sauvegarde\n");
                    }

                    int whatToModifyChoice = Convert.ToInt32(Console.ReadLine());
                    //aremplacer par l'ajout de l'appel de la méthode du controlleur pour modifier la sauvegarde selon ce qu'on va choisir de modifier

                    ChangeViewMenuInput(0);
                    affichage();

                    break;

                case 6:
                    if (model.GetLanguage() == "fr")
                    {
                        Console.WriteLine("Quel langage voulez vous afficher?");
                    }
                    if (model.GetLanguage() == "en")
                    {
                        Console.WriteLine("wich language would you want to display?");
                    }
                    int languageCounter = 1;
                    foreach(String language in model.GetLanguageList())
                    {
                        Console.WriteLine(Convert.ToString(languageCounter)+" "+ language);
                        languageCounter++;
                    }
                    int languageChoice = Convert.ToInt32(Console.ReadLine());
                    model.SetLanguage(model.GetLanguageList()[languageChoice - 1]);//select of the right language into the list in the model class
                    ChangeViewMenuInput(0);
                    affichage();
                    break;
                case 7:
                    if (model.GetLanguage() == "fr")
                    {
                        Console.WriteLine("Etes vous sur de vouloir quitter l'application?\n\n1 - oui\n2 - non\n");
                    }
                    if (model.GetLanguage() == "en")
                    {
                        Console.WriteLine("Are you sure that you want to exit from the app?\n\n1 - yes\n2 - no\n");
                    }
                    int ExitChoice = Convert.ToInt32(Console.ReadLine());

                    if(ExitChoice == 1)
                    {
                        return;
                    }
                    else
                    {
                        ChangeViewMenuInput(0);
                        affichage();
                    }

                    break;
            }
        }

        public void ChangeViewMenuInput(int a)
        {
            Console.Clear();
            model.SetMenuView(a);
            affichage();
        }
    }
}
