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
            int a = 0;
            switch (model.GetMenuView())
            {
                case 0:
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
                    break;
            }
        }
    }
}
