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
                        "2 - Faire une sauvegarde\n" +
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
                        "2 - make a save\n" +
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

                case 1: //Affichage mode 1
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
                            Console.WriteLine("Entrez le chemin  de l'emplacement de la sauvegarde");
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
                                    "Entrez un chiffre ppour choisir type d'enregistrement:\n" +
                                    "1 - complet (on resauvegarde tout l'élément)" +
                                    "2 - différentiel (sauvegarde seulement les changements lorsqu'il y en a)"
                                );
                        }
                        if (model.GetLanguage() == "en")
                        {
                            Console.WriteLine
                                (
                                    "Type the number to choose a type of saving:\n" +
                                    "1 - complete (saving of all of the element)" +
                                    "2 - diffential (saving of changes only if they exists)"
                                );
                        }
                    int choixType = Convert.ToInt32(Console.ReadLine());
                        if(choixType == 1)
                        {
                            int b = 2; //remplacer par le change du SaveWorkModel
                        }
                        if (choixType == 2)
                        {
                            int b = 2; //remplacer par le change du SaveWorkModel
                        }
                        ChangeViewMenuInput(0);
                        affichage();
                    
                    if (model.GetLanguage() == "en")
                    {
                        Console.WriteLine("");
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
