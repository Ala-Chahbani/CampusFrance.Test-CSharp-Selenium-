namespace CampusFrance.Test.DataUtils.ClassesJDD
{
    // classe représentant les données d'un formulaire utilisateur
    public class FormulaireUtilisateur
    {
        public string Email { get; set;} = "";
        public string MotDePasse { get; set;} = "";
        public string ConfirmationMotDePasse { get; set;} = "";
        public string Civilite { get; set;} = "";
        public string Nom { get; set;} = "";
        public string Prenom { get; set;} = "";
        public string PaysResidence { get; set;} = "";
        public string[] PaysNationalite { get; set;} = [];
        public string CodePostal { get; set;} = "";
        public string Ville { get; set;} = "";
        public string Telephone { get; set;} = "";
        public string Profession { get; set;} = "";
        public string DomaineEtude { get; set;} = "";
        public string NiveauEtude { get; set;} = "";
        public bool ConcentementTraitementDonnees { get; set;} = false;
    }
}
