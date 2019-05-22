using System;
using System.Threading.Tasks;

namespace KingTranslator
{
    public interface ITranslator
    {
        void TranslatePhrase(TranslatorItem item);

        Task SaveToDictionary(TranslatorItem item);

        Task DeleteFromDictionary(TranslatorItem item);
    }
}
