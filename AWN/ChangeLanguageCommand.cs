using System;
using System.Threading.Tasks;

namespace AWN
{
    public class ChangeLanguageCommand : ICommand
    {
        private readonly OpenAiService _openAiService;

        public ChangeLanguageCommand(OpenAiService openAiService)
        {
            _openAiService = openAiService;
        }

        public bool CanExecute(string commandInput)
        {
            return commandInput.StartsWith("change language to ", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<string> ExecuteAsync(string commandInput)
        {
            var language = commandInput.Substring("change language to ".Length).TrimEnd('.');
            var isoLanguageCode = await _openAiService.TranslateLanguageToIsoCodeAsync(language);

            if (string.IsNullOrEmpty(isoLanguageCode))
            {
                return "Failed to change language. The specified language is not recognized.";
            }

            Environment.SetEnvironmentVariable("DEFAULT_LANGUAGE", isoLanguageCode);
            return $"Language changed to {language} ({isoLanguageCode}).";
        }
    }
}
