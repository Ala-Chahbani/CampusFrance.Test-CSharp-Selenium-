using CampusFrance.Test.DataUtils.ClassesJDD;
using CampusFrance.Test.DataUtils.LectureJDD;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace CampusFrance.Test.GestionDeCompte.Creation
{
    public class CreationEtudiant
    {
        // gère le driver qui transmet les commandes utilisateurs au navigateur Edge
        private readonly IWebDriver Driver = new EdgeDriver();

        // l'url de la page de création
        private readonly string UrlPageCreation = "https://www.campusfrance.org/fr/user/register";

        //  prépare l'exécution de l'ensemble des tests de la classe CreationEtudiant
        [OneTimeSetUp]
        public void Preparation()
        {
            // maximise la fenêtre lancée par le driver
            Driver.Manage().Window.Maximize();
        }

        // méthode de test de renseignement d'un étudiant de mathématiques en licence 3
        [Test]
        public void RenseignementEtudiantMathematiquesLicence3()
        {
            // le fichier JSON du formulaire utilisateur doit être copié dans le dossier du build de test
            FormulaireUtilisateur formulaireUtilisateur = LectureFormulaireUtilisateur.LectureFormulaireUtilisateurDepuisJSON(".\\Data\\FormulaireUtilisateur\\etudiant_data.json");
            Assert.Ignore();
        }

        // code exécuté après l'exécution de tous les tests de la classe CreationEtudiant
        [OneTimeTearDown]
        public void Fin()
        {
            // ferme entièrement le navigateur et arrête le processus msedgedriver
            Driver.Quit();
            // libère l'objet de gestion du driver
            Driver.Dispose();
        }
    }
}