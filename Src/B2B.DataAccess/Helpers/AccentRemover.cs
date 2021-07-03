namespace B2B.DataAccess.Helpers
{
    public static class AccentRemover
    {
        private const string Accents = "áéíóöőúüűÁÉÍÓÖŐÚÜŰ";
        private const string AccentsReplacements = "aeiooouuuAEIOOOUUU";

        public static string RemoveAccents(string str)
        {
            for (var i = 0; i < str.Length; i++)
            {
                var idx = Accents.IndexOf(str[i]);
                if (idx >= 0)
                    str.Replace(str[i], AccentsReplacements[idx]);
            }

            return str;
        }
    }
}