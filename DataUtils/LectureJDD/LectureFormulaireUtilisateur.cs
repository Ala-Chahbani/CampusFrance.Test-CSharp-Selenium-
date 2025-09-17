using CampusFrance.Test.DataUtils.ClassesJDD;
using System.Text.Json;

namespace CampusFrance.Test.DataUtils.LectureJDD
{
    public class LectureFormulaireUtilisateur
    {
        // permet la lecture d'un objet FormulaireUtilisateur depuis un fichier JSON
        public static FormulaireUtilisateur LectureFormulaireUtilisateurDepuisJSON(string cheminFichierRelatif)
        {
            StreamReader lecteurFichier = new(cheminFichierRelatif);
            string contenuFichier = lecteurFichier.ReadToEnd();
            // transforme le contenu textuel du fichier en un objet FormulaireUtilisateur et lève une exception en cas d'echec
            FormulaireUtilisateur formulaireUtilisateur = JsonSerializer.Deserialize<FormulaireUtilisateur>(contenuFichier)
                ?? throw new Exception("Le fichier ne correspond pas à un objet FormulaireUtilisateur valide");
            return formulaireUtilisateur;
        }

        // permet la lecture de plusieurs objets FormulaireUtilisateur depuis un fichier JSON
        public static FormulaireUtilisateur[] LectLecturePlusieursFormulairesUtilisateurDepuisJSON(string cheminFichierRelatif)
        {
            StreamReader lecteurFichier = new(cheminFichierRelatif);
            string contenuFichier = lecteurFichier.ReadToEnd();
            // transforme le contenu textuel du fichier en un objet FormulaireUtilisateur et lève une exception en cas d'echec
            FormulaireUtilisateur[] formulairesUtilisateur = JsonSerializer.Deserialize<FormulaireUtilisateur[]>(contenuFichier)
                ?? throw new Exception("Le fichier ne correspond pas à une liste d'objets FormulaireUtilisateur valide");
            return formulairesUtilisateur;
        }
    }
}
