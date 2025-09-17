using CampusFrance.Test.DataUtils.ClassesJDD;
using CampusFrance.Test.DataUtils.LectureJDD;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace CampusFrance.Test.GestionDeCompte.Creation
{
    public class CreationEtudiant
    {
        // g�re le driver qui transmet les commandes utilisateurs au navigateur Edge
        private readonly IWebDriver Driver = new EdgeDriver();

        // l'url de la page de cr�ation
        private readonly string UrlPageCreation = "https://www.campusfrance.org/fr/user/register";

        //  pr�pare l'ex�cution de l'ensemble des tests de la classe CreationEtudiant
        [OneTimeSetUp]
        public void Preparation()
        {
            // maximise la fen�tre lanc�e par le driver
            Driver.Manage().Window.Maximize();
        }

        // m�thode de test de renseignement d'un �tudiant de math�matiques en licence 3
        [Test]
        public void RenseignementEtudiantMathematiquesLicence3()
        {
            // le fichier JSON du formulaire utilisateur doit �tre copi� dans le dossier du build de test
            FormulaireUtilisateur formulaireUtilisateur = LectureFormulaireUtilisateur.LectureFormulaireUtilisateurDepuisJSON(".\\Data\\FormulaireUtilisateur\\etudiant_data.json");
            Assert.Ignore();
        }

        // code ex�cut� apr�s l'ex�cution de tous les tests de la classe CreationEtudiant
        [OneTimeTearDown]
        public void Fin()
        {
            // ferme enti�rement le navigateur et arr�te le processus msedgedriver
            Driver.Quit();
            // lib�re l'objet de gestion du driver
            Driver.Dispose();
        }
    }
}