using System.Collections.Generic;
using System.Linq;
using MonoGame.Extended;
using Quaver.Shared.Config;
using Quaver.Shared.Graphics;
using Quaver.Shared.Graphics.Form.Dropdowns;
using Quaver.Shared.Localization;
using Wobble.Bindables;
using Wobble.Graphics;

namespace Quaver.Shared.Screens.Options.Items.Custom
{
    public class OptionsItemLanguage : OptionsItemDropdown
    {
        /// <summary>
        /// </summary>
        /// <param name="containerRect"></param>
        /// <param name="name"></param>
        /// <param name="language"></param>
        public OptionsItemLanguage(RectangleF containerRect, string name, Bindable<string> language)
            : base(containerRect, name, new Dropdown(GetOptions(), new ScalableVector2(180, 35), 22,
                Colors.MainAccent, GetSelectedIndex(language)))
        {
            Tags = new List<string> { "language", "localization", "translation", "locale" };

            Dropdown.ItemSelected += (sender, args) =>
            {
                if (language == null)
                    return;

                language.Value = Translator.SupportedLanguages[args.Index].Code;
            };
        }

        /// <summary>
        ///     The display names of the supported languages, in order.
        /// </summary>
        /// <returns></returns>
        private static List<string> GetOptions()
            => Translator.SupportedLanguages.Select(x => x.Name).ToList();

        /// <summary>
        ///     The index of the currently configured language (defaults to the first language if unknown).
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        private static int GetSelectedIndex(Bindable<string> language)
        {
            if (language == null)
                return 0;

            for (var i = 0; i < Translator.SupportedLanguages.Count; i++)
            {
                if (Translator.SupportedLanguages[i].Code == language.Value)
                    return i;
            }

            return 0;
        }
    }
}
