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

        [Test]
        public void CreationEtudiantMathematiquesLicence3()
        {
            Assert.Ignore();
        }

        [OneTimeTearDown]
        public void Fin()
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }
}