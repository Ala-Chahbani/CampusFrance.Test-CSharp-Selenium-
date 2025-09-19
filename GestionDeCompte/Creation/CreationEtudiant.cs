using CampusFrance.Test.DataUtils.ClassesJDD;
using CampusFrance.Test.DataUtils.LectureJDD;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace CampusFrance.Test.GestionDeCompte.Creation
{
    public class CreationEtudiant
    {
        // gère le driver qui transmet les commandes utilisateurs au navigateur Edge
        private readonly IWebDriver Driver = new ChromeDriver();

        private readonly string UrlPageCreation = "https://www.campusfrance.org/fr/user/register";

        //  prépare l'exécution de l'ensemble des tests de la classe CreationEtudiant
        [OneTimeSetUp]
        public void Preparation()
        {
            Driver.Manage().Window.Maximize();
        }

        private void RenseignementFormulaireEntier(FormulaireUtilisateur utilisateur)
        {
            // attendre la disparition des cookies
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(5));
            wait.Until(_ => {
                try
                {
                    IWebElement boutonRefusCookies = Driver.FindElement(By.Id("tarteaucitronAllDenied2"));
                    boutonRefusCookies.Click();
                    return !boutonRefusCookies.Displayed;
                }
                catch (Exception e)
                {
                    return false;
                }
            });

            // email
            Driver.FindElement(By.CssSelector(".username")).SendKeys(utilisateur.Email);

            // mot de passe
            Driver.FindElement(By.Id("edit-pass-pass1")).SendKeys(utilisateur.MotDePasse);

            // confirmation mot de passe
            Driver.FindElement(By.Id("edit-pass-pass2")).SendKeys(utilisateur.ConfirmationMotDePasse);

            // civilité
            if (utilisateur.Civilite == "Mr")
            {
                Driver.FindElement(By.XPath("//*[@id=\"edit-field-civilite\"]/div[2]/label")).Click();
            }
            else if (utilisateur.Civilite == "Mme")
            {
                Driver.FindElement(By.XPath("//*[@id=\"edit-field-civilite\"]/div[1]/label")).Click();
            }

            // nom
            Driver.FindElement(By.Id("edit-field-nom-0-value")).SendKeys(utilisateur.Nom);

            // prenom
            Driver.FindElement(By.Id("edit-field-prenom-0-value")).SendKeys(utilisateur.Prenom);

            // pays de résidence
            IWebElement champPaysResidence = Driver.FindElement(By.Id("edit-field-pays-concernes-selectized"));
            champPaysResidence.Click();
            champPaysResidence.SendKeys(Keys.Backspace);
            champPaysResidence.SendKeys(utilisateur.PaysResidence);
            champPaysResidence.SendKeys(Keys.Enter);

            // ajout de champs pays de nationalité si nécessaire
            for (int i = 1; i < utilisateur.PaysNationalite.Length; i++)
            {
                IWebElement boutonAjoutNationalite = Driver.FindElement(By.CssSelector(".field-add-more-submit"));
                boutonAjoutNationalite.Click();
                // attendre l'apparition du nouveau champ pays de nationalité créé
                wait.Until(_ =>
                {
                    try
                    {
                        Driver.FindElement(By.Name("field_nationalite[" + i + "][target_id]"));
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                });
            }

            // pays de nationalité
            for (int i = 0; i < utilisateur.PaysNationalite.Length; i++)
            {
                IWebElement champPaysNationalite = Driver.FindElement(By.Name("field_nationalite[" + i + "][target_id]"));
                champPaysNationalite.Click();
                champPaysNationalite.SendKeys(utilisateur.PaysNationalite[i]);
                // attendre l'apparition de la liste des options
                wait.Until(_ =>
                {
                    try
                    {
                        Driver.FindElement(By.XPath("/html/body/ul[" + (i + 1) + "]/li[1]/a"));
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                });
                // clic sur la première option proposée
                Driver.FindElement(By.XPath("/html/body/ul[" + (i + 1) + "]/li[1]/a")).Click();
            }

            // code postal
            Driver.FindElement(By.Id("edit-field-code-postal-0-value")).SendKeys(utilisateur.CodePostal);

            // ville
            Driver.FindElement(By.Id("edit-field-ville-0-value")).SendKeys(utilisateur.Ville);

            // téléphone
            Driver.FindElement(By.Id("edit-field-telephone-0-value")).SendKeys(utilisateur.Telephone);

            // etudiant
            if (utilisateur.Profession == "etudiant")
            {
                Driver.FindElement(By.XPath("//*[@id=\"edit-field-publics-cibles\"]/div[1]/label")).Click();

                // attendre l'apparition du champ de domaine d'études
                wait.Until(_ =>
                {
                    try
                    {
                        Driver.FindElement(By.Id("edit-field-domaine-etudes-selectized"));
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                });

                // domaine d'études
                IWebElement domaineEtudes = Driver.FindElement(By.Id("edit-field-domaine-etudes-selectized"));
                domaineEtudes.Click();
                domaineEtudes.SendKeys(Keys.Backspace);
                domaineEtudes.SendKeys(utilisateur.DomaineEtude);
                domaineEtudes.SendKeys(Keys.Enter);

                // niveau d'études
                IWebElement niveauEtudes = Driver.FindElement(By.Id("edit-field-niveaux-etude-selectized"));
                niveauEtudes.Click();
                niveauEtudes.SendKeys(Keys.Backspace);
                niveauEtudes.SendKeys(utilisateur.NiveauEtude);
                niveauEtudes.SendKeys(Keys.Enter);
            }

            // consentement de traitement des données
            if (utilisateur.ConcentementTraitementDonnees)
            {
                Driver.FindElement(By.XPath("//*[@id=\"edit-field-accepte-communications-wrapper\"]/div/label")).Click();
            }
        }

        // méthode de test de renseignement d'un étudiant de mathématiques en licence 3
        [Test]
        public void RenseignementEtudiantMathematiquesLicence3()
        {
            // le fichier JSON du formulaire utilisateur doit être copié dans le dossier du build de test
            FormulaireUtilisateur formulaireUtilisateur = LectureFormulaireUtilisateur.LectLecturePlusieursFormulairesUtilisateurDepuisJSON(".\\Data\\FormulaireUtilisateur\\etudiant_data.json")[0];

            // remplissage du formulaire
            Driver.Navigate().GoToUrl(UrlPageCreation);
            RenseignementFormulaireEntier(formulaireUtilisateur);

            // le fichier JSON des valeurs attendues doit être copié dans le dossier du build de test
            FormulaireUtilisateur valeursAttendues = LectureFormulaireUtilisateur.LectLecturePlusieursFormulairesUtilisateurDepuisJSON(".\\Data\\FormulaireUtilisateur\\etudiant_expected.json")[0];
            Assert.Multiple(() =>
            {
                // vérification du bouton de création
                Assert.That(Driver.FindElement(By.Id("edit-submit")).GetAttribute("value"), Is.EqualTo("Créer un compte"));

                // vérification de la case de fonction
                Assert.That(Driver.FindElement(By.Id("edit-field-publics-cibles-2")).Selected, Is.EqualTo(true));

                // vérification du domaine d'étude
                Assert.That(Driver.FindElement(By.XPath("//*[@id=\"edit-field-domaine-etudes-wrapper\"]/div/div/div[1]/div")).Text, Is.EqualTo(valeursAttendues.DomaineEtude));

                // vérification du niveau d'étude
                Assert.That(Driver.FindElement(By.XPath("//*[@id=\"edit-field-niveaux-etude-wrapper\"]/div/div/div[1]/div")).Text, Is.EqualTo(valeursAttendues.NiveauEtude));
            });
        }

        [Test]
        public void RenseignementEtudiantSciencesEconomiquesEtPolitiquesMaster2()
        {
            // le fichier JSON du formulaire utilisateur doit être copié dans le dossier du build de test
            FormulaireUtilisateur formulaireUtilisateur = LectureFormulaireUtilisateur.LectLecturePlusieursFormulairesUtilisateurDepuisJSON(".\\Data\\FormulaireUtilisateur\\etudiant_data.json")[1];

            // remplissage du formulaire
            Driver.Navigate().GoToUrl(UrlPageCreation);
            RenseignementFormulaireEntier(formulaireUtilisateur);

            // le fichier JSON des valeurs attendues doit être copié dans le dossier du build de test
            FormulaireUtilisateur valeursAttendues = LectureFormulaireUtilisateur.LectLecturePlusieursFormulairesUtilisateurDepuisJSON(".\\Data\\FormulaireUtilisateur\\etudiant_expected.json")[1];
            Assert.Multiple(() =>
            {
                // vérification du bouton de création
                Assert.That(Driver.FindElement(By.Id("edit-submit")).GetAttribute("value"), Is.EqualTo("Créer un compte"));

                // vérification de la case de fonction
                Assert.That(Driver.FindElement(By.Id("edit-field-publics-cibles-2")).Selected, Is.EqualTo(true));

                // vérification du domaine d'étude
                Assert.That(Driver.FindElement(By.XPath("//*[@id=\"edit-field-domaine-etudes-wrapper\"]/div/div/div[1]/div")).Text, Is.EqualTo(valeursAttendues.DomaineEtude));

                // vérification du niveau d'étude
                Assert.That(Driver.FindElement(By.XPath("//*[@id=\"edit-field-niveaux-etude-wrapper\"]/div/div/div[1]/div")).Text, Is.EqualTo(valeursAttendues.NiveauEtude));
            });
        }

        // code exécuté après l'exécution de chaque test de la classe CreationEtudiant
        [TearDown]
        public void TearDown()
        {
            // supprime les cookies enregistrées par le test
            Driver.Manage().Cookies.DeleteAllCookies();
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