using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Language
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual IList<GuideLanguage> GuideLanguage {get; set;}
    }
}
